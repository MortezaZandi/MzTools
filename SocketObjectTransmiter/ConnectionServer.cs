using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SocketObjectTransmiter.Models;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter
{
    public class ConnectionServer : ConnectionBase
    {
        private readonly List<ConnectionHandler> _activeConnections = new List<ConnectionHandler>();
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        
        public event EventHandler<ConnectionHandler> OnClientConnected;
        public event EventHandler<ConnectionHandler> OnClientDisconnected;
        public event EventHandler<(ConnectionHandler Connection, SocketMessage Message)> OnMessageReceived;

        public IReadOnlyList<ConnectionHandler> ActiveConnections => _activeConnections.AsReadOnly();

        public ConnectionServer(int port, bool isUdp = false, string password = null) 
            : base(port, isUdp, password)
        {
        }

        public async Task StartAsync()
        {
            try
            {
                Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
                
                if (!IsUdp)
                {
                    Socket.Listen(100); // Allow up to 100 pending connections
                }

                Status = ConnectionStatus.Listening;
                
                while (!_cancellationSource.Token.IsCancellationRequested)
                {
                    if (IsUdp)
                    {
                        await HandleUdpConnectionAsync();
                    }
                    else
                    {
                        await HandleTcpConnectionAsync();
                    }
                }
            }
            catch (Exception)
            {
                Status = ConnectionStatus.Disconnected;
                throw;
            }
        }

        private async Task HandleTcpConnectionAsync()
        {
            var clientSocket = await Socket.AcceptAsync();
            var handler = new ConnectionHandler(clientSocket);
            
            if (RequireAuthentication)
            {
                if (!await AuthenticateClient(handler))
                {
                    handler.Dispose();
                    return;
                }
            }

            InitializeConnectionHandler(handler);
            _ = handler.StartAsync(); // Start handling messages in background
        }

        private async Task HandleUdpConnectionAsync()
        {
            var handler = new ConnectionHandler(Socket, true);
            
            if (RequireAuthentication)
            {
                if (!await AuthenticateClient(handler))
                {
                    handler.Dispose();
                    return;
                }
            }

            InitializeConnectionHandler(handler);
            await handler.StartAsync();
        }

        private async Task<bool> AuthenticateClient(ConnectionHandler handler)
        {
            // Wait for authentication message
            var authTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            var authResult = new TaskCompletionSource<bool>();
            
            try
            {
                // Create message handler before starting the connection handler
                void OnMessage(object s, SocketMessage msg)
                {
                    if (msg.MessageType == MessageType.Authentication)
                    {
                        var receivedPassword = msg.Data?.ToString();
                        authResult.TrySetResult(receivedPassword == Password);
                    }
                }

                // Subscribe to messages
                handler.OnMessageReceived += OnMessage;

                // Start the handler to begin receiving messages
                _ = handler.StartAsync();
                
                using (authTimeout)
                {
                    var timeoutTask = Task.Delay(TimeSpan.FromSeconds(60), authTimeout.Token);
                    var completedTask = await Task.WhenAny(authResult.Task, timeoutTask);
                    
                    // Clean up the event handler
                    handler.OnMessageReceived -= OnMessage;
                    
                    if (completedTask == timeoutTask)
                    {
                        return false;
                    }
                    return await authResult.Task;
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        private void InitializeConnectionHandler(ConnectionHandler handler)
        {
            handler.OnDisconnected += (s, e) =>
            {
                _activeConnections.Remove(handler);
                OnClientDisconnected?.Invoke(this, handler);
            };

            handler.OnMessageReceived += (s, message) =>
            {
                OnMessageReceived?.Invoke(this, (handler, message));
            };

            _activeConnections.Add(handler);
            OnClientConnected?.Invoke(this, handler);
        }

        public async Task BroadcastAsync(object data)
        {
            foreach (var connection in _activeConnections)
            {
                await connection.SendAsync(data);
            }
        }

        public override void Dispose()
        {
            _cancellationSource.Cancel();
            foreach (var connection in _activeConnections)
            {
                connection.Dispose();
            }
            _activeConnections.Clear();
            base.Dispose();
        }
    }
} 