namespace KyanSetup
{
    partial class DefaultConfigsPageControl
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clmConfigName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmConfigValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(145, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 25);
            this.button2.TabIndex = 12;
            this.button2.Text = "Delete Config";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(262, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnUnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselectAll.Location = new System.Drawing.Point(28, 194);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(111, 25);
            this.btnUnselectAll.TabIndex = 10;
            this.btnUnselectAll.Text = "Add Config";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmConfigName,
            this.clmConfigValue});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(25, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(716, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // clmConfigName
            // 
            this.clmConfigName.HeaderText = "ConfigName";
            this.clmConfigName.Name = "clmConfigName";
            this.clmConfigName.Width = 250;
            // 
            // clmConfigValue
            // 
            this.clmConfigValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmConfigValue.HeaderText = "Config Value";
            this.clmConfigValue.Name = "clmConfigValue";
            // 
            // DefaultConfigsPageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DefaultConfigsPageControl";
            this.Padding = new System.Windows.Forms.Padding(25);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmConfigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmConfigValue;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
