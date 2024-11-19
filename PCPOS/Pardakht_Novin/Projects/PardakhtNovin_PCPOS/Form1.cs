using Intek.PcPosLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PardakhtNovin_PCPOS
{
    public partial class Form1 : Form
    {
        public int result { get; set; }

        private PCPOS pcpos;

        public Form1()
        {
            InitializeComponent();

            InitPCPOS();
        }

        private void InitPCPOS()
        {
            pcpos = new PCPOS();


            //Lan port:
            pcpos.ConnectionType = PCPOS.cnType.LAN;
            pcpos.Ip = "10.20.30.59";
            pcpos.Port = 1362;


            //Serial port:
            //string str = cmb_bdRate.Text;
            //if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
            //pcpos.baudRate = Int32.Parse(str);
            //pcpos.ConnectionType = PCPOS.cnType.SERIAL;
            //pcpos.ComPort = cmb_comport.Text;


            Log($"Pos start on {pcpos.Ip}:{pcpos.Port}");


            pcpos.GetResponse += new PCPOS.ResponseEventHandler(ResponseReady);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (!txtPaymentAmount.IsValidNumber())
            {
                MessageBox.Show("Invalid Amount");
            }

            pcpos.TerminalID = string.Empty;

            pcpos.Amount = txtPaymentAmount.Text;

            pcpos.PrCode = "000000";

            try
            {
                pcpos.send_transaction();
                Log("Response: " + pcpos.Request);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        void ResponseReady(string raw_response)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ResponseReady), new object[] { raw_response });
            }
            else
            {
                if (pcpos.Response.GetTrxnResp(false) == "00")
                {
                    lblPaymentStatus.Text = "Success";
                    lblPaymentStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblPaymentStatus.Text = "Error";
                    lblPaymentStatus.ForeColor = System.Drawing.Color.Red;
                }

                lblTransactionId.Text = pcpos.Response.GetTrxnSerial(false);

                Log(raw_response);
                Log(pcpos.Response.GetParsedResp(raw_response));
            }
        }

        private void Log(string log)
        {
            txtLogs.Text += $"{DateTime.Now:HH:mm:ss}:  {log}{Environment.NewLine}";
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (pcpos.TestConnection())
            {
                MessageBox.Show("Connection is successful");
            }
            else
            {
                MessageBox.Show("No Connection");
            }
        }
    }

    public static class Extensions
    {
        public static bool IsValidNumber(this TextBox textBox)
        {
            var amount = 0m;
            return decimal.TryParse(textBox.Text, out amount);
        }

        public static int Value(this TextBox textBox)
        {
            var amount = 0m;

            if (decimal.TryParse(textBox.Text, out amount))
            {
                return (int)amount;
            }
            else
            {
                return 0;
            }

        }
    }
}
