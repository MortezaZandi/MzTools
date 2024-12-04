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

    public static class MyContainer
    {
        public static IContainer container { get; set; }
    }


    internal static class Program
    {

        public static IContainer ioc;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var Container = InitializeIOC();
            ApContext.Initialize(Container);

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
              .InstancePerDependency();

            //// Register Form2 specifically as non-singleton
            //builder.RegisterType<Form2>()
            //    .InstancePerDependency();  // This ensures a new instance each time

            return builder.Build();
        }
    }
}
