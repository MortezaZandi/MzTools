using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using SocketObjectTransmiter.Models;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter
{
    public class ConnectionHandler : IDisposable
    {
        private readonly Socket _socket;
        private readonly bool _isUdp;
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        private readonly Dictionary<Guid, List<SocketMessage>> _partialMessages = new Dictionary<Guid, List<SocketMessage>>();
        private readonly byte[] _buffer = new byte[8192];
        private readonly Timer _keepAliveTimer;
        private EndPoint _remoteEndPoint;

        public event EventHandler<SocketMessage> OnMessageReceived;
        public event EventHandler OnDisconnected;
        public event EventHandler OnConnected;
        
        public ConnectionStatus Status { get; private set; }
        public string RemoteAddress
        {
            get
            {
                try
                {
                    if (_isUdp)
                    {
                        return _remoteEndPoint?.ToString();
                    }
                    return (_socket.RemoteEndPoint as IPEndPoint)?.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }
        public int MessageMaxSize { get; set; } = 1024 * 64; // 64KB default

        public ConnectionHandler(Socket socket, bool isUdp = false)
        {
            _socket = socket;
            _isUdp = isUdp;
            Status = ConnectionStatus.NotConnected;
            
            if (isUdp)
            {
                _keepAliveTimer = new Timer(SendKeepAlive, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            }
        }

        public async Task StartAsync()
        {
            Status = ConnectionStatus.Connected;
            OnConnected?.Invoke(this, EventArgs.Empty);

            try
            {
                while (!_cancellationSource.Token.IsCancellationRequested)
                {
                    if (_isUdp)
                    {
                        EndPoint remoteEP = _remoteEndPoint;
                        var result = await _socket.ReceiveFromAsync(new ArraySegment<byte>(_buffer), SocketFlags.None, remoteEP);
                        await ProcessReceivedDataAsync(_buffer.Take(result.ReceivedBytes).ToArray());
                    }
                    else
                    {
                        var received = await _socket.ReceiveAsync(new ArraySegment<byte>(_buffer), SocketFlags.None);
                        if (received == 0) break; // Connection closed
                        await ProcessReceivedDataAsync(_buffer.Take(received).ToArray());
                    }
                }
            }
            catch (Exception)
            {
                HandleDisconnection();
            }
        }

        public async Task SendAsync(object data)
        {
            if (data is SocketObjectTransmiter.Models.SocketMessage socketMessage)
            {
                await SendMessageAsync(socketMessage);
            }
            else
            {
                var message = new SocketObjectTransmiter.Models.SocketMessage { Data = data };
                await SendMessageAsync(message);
            }
        }

        private async Task SendMessageAsync(SocketMessage message)
        {
            var serialized = SerializeMessage(message);
            
            if (serialized.Length > MessageMaxSize)
            {
                var parts = SplitMessage(serialized, message.MessageId, message.MessageType);
                foreach (var part in parts)
                {
                    await SendSerializedMessageAsync(SerializeMessage(part));
                }
            }
            else
            {
                await SendSerializedMessageAsync(serialized);
            }
        }

        private async Task SendSerializedMessageAsync(byte[] data)
        {
            if (_isUdp)
            {
                await _socket.SendToAsync(new ArraySegment<byte>(data), SocketFlags.None, _remoteEndPoint);
            }
            else
            {
                await _socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
            }
        }

        private async Task ProcessReceivedDataAsync(byte[] data)
        {
            var message = DeserializeMessage(data);

            switch (message.MessageType)
            {
                case MessageType.KeepAlive:
                    // Handle keep-alive
                    break;
                    
                case MessageType.Authentication:
                    OnMessageReceived?.Invoke(this, message);
                    break;
                    
                case MessageType.Data:
                    if (message.TotalParts > 1)
                    {
                        ProcessMessagePart(message);
                    }
                    else
                    {
                        OnMessageReceived?.Invoke(this, message);
                    }
                    break;
            }
        }

        private void ProcessMessagePart(SocketMessage part)
        {
            if (!_partialMessages.ContainsKey(part.MessageId))
            {
                _partialMessages[part.MessageId] = new List<SocketMessage>();
            }

            _partialMessages[part.MessageId].Add(part);

            if (_partialMessages[part.MessageId].Count == part.TotalParts)
            {
                var completeMessage = CombineMessageParts(_partialMessages[part.MessageId]);
                _partialMessages.Remove(part.MessageId);
                OnMessageReceived?.Invoke(this, completeMessage);
            }
        }

        private void SendKeepAlive(object state)
        {
            if (Status == ConnectionStatus.Connected && _isUdp)
            {
                var keepAlive = new SocketMessage { MessageType = MessageType.KeepAlive };
                _ = SendMessageAsync(keepAlive);
            }
        }

        private void HandleDisconnection()
        {
            Status = ConnectionStatus.Disconnected;
            OnDisconnected?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _cancellationSource.Cancel();
            _keepAliveTimer?.Dispose();
            _socket?.Dispose();
        }

        // Helper methods for serialization and message splitting
        private byte[] SerializeMessage(SocketMessage message) 
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, message);
            return ms.ToArray();
        }

        private SocketMessage DeserializeMessage(byte[] data)
        {
            using var ms = new MemoryStream(data);
            var formatter = new BinaryFormatter();
            return (SocketMessage)formatter.Deserialize(ms);
        }

        private List<SocketMessage> SplitMessage(byte[] data, Guid messageId, MessageType messageType)
        {
            var parts = new List<SocketMessage>();
            var totalParts = (int)Math.Ceiling(data.Length / (double)MessageMaxSize);

            for (var i = 0; i < totalParts; i++)
            {
                var partData = data.Skip(i * MessageMaxSize).Take(MessageMaxSize).ToArray();
                parts.Add(new SocketMessage
                {
                    MessageId = messageId,
                    PartNumber = i + 1,
                    TotalParts = totalParts,
                    Data = partData,
                    MessageType = messageType
                });
            }

            return parts;
        }

        private SocketMessage CombineMessageParts(List<SocketMessage> parts)
        {
            var orderedParts = parts.OrderBy(p => p.PartNumber).ToList();
            var firstPart = orderedParts.First();
            
            var combinedData = orderedParts
                .SelectMany(p => (byte[])p.Data)
                .ToArray();

            var result = DeserializeMessage(combinedData);
            result.MessageType = firstPart.MessageType;
            return result;
        }
    }
} 