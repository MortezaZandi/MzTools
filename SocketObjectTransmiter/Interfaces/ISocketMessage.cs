using System;
using SocketObjectTransmiter.Enums;

namespace SocketObjectTransmiter.Interfaces
{
    public interface ISocketMessage
    {
        Guid MessageId { get; set; }
        string SenderAddress { get; set; }
        DateTime SendTime { get; set; }
        object Data { get; set; }
        int PartNumber { get; set; }
        int TotalParts { get; set; }
        bool IsComplete { get; }
        MessageType MessageType { get; set; }
    }
} 