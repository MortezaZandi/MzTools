using Autofac;
using System.Windows.Forms;

namespace MBase.UI.WinForm
{
    public class ApContext
    {
        private static ApContext instance;
        private readonly IContainer container;
        public ApContext()
        {
        }

        private ApContext(IContainer container)
        {
            this.container = container;
        }

        public static ApContext Instance
        {
            get { return instance; }
        }

        public IContainer Container
        {
            get { return container; }
        }

        public static void Initialize(IContainer container)
        {
            if (instance == null)
            {
                instance = new ApContext(container);
            }
        }
    }
} 