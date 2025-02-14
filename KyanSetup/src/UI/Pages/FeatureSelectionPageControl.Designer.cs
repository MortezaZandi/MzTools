namespace KyanSetup
{
    partial class FeatureSelectionPageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkAllowFreeSelection = new System.Windows.Forms.CheckBox();
            this.lblRequiredSpaceOnDisk = new System.Windows.Forms.Label();
            this.lblFeatureDescription = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAllowFreeSelection
            // 
            this.chkAllowFreeSelection.AutoSize = true;
            this.chkAllowFreeSelection.Location = new System.Drawing.Point(16, 17);
            this.chkAllowFreeSelection.Name = "chkAllowFreeSelection";
            this.chkAllowFreeSelection.Size = new System.Drawing.Size(133, 18);
            this.chkAllowFreeSelection.TabIndex = 7;
            this.chkAllowFreeSelection.Text = "Allow free selection";
            this.chkAllowFreeSelection.UseVisualStyleBackColor = true;
            this.chkAllowFreeSelection.CheckedChanged += new System.EventHandler(this.chkAllowFreeSelection_CheckedChanged);
            // 
            // lblRequiredSpaceOnDisk
            // 
            this.lblRequiredSpaceOnDisk.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRequiredSpaceOnDisk.Location = new System.Drawing.Point(10, 342);
            this.lblRequiredSpaceOnDisk.Name = "lblRequiredSpaceOnDisk";
            this.lblRequiredSpaceOnDisk.Size = new System.Drawing.Size(279, 52);
            this.lblRequiredSpaceOnDisk.TabIndex = 6;
            // 
            // lblFeatureDescription
            // 
            this.lblFeatureDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFeatureDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFeatureDescription.Location = new System.Drawing.Point(10, 50);
            this.lblFeatureDescription.Name = "lblFeatureDescription";
            this.lblFeatureDescription.Size = new System.Drawing.Size(279, 292);
            this.lblFeatureDescription.TabIndex = 5;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(50, 50);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(347, 290);
            this.checkedListBox1.TabIndex = 4;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(50, 50, 10, 50);
            this.panel1.Size = new System.Drawing.Size(407, 433);
            this.panel1.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkAllowFreeSelection);
            this.panel3.Controls.Add(this.btnSelectAll);
            this.panel3.Controls.Add(this.btnUnselectAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(50, 340);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(347, 100);
            this.panel3.TabIndex = 11;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Location = new System.Drawing.Point(13, 54);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(111, 25);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnUnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselectAll.Location = new System.Drawing.Point(154, 54);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(111, 25);
            this.btnUnselectAll.TabIndex = 9;
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Features";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblRequiredSpaceOnDisk);
            this.panel2.Controls.Add(this.lblFeatureDescription);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(407, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 50, 25, 25);
            this.panel2.Size = new System.Drawing.Size(314, 433);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Features Description";
            // 
            // FeatureSelectionPageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FeatureSelectionPageControl";
            this.Size = new System.Drawing.Size(721, 433);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAllowFreeSelection;
        private System.Windows.Forms.Label lblRequiredSpaceOnDisk;
        private System.Windows.Forms.Label lblFeatureDescription;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Panel panel3;
    }
}
