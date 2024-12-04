using Autofac;
using System.Windows.Forms;

namespace MBase.UI.WinForm
{
    public class BaseDialog : Form
    {
        protected readonly ApContext appContext;

        public BaseDialog()
        {
            this.appContext = ApContext.Instance;
        }

        protected T Resolve<T>() where T : class
        {
            return appContext.Container.Resolve<T>();
        }
    }
} 