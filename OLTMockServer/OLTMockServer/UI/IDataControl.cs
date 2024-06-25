using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLTMockServer.UI
{
    public interface IDataControl
    {
        IConfirmableDialog ParentDialog { get; set; }
        DockStyle Dock { get; set; }
    }
}
