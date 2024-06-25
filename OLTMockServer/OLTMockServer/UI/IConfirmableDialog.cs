using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.UI
{
    public interface IConfirmableDialog
    {
        void OK();
        void Cancel();
    }
}
