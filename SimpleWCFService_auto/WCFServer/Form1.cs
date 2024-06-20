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
        private readonly MZWCFService serviceClass;
        private readonly Type serviceInterface;
        private WebServiceHost host;

        public Form1()
        {
            InitializeComponent();

            this.serviceClass = new MZWCFService();
            this.serviceInterface = typeof(IMZWCFService);
            WCFExtra.LoggingOperationInvoker.OnNewLog += MZWCFService_OnRequest;
        }

        private void MZWCFService_OnRequest(string message)
        {
            listBox1.Items.Add(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var baseAddress = new Uri("http://ks40:2050/MZWCFService/");

                host = new WebServiceHost(serviceClass, baseAddress);

                AddDescriptionBehavior(host);

                AddServiceEndpoint(this.host, this.serviceInterface);

                //ServiceMetadataBehavior metadataBehavior;
                //metadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                //if (metadataBehavior == null)
                //{
                //    metadataBehavior = new ServiceMetadataBehavior();
                //    metadataBehavior.HttpGetEnabled = true;
                //    host.Description.Behaviors.Add(metadataBehavior);
                //}
                //else
                //{
                //    metadataBehavior.HttpGetEnabled = true;
                //}

                host.Authorization.ServiceAuthorizationManager = new WCFServiceAuthorizationManager();

                host.Open();

                listBox1.Items.Add("WCF Service has been started successfully.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private static void AddServiceEndpoint(ServiceHost host, Type serviceInterfaceType)
        {
            ServiceEndpoint restEndpoint = host.AddServiceEndpoint(serviceInterfaceType, new WebHttpBinding() { MaxReceivedMessageSize = 1073741824 }, "");
            WebHttpBehavior restWebHttpBehavior = new WebHttpBehavior();
            restWebHttpBehavior.HelpEnabled = true;
            restEndpoint.EndpointBehaviors.Add(restWebHttpBehavior);
        }

        private static void AddDescriptionBehavior(ServiceHost host)
        {
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            // if not found - add behavior with setting turned on
            if (debug == null)
            {
                host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
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
