using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHole
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer tmrUpdate;
        private int lastDrawX;
        private int lastDrawY;
        private IContainer components;
        private GroupBox controlBox;
        private Label label1;
        private Panel panel1;
        private TrackBar sizeBar;
        private Label lblSize;
        private Button btnStart;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && this.Opacity < 0.9d)
            {
                this.Opacity += 0.1d;
            }
            else if (e.Delta < 0 && this.Opacity > 0.1d)
            {
                this.Opacity -= 0.1d;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSize = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.sizeBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.controlBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 50;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // controlBox
            // 
            this.controlBox.Controls.Add(this.panel1);
            this.controlBox.Controls.Add(this.label1);
            this.controlBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlBox.Location = new System.Drawing.Point(0, 0);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(445, 171);
            this.controlBox.TabIndex = 0;
            this.controlBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSize);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.sizeBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 91);
            this.panel1.TabIndex = 1;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(18, 54);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(25, 13);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "250";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(337, 54);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // sizeBar
            // 
            this.sizeBar.LargeChange = 100;
            this.sizeBar.Location = new System.Drawing.Point(9, 8);
            this.sizeBar.Maximum = 1000;
            this.sizeBar.Minimum = 50;
            this.sizeBar.Name = "sizeBar";
            this.sizeBar.Size = new System.Drawing.Size(421, 45);
            this.sizeBar.SmallChange = 50;
            this.sizeBar.TabIndex = 1;
            this.sizeBar.TickFrequency = 50;
            this.sizeBar.Value = 250;
            this.sizeBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "Press Alt to switch on/off\r\nPress Ctrl to tiny lighter (%90)\r\nPress Shift to Clea" +
    "r (%100)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 171);
            this.Controls.Add(this.controlBox);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MouseHole";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.controlBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBar)).EndInit();
            this.ResumeLayout(false);

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            DrawCursor();
        }

        private void DrawCursor()
        {
            int x = MousePosition.X;
            int y = MousePosition.Y;
            int h = sizeBar.Value;
            int w = sizeBar.Value;

            bool inEditMode = controlBox.Visible == true;
            bool samePoint = lastDrawX == x && lastDrawY == y;
            bool shouldDraw = !samePoint && !inEditMode;

            if (shouldDraw)
            {
                using (var g = this.CreateGraphics())
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    var bg = Color.Black;
                    g.Clear(bg);
                    g.FillEllipse(Brushes.White, new RectangleF(x - w / 2, y - h / 2, w, h));
                }

                this.TopMost = true;
            }

            lastDrawX = x;
            lastDrawY = y;


            if (Control.ModifierKeys == Keys.Control)
            {
                this.Opacity = 0.9d;
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                this.Opacity = 0d;
            }
            else if (Control.ModifierKeys == Keys.Alt)
            {
                ToggleSwitch();
            }
            else if (this.Opacity != 1)
            {
                this.Opacity = 1;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ToggleSwitch();
        }

        private void ToggleSwitch()
        {
            tmrUpdate.Enabled = !tmrUpdate.Enabled;

            if (tmrUpdate.Enabled)
            {
                GoToPlayMode();
            }
            else
            {
                Invalidate();
                Thread.Sleep(100);

                GoToEditMode();
            }
        }

        private void GoToEditMode()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            controlBox.Show();
        }

        private void GoToPlayMode()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            controlBox.Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblSize.Text = sizeBar.Value.ToString();
        }
    }
}
