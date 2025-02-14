using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KyanSetup.UI
{
    public partial class SplashScreen : Form
    {
        private static SplashScreen instance;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public static void ShowSplashScreen()
        {
            if (instance == null)
            {
                instance = new SplashScreen();
            }

            instance.Show();
        }

        public static void HideSplashScreen()
        {
            instance.Hide();
        }

        public static void UpdateStep(string v)
        {
            instance.lblStepName.Text = v;
            instance.Invalidate();
            Application.DoEvents();
        }
    }
}
