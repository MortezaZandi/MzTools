﻿namespace MMF_IPC
{
    partial class WriteDialog
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
            this.btnActionA = new System.Windows.Forms.Button();
            this.btnActionB = new System.Windows.Forms.Button();
            this.btnActionC = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lnkOpenReader = new System.Windows.Forms.LinkLabel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnActionA
            // 
            this.btnActionA.Location = new System.Drawing.Point(24, 12);
            this.btnActionA.Name = "btnActionA";
            this.btnActionA.Size = new System.Drawing.Size(154, 27);
            this.btnActionA.TabIndex = 0;
            this.btnActionA.Text = "Action A";
            this.btnActionA.UseVisualStyleBackColor = true;
            this.btnActionA.Click += new System.EventHandler(this.btnActionA_Click);
            // 
            // btnActionB
            // 
            this.btnActionB.Location = new System.Drawing.Point(24, 41);
            this.btnActionB.Name = "btnActionB";
            this.btnActionB.Size = new System.Drawing.Size(154, 27);
            this.btnActionB.TabIndex = 1;
            this.btnActionB.Text = "Action B";
            this.btnActionB.UseVisualStyleBackColor = true;
            this.btnActionB.Click += new System.EventHandler(this.btnActionB_Click);
            // 
            // btnActionC
            // 
            this.btnActionC.Location = new System.Drawing.Point(24, 70);
            this.btnActionC.Name = "btnActionC";
            this.btnActionC.Size = new System.Drawing.Size(154, 27);
            this.btnActionC.TabIndex = 2;
            this.btnActionC.Text = "Action C";
            this.btnActionC.UseVisualStyleBackColor = true;
            this.btnActionC.Click += new System.EventHandler(this.btnActionC_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(24, 103);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "OPT1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lnkOpenReader
            // 
            this.lnkOpenReader.AutoSize = true;
            this.lnkOpenReader.Location = new System.Drawing.Point(123, 144);
            this.lnkOpenReader.Name = "lnkOpenReader";
            this.lnkOpenReader.Size = new System.Drawing.Size(71, 13);
            this.lnkOpenReader.TabIndex = 4;
            this.lnkOpenReader.TabStop = true;
            this.lnkOpenReader.Text = "Open Reader";
            this.lnkOpenReader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenReader_LinkClicked);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 173);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(188, 199);
            this.listBox1.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(24, 126);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(89, 17);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "Rapid Report";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // WriteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 387);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lnkOpenReader);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnActionC);
            this.Controls.Add(this.btnActionB);
            this.Controls.Add(this.btnActionA);
            this.Name = "WriteDialog";
            this.Text = "Sender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnActionA;
        private System.Windows.Forms.Button btnActionB;
        private System.Windows.Forms.Button btnActionC;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.LinkLabel lnkOpenReader;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

