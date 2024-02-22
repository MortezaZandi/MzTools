using MZCryptoTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMF_IPC_Writer
{
    public class TraceLogInfo
    {
        private Stopwatch sp_call_PS_WCF_Method_A;
        private Stopwatch sp_call_PS_WCF_Method_B;
        private Stopwatch sp_call_PS_WCF_Method_C;

        private int Call_Count_WCF_Method_A;
        private int Call_Count_WCF_Method_B;
        private int Call_Count_WCF_Method_C;
        private int LoopStepName;

        public void Start_CallTo_WCF_Method_A()
        {
            sp_call_PS_WCF_Method_A.Start();
        }

        public void Start_CallTo_WCF_Method_B()
        {
            sp_call_PS_WCF_Method_B.Start();
        }

        public void Start_CallTo_WCF_Method_C()
        {
            sp_call_PS_WCF_Method_C.Start();
        }

        public void End_CallTo_WCF_Method_A()
        {
            sp_call_PS_WCF_Method_A.Stop();
        }

        public void End_CallTo_WCF_Method_B()
        {
            sp_call_PS_WCF_Method_B.Stop();
        }

        public void End_CallTo_WCF_Method_C()
        {
            sp_call_PS_WCF_Method_C.Stop();
        }

        public void Reset()
        {
            Call_Count_WCF_Method_A = 0;
            Call_Count_WCF_Method_B = 0;
            Call_Count_WCF_Method_C = 0;
            LoopStepName = 0;

            sp_call_PS_WCF_Method_A.Restart();
            sp_call_PS_WCF_Method_B.Restart();
            sp_call_PS_WCF_Method_C.Restart();
        }

        public override string ToString()
        {
            return XmlDataSerializer.SerializeToXMLString(this);
        }
    }
}
