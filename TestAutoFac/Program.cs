using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAutoFac
{
    internal static class Program
    {
        private static Autofac.IContainer container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Resolve Form1 from the container
            using (var scope = container.BeginLifetimeScope())
            {
                var mainForm = scope.Resolve<MainDialog>();
                Application.Run(mainForm);
            }
        }

        private static void InitContainer()
        {
            var builder = new ContainerBuilder();

            // Register DialogService
            builder.RegisterType<Framework>()
                  .As<IFramework>()
                  .InstancePerLifetimeScope();

            // Register Forms
            builder.RegisterType<MainDialog>();
            
            builder.RegisterType<PersonDialog>()
                  .InstancePerLifetimeScope();
            
            builder.RegisterType<AddressDialog>()
                  .InstancePerDependency();

            // Register services
            builder.RegisterType<UnitOfWork>()
                  .As<IUnitOfWork>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<PersonService>()
                  .As<IPersonService>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<AddressService>()
                  .As<IAddressService>()
                  .InstancePerLifetimeScope();

            container = builder.Build();
        }
    }
}
