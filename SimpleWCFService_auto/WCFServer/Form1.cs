using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFServer.WCFExtra;

namespace WCFServer
{
    public partial class Form1 : Form
    {
        private readonly WCFServiceManager wcfServiceManager;

        public Form1()
        {
            InitializeComponent();
            WCFExtra.LoggingOperationInvoker.OnNewLog += MZWCFService_OnRequest;
            this.wcfServiceManager = new WCFServiceManager();
            this.wcfServiceManager.RegisterService(Environment.MachineName, nameof(MZWCFService), 2050, typeof(MZWCFService), typeof(IMZWCFService));
        }

        private void MZWCFService_OnRequest(string message)
        {
            listBox1.Items.Add(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             try
            {
                this.wcfServiceManager.StartAll();

                listBox1.Items.Add("WCF Service has been started successfully.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
        
        private Process client;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (client != null && !client.HasExited)
            {
                MessageBox.Show("Client already running.");
                return;
            }

            client = Process.Start("WCFClient.exe");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }
    }
}
