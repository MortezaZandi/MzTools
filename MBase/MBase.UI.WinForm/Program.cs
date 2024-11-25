using Autofac;
using MBase.Common.Core.AppServices;
using MBase.Common.Definistions.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBase.UI.WinForm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var Container = InitializeIOC();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<Form1>());
        }


        private static IContainer InitializeIOC()
        {
            var assemblyList =
                Directory.GetFiles(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MBase.*",
                    SearchOption.TopDirectoryOnly)
                        .Where(f => f.EndsWith(".dll") || f.EndsWith(".exe"))
                        .Select(f => Assembly.LoadFrom(f)).ToList();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
              .Where(type => type.IsAssignableFrom(type)).AsSelf()
              .InstancePerLifetimeScope();

            // builder.RegisterType<Form1>();

            return builder.Build();
        }
    }
}
