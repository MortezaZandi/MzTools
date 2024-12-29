using Autofac;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TestAutoFac
{
    public interface IFramework
    {
        void ShowServiceDialog<TDialog>() where TDialog : Form;
        void ShowDialog<TDialog>() where TDialog : Form;
    }

    public class Framework : IFramework
    {
        private readonly ILifetimeScope lifetimeScope;

        public Framework(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public void ShowServiceDialog<TDialog>() where TDialog : Form
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                using (var dialog = scope.Resolve<TDialog>())
                {
                    SetDialogCommonProperties(dialog);
                    dialog.ShowDialog();
                }
            }
        }

        public void ShowDialog<TDialog>() where TDialog : Form
        {
            using (var dialog = lifetimeScope.Resolve<TDialog>())
            {
                SetDialogCommonProperties(dialog);
                dialog.ShowDialog();
            }
        }

        private void SetDialogCommonProperties(Form dialog)
        {
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.Text += "- Auto Generated";
        }
    }
}