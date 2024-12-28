using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketObjectTransmiter.Models;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter
{
    public class ConnectionClient : ConnectionBase
    {
        private ConnectionHandler _connectionHandler;
        
        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler<SocketMessage> OnMessageReceived;

        public string RemoteAddress => _connectionHandler?.RemoteAddress;

        public ConnectionClient(int port, bool isUdp = false, string password = null) 
            : base(port, isUdp, password)
        {
        }

        public async Task ConnectAsync(string serverAddress)
        {
            try
            {
                Status = ConnectionStatus.Connecting;
                
                var endpoint = new IPEndPoint(
                    IPAddress.Parse(serverAddress), 
                    Port
                );

                if (IsUdp)
                {
                    await Socket.ConnectAsync(endpoint);
                    _connectionHandler = new ConnectionHandler(Socket, true);
                }
                else
                {
                    await Socket.ConnectAsync(endpoint);
                    _connectionHandler = new ConnectionHandler(Socket);
                }

                InitializeConnectionHandler();

                if (RequireAuthentication)
                {
                    await SendAuthenticationAsync();
                }

                Status = ConnectionStatus.Connected;
                OnConnected?.Invoke(this, EventArgs.Empty);
                
                await _connectionHandler.StartAsync();
            }
            catch (Exception)
            {
                Status = ConnectionStatus.Disconnected;
                throw;
            }
        }

        private async Task SendAuthenticationAsync()
        {
            var authMessage = new SocketMessage
            {
                MessageType = MessageType.Authentication,
                Data = Password
            };
            
            await _connectionHandler.SendAsync(authMessage);
        }

        private void InitializeConnectionHandler()
        {
            _connectionHandler.OnDisconnected += (s, e) =>
            {
                Status = ConnectionStatus.Disconnected;
                OnDisconnected?.Invoke(this, e);
            };

            _connectionHandler.OnMessageReceived += (s, message) =>
            {
                OnMessageReceived?.Invoke(this, message);
            };
        }

        public async Task SendAsync(object data)
        {
            if (Status != ConnectionStatus.Connected)
                throw new InvalidOperationException("Client is not connected");

            await _connectionHandler.SendAsync(data);
        }

        public override void Dispose()
        {
            _connectionHandler?.Dispose();
            base.Dispose();
        }
    }
} 