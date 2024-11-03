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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

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
        private Label label3;
        private Label label2;
        private NumericUpDown nmrSizeWidth;
        private NumericUpDown nmrSizeHeight;
        private RadioButton rdCircular;
        private RadioButton rdRectangular;
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
            this.rdCircular = new System.Windows.Forms.RadioButton();
            this.rdRectangular = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nmrSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.nmrSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.controlBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSizeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSizeHeight)).BeginInit();
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
            this.controlBox.Size = new System.Drawing.Size(444, 233);
            this.controlBox.TabIndex = 0;
            this.controlBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdCircular);
            this.panel1.Controls.Add(this.rdRectangular);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nmrSizeWidth);
            this.panel1.Controls.Add(this.nmrSizeHeight);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 185);
            this.panel1.TabIndex = 1;
            // 
            // rdCircular
            // 
            this.rdCircular.AutoSize = true;
            this.rdCircular.Checked = true;
            this.rdCircular.Location = new System.Drawing.Point(9, 77);
            this.rdCircular.Name = "rdCircular";
            this.rdCircular.Size = new System.Drawing.Size(60, 17);
            this.rdCircular.TabIndex = 1;
            this.rdCircular.TabStop = true;
            this.rdCircular.Text = "Circular";
            this.rdCircular.UseVisualStyleBackColor = true;
            // 
            // rdRectangular
            // 
            this.rdRectangular.AutoSize = true;
            this.rdRectangular.Location = new System.Drawing.Point(9, 100);
            this.rdRectangular.Name = "rdRectangular";
            this.rdRectangular.Size = new System.Drawing.Size(83, 17);
            this.rdRectangular.TabIndex = 2;
            this.rdRectangular.Text = "Rectangular";
            this.rdRectangular.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Spot Height:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Spot Width";
            // 
            // nmrSizeWidth
            // 
            this.nmrSizeWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.nmrSizeWidth.Location = new System.Drawing.Point(75, 15);
            this.nmrSizeWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmrSizeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrSizeWidth.Name = "nmrSizeWidth";
            this.nmrSizeWidth.Size = new System.Drawing.Size(112, 20);
            this.nmrSizeWidth.TabIndex = 5;
            this.nmrSizeWidth.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // nmrSizeHeight
            // 
            this.nmrSizeHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.nmrSizeHeight.Location = new System.Drawing.Point(75, 41);
            this.nmrSizeHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmrSizeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrSizeHeight.Name = "nmrSizeHeight";
            this.nmrSizeHeight.Size = new System.Drawing.Size(112, 20);
            this.nmrSizeHeight.TabIndex = 3;
            this.nmrSizeHeight.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(337, 143);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alt exit    |    Ctrl Dark View    |    Shift Clear View";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 233);
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
            ((System.ComponentModel.ISupportInitialize)(this.nmrSizeWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSizeHeight)).EndInit();
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
            int h = (int)nmrSizeHeight.Value;
            int w = (int)nmrSizeWidth.Value;

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

                    var str = label1.Text;

                    var sf = new StringFormat() { Alignment = StringAlignment.Center };

                    var sz = g.MeasureString(str, this.Font);

                    g.DrawString(str, this.Font, Brushes.Yellow, Screen.PrimaryScreen.WorkingArea.Width / 2, 1, sf);

                    g.DrawLine(Pens.Yellow, 0, sz.Height + 5, Screen.PrimaryScreen.WorkingArea.Width, sz.Height + 5);

                    if (rdRectangular.Checked)
                    {
                        g.FillRectangle(Brushes.White, new RectangleF(x - w / 2, y - h / 2, w, h));
                    }
                    else if (rdCircular.Checked)
                    {
                        g.FillEllipse(Brushes.White, new RectangleF(x - w / 2, y - h / 2, w, h));
                    }
                    else
                    {
                        throw new Exception("Shape type was not handled");
                    }
                }

                this.TopMost = true;
            }

            lastDrawX = x;
            lastDrawY = y;


            if (Control.ModifierKeys == Keys.Control)
            {
                this.Opacity = 0.5d;
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

    }
}
