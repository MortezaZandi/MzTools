namespace MMF_IPC_Reader
{
    partial class ReadDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblActionC = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblActionB = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblActionA = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLastAction = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 253);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblActionC);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 20);
            this.panel4.TabIndex = 5;
            // 
            // lblActionC
            // 
            this.lblActionC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionC.Location = new System.Drawing.Point(0, 0);
            this.lblActionC.Name = "lblActionC";
            this.lblActionC.Size = new System.Drawing.Size(442, 20);
            this.lblActionC.TabIndex = 1;
            this.lblActionC.Text = "ActionC";
            this.lblActionC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblActionB);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 20);
            this.panel3.TabIndex = 4;
            // 
            // lblActionB
            // 
            this.lblActionB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionB.Location = new System.Drawing.Point(0, 0);
            this.lblActionB.Name = "lblActionB";
            this.lblActionB.Size = new System.Drawing.Size(442, 20);
            this.lblActionB.TabIndex = 1;
            this.lblActionB.Text = "ActionB";
            this.lblActionB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblActionA);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 20);
            this.panel2.TabIndex = 3;
            // 
            // lblActionA
            // 
            this.lblActionA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionA.Location = new System.Drawing.Point(0, 0);
            this.lblActionA.Name = "lblActionA";
            this.lblActionA.Size = new System.Drawing.Size(442, 20);
            this.lblActionA.TabIndex = 1;
            this.lblActionA.Text = "ActionA";
            this.lblActionA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLastAction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 20);
            this.panel1.TabIndex = 6;
            // 
            // lblLastAction
            // 
            this.lblLastAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastAction.Location = new System.Drawing.Point(0, 0);
            this.lblLastAction.Name = "lblLastAction";
            this.lblLastAction.Size = new System.Drawing.Size(442, 20);
            this.lblLastAction.TabIndex = 1;
            this.lblLastAction.Text = "LastAction";
            this.lblLastAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 253);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReadDialog";
            this.Text = "Reader";
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblActionA;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblActionC;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblActionB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLastAction;
    }
}

