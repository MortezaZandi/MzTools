using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.UI
{
    public interface IWizardDialog : IConfirmableDialog
    {
        void GoToNextPage();
        void GoToPreviousPage();
        T GetPageOfType<T>() where T : DataWizardBaseControl;

        IUIFactory UIFactory { get; }
    }
}
