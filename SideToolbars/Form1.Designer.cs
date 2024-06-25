namespace SideToolbars
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrAutoExpand = new System.Windows.Forms.Timer(this.components);
            this.rightPanel = new SideToolbars.ToolbarPanel();
            this.leftPanel = new SideToolbars.ToolbarPanel();
            this.downPanel = new SideToolbars.ToolbarPanel();
            this.topPanel = new SideToolbars.ToolbarPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrAutoExpand
            // 
            this.tmrAutoExpand.Enabled = true;
            this.tmrAutoExpand.Interval = 500;
            this.tmrAutoExpand.Tick += new System.EventHandler(this.tmrAutoExpand_Tick);
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rightPanel.CollapseSize = 10;
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.ExpandSize = 100;
            this.rightPanel.Location = new System.Drawing.Point(688, 100);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(100, 262);
            this.rightPanel.TabIndex = 3;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.leftPanel.CollapseSize = 10;
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.ExpandSize = 100;
            this.leftPanel.Location = new System.Drawing.Point(0, 100);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(100, 262);
            this.leftPanel.TabIndex = 2;
            // 
            // downPanel
            // 
            this.downPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.downPanel.CollapseSize = 10;
            this.downPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.downPanel.ExpandSize = 100;
            this.downPanel.Location = new System.Drawing.Point(0, 362);
            this.downPanel.Name = "downPanel";
            this.downPanel.Size = new System.Drawing.Size(788, 100);
            this.downPanel.TabIndex = 1;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.CollapseSize = 10;
            this.topPanel.Controls.Add(this.textBox2);
            this.topPanel.Controls.Add(this.textBox1);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.button1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.ExpandSize = 100;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(788, 100);
            this.topPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(229, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(52, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(52, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 20);
            this.textBox2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(788, 462);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.downPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolbarPanel topPanel;
        private ToolbarPanel downPanel;
        private ToolbarPanel leftPanel;
        private ToolbarPanel rightPanel;
        private System.Windows.Forms.Timer tmrAutoExpand;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

