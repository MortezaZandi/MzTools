using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFServer
{
    public partial class Form1 : Form
    {
        private readonly MZWCFService mZWCFService;

        public Form1()
        {
            InitializeComponent();

            this.mZWCFService = new MZWCFService();
            this.mZWCFService.OnRequest += MZWCFService_OnRequest;
        }

        private void MZWCFService_OnRequest(string message)
        {
            listBox1.Items.Add(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var serviceHost = new ServiceHost(this.mZWCFService);
                serviceHost.Open();

                MessageBox.Show("WCF Service has been started successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
