using MBase.Common.Definistions.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBase.UI.WinForm
{
    public partial class Form1 : Form
    {
        private readonly ISampleService sampleService;
        private readonly IFileService fileService;

        public Form1(ISampleService sampleService, IFileService fileService)
        {
            InitializeComponent();

            this.sampleService = sampleService;
            this.fileService = fileService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = @"C:\Tables.txt";

            var fileLine = fileService.ReadFirstLine(file);

            MessageBox.Show(this.sampleService.DecorateFileData(fileLine));
        }
    }
}
