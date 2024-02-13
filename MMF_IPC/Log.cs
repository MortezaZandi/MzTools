using System;

public class Log
{
    public DateTime LogTime { get; set; }
    public string LogMessage { get; set; }

    public Log()
    {
    }

    public Log(string logMessage)
    {
        Log = DateTime.Now;
        LogMessage = logMessage;
    }
