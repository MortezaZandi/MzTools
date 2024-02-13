using System;

public class Log
{
    public DateTime LogTime { get; set; }
    public string LogMessage { get; set; }
    public int LogLevel { get; set; }
    public Guid LogID { get; set; }

    public Log()
    {
        this.LogTime = DateTime.Now;
        this.LogID = Guid.NewGuid();
    }

    public Log(string logMessage) : this()
    {
        this.LogMessage = logMessage;
    }
}
