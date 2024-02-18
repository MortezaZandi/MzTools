using System.Threading;

namespace MMF_IPC
{
    public class MLogger
    {
        private MMF mmf;
        private EventWaitHandle writeDoneSignal;
        private EventWaitHandle logAvailableSignal;
        private string logName;
        private Thread logAvailableReporterThread;

        public delegate void NewLogAvailableEventHandler(string log);

        public event NewLogAvailableEventHandler OnNewLogAvailable;

        public MLogger(string logName)
        {
            this.logName = logName;
            this.writeDoneSignal = new EventWaitHandle(false, EventResetMode.ManualReset, $"{this.logName}-WriteDone");
        }

        public void Create()
        {
            mmf = new MMF(this.logName, 10);

            mmf.Create();

            this.logAvailableSignal = new EventWaitHandle(true, EventResetMode.ManualReset, $"{this.logName}-LogAvailable");
        }

        public bool IsLoggerAvailable()
        {
            bool ev = EventWaitHandle.TryOpenExisting($"{this.logName}-LogAvailable", out logAvailableSignal);

            return (logAvailableSignal != null);
        }

        public void Open()
        {
            bool ev = EventWaitHandle.TryOpenExisting($"{this.logName}-LogAvailable", out logAvailableSignal);

            if (logAvailableSignal == null)
            {
                throw new System.Exception($"Logger not available for {this.logName}");
            }

            this.mmf = new MMF(this.logName, 10);

            this.mmf.Open();


            this.logAvailableReporterThread = new Thread(() =>
            {
                while (true)
                {
                    if (IsNewLogAvailable())
                    {
                        OnNewLogAvailable?.Invoke(ReadLastLog());
                    }

                    Thread.Sleep(10);
                }
            });

            this.logAvailableReporterThread.IsBackground = true;

            this.logAvailableReporterThread.Start();
        }

        public void SetLogAvailableSignal()
        {
            this.writeDoneSignal.Set();
        }

        public void WriteNewLog(string message)
        {
            mmf.Write(message);

            SetLogAvailableSignal();
        }

        public string ReadLastLog()
        {
            writeDoneSignal.Reset();

            return mmf.Read();
        }

        public bool IsNewLogAvailable()
        {
            return this.writeDoneSignal.WaitOne(1);
        }
    }
}
