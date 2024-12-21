using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestStrongName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var assemblies = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*");
            int i = 0;
            int found = 0;
            var proc = Process.GetCurrentProcess();
            var tasks = new List<Task>();
            foreach (var assembly in assemblies)
            {
                if (proc.Threads.Count < 12)
                {
                    var t = Task.Factory.StartNew(() =>
                    {
                        Text = $"Sgn Check:  {i}/{assemblies.Count()}";
                        i++;
                        try
                        {
                            var asm = Assembly.LoadFrom(assembly);

                            if (asm != null)
                            {
                                if (!IsAssemblyStrongNamed(asm))
                                {
                                    found++;
                                    lock (this.listBox1)
                                    {
                                        listBox1.Items.Add(assembly);
                                    }
                                }
                            }

                            Application.DoEvents();
                        }
                        catch (Exception)
                        {
                            var f = assembly.ToLower().ToLower();

                            if (f.EndsWith(".dll") || f.EndsWith(".exe"))
                            {
                                listBox2.Items.Add(assembly);
                            }
                        }
                    });

                    tasks.Add(t);
                }
                else
                {
                    Text = "Sgn Check: Wait for threads";

                    while (proc.Threads.Count > 12)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1000);
                    }
                }
            }

            Task.WaitAll(tasks.ToArray());

            listBox1.Items.Add($"Finished, Found {found} not signed assemblies.");
        }

        static bool IsAssemblyStrongNamed(Assembly assembly)
        {
            try
            {
                // Get the assembly name
                AssemblyName assemblyName = assembly.GetName();

                // Check if the public key is present
                return assemblyName.GetPublicKey().Length > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking assembly: {ex.Message}");
                return false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = listBox1.Text;
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
