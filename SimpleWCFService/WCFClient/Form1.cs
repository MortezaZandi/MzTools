using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient("http://localhost:2050/MZWCFService/CheckConnection");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var requestBodyJson = "{\r\n    \"Name\": \"" + textBox2.Text + "\"\r\n}";
                request.AddParameter("application/json", requestBodyJson, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                textBox1.Text = response.Content;
                textBox1.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                textBox1.Text = (ex.Message);
                textBox1.ForeColor = Color.Red;
            }
        }
    }
}
