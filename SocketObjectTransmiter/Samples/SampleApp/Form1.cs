using SocketObjectTransmiter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private ConnectionServer server;
        private async void btnStartServer_Click(object sender, EventArgs e)
        {
            // Server
            server = new ConnectionServer(port: 5000, isUdp: false, password: "secret");
            server.OnClientConnected += (s, client) =>
            {
                lstClients.Items.Add(client);
            };

            server.OnMessageReceived += (s, args) => stServerReceivedMessages.Items.Add(args.Message.Data);
            await server.StartAsync();

        }

        private async void btnStartClient_Click(object sender, EventArgs e)
        {
            // Client
            client = new ConnectionClient(port: 5000, isUdp: false, password: "secret");
            client.OnMessageReceived += (s, message) =>
            {
                lstClientReceivedMessages.Items.Add(message.Data);
            };
            await client.ConnectAsync("127.0.0.1");
            await client.SendAsync("Hello, Server!");
        }

        private ConnectionClient client;
        private async void btnSendServerMessage_Click(object sender, EventArgs e)
        {
            if (lstClients.SelectedItem != null)
            {
                var client = lstClients.SelectedItem as ConnectionHandler;
                await client.SendAsync(txtServerMessage.Text);
            }
            else
            {
                await server.BroadcastAsync(txtServerMessage.Text);
            }
            txtServerMessage.Clear();
        }

        private async void btnSendClientMessage_Click(object sender, EventArgs e)
        {
            await client.SendAsync(txtClientMessage.Text);
            txtClientMessage.Clear();
        }

        private void btnCloseServer_Click(object sender, EventArgs e)
        {
            server.Dispose();
        }

        private async void btnBroadcastServerMessage_Click(object sender, EventArgs e)
        {
            await server.BroadcastAsync(txtServerMessage.Text);
            txtServerMessage.Clear();
        }
    }

    public class ConnectionInfo
    {
        public ConnectionInfo(ConnectionClient client)
        {
            Client = client;
        }

        public ConnectionClient Client { get; set; }

        public override string ToString()
        {
            return Client.RemoteAddress;
        }
    }
}
