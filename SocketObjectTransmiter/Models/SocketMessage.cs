using System;
using SocketObjectTransmiter.Interfaces;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter.Models
{
    [Serializable]
    public class SocketMessage : ISocketMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public string SenderAddress { get; set; }
        public DateTime SendTime { get; set; } = DateTime.UtcNow;
        public object Data { get; set; }
        public int PartNumber { get; set; }
        public int TotalParts { get; set; } = 1;
        public bool IsComplete => PartNumber == TotalParts;
        public MessageType MessageType { get; set; } = MessageType.Data;
    }
} 