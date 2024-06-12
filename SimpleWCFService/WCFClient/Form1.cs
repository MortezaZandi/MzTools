using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFServer.Requests;

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
            var req = new CheckConnection() { Name = textBox2.Text }.ToJson();

            textBox1.Text = Req<string>("CheckConnection", Method.POST, req);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var request = new WCFServer.Requests.FileListRequest();
            request.Path = textBox3.Text;
            var fileListResponse = Req<FileListResponse>("FileList", Method.POST, request.ToJson());

            listBox1.Items.Clear();
            textBox3.Text = fileListResponse.Path;
            foreach (var item in fileListResponse.Files)
            {
                listBox1.Items.Add(item);
            }
        }

        private T Req<T>(string method, Method mode, string body)
        {
            try
            {
                var client = new RestClient($"http://ks40:2050/MZWCFService/{method}");
                client.Timeout = -1;
                var request = new RestRequest(mode);
                request.AddHeader("Content-Type", "application/json");
                var requestBodyJson = body;
                request.AddParameter("application/json", requestBodyJson, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int position = 0;
            int totalDownloadedSize = 0;

            while (true)
            {

                var requst = new DownloadRequest();
                requst.Path = listBox1.Text;
                requst.Length = 10000;
                requst.Position = position;

                var response = Req<DownloadResponse>("Download", Method.POST, requst.ToJson());

                var savePath = Path.Combine(Path.GetDirectoryName(requst.Path), Path.GetFileNameWithoutExtension(requst.Path) + "~." + Path.GetExtension(requst.Path));

                int writeErrorCount = 0;
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(savePath, FileMode.Append))
                        {
                            fs.Write(response.Bytes, 0, response.Bytes.Length);
                        }
                    }
                    catch (Exception)
                    {
                        writeErrorCount++;
                    }

                    if (writeErrorCount > 9)
                    {
                        throw new ApplicationException("Write error");
                    }
                    else if (writeErrorCount > 0)
                    {
                        Thread.Sleep(100);
                    }
                }


                position += response.Length;

                totalDownloadedSize += response.Bytes.Length;
                this.Text = $"Download... %{((totalDownloadedSize / response.TotalLength) * 100):N1}";

                Application.DoEvents();

                if (response.Length == 0)
                {
                    MessageBox.Show("Download finished");
                    break;
                }
            }
        }

    }
}
