using System;
using System.Net.Sockets;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter
{
    public abstract class ConnectionBase : IDisposable
    {
        protected readonly string Password;
        protected readonly bool RequireAuthentication;
        protected readonly bool IsUdp;
        protected Socket Socket;
        protected readonly int Port;
        
        public ConnectionStatus Status { get; protected set; } = ConnectionStatus.NotConnected;
        public int MessageMaxSize { get; set; } = 1024 * 64;
        public bool IsConnected => Status == ConnectionStatus.Connected;

        protected ConnectionBase(int port, bool isUdp = false, string password = null)
        {
            Port = port;
            IsUdp = isUdp;
            Password = password;
            RequireAuthentication = !string.IsNullOrEmpty(Password);
            
            Socket = new Socket(
                AddressFamily.InterNetwork,
                isUdp ? SocketType.Dgram : SocketType.Stream,
                isUdp ? ProtocolType.Udp : ProtocolType.Tcp
            );
        }

        public virtual void Dispose()
        {
            Socket?.Dispose();
        }
    }
} 