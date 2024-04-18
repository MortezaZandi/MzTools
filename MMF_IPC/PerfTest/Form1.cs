using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerfTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Do();
        }

        public static void Do()
        {
            string data = "this is a string";

            var a_result = RunDataManipulator(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(10000);
                    data = data.Replace("i", "I");
                });

                return data;
            });

            var b_result = RunDataManipulator(() =>
            {
                data = data.Replace("s", "S");
                return data;
            });
        }


        private static string RunDataManipulator(Func<string> action)
        {
            return action();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10000);
                MessageBox.Show("Helloooo");
            });

            await t;

            MessageBox.Show("hi");
        }

        private async Task ShowDelayedMessage()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(10000);
                MessageBox.Show("Helloooo");
            });
        }
    }
}
