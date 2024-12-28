namespace SampleApp
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
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStartClient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.stServerReceivedMessages = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstClientReceivedMessages = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerMessage = new System.Windows.Forms.TextBox();
            this.btnSendServerMessage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClientMessage = new System.Windows.Forms.TextBox();
            this.btnSendClientMessage = new System.Windows.Forms.Button();
            this.btnCloseClient = new System.Windows.Forms.Button();
            this.btnCloseServer = new System.Windows.Forms.Button();
            this.btnBroadcastServerMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(34, 33);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(151, 28);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStartClient
            // 
            this.btnStartClient.Location = new System.Drawing.Point(547, 75);
            this.btnStartClient.Name = "btnStartClient";
            this.btnStartClient.Size = new System.Drawing.Size(151, 28);
            this.btnStartClient.TabIndex = 1;
            this.btnStartClient.Text = "Start Client";
            this.btnStartClient.UseVisualStyleBackColor = true;
            this.btnStartClient.Click += new System.EventHandler(this.btnStartClient_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.Location = new System.Drawing.Point(34, 67);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(151, 95);
            this.lstClients.TabIndex = 3;
            // 
            // stServerReceivedMessages
            // 
            this.stServerReceivedMessages.FormattingEnabled = true;
            this.stServerReceivedMessages.Location = new System.Drawing.Point(34, 278);
            this.stServerReceivedMessages.Name = "stServerReceivedMessages";
            this.stServerReceivedMessages.Size = new System.Drawing.Size(181, 95);
            this.stServerReceivedMessages.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // lstClientReceivedMessages
            // 
            this.lstClientReceivedMessages.FormattingEnabled = true;
            this.lstClientReceivedMessages.Location = new System.Drawing.Point(547, 216);
            this.lstClientReceivedMessages.Name = "lstClientReceivedMessages";
            this.lstClientReceivedMessages.Size = new System.Drawing.Size(195, 147);
            this.lstClientReceivedMessages.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // txtServerMessage
            // 
            this.txtServerMessage.Location = new System.Drawing.Point(34, 232);
            this.txtServerMessage.Name = "txtServerMessage";
            this.txtServerMessage.Size = new System.Drawing.Size(100, 20);
            this.txtServerMessage.TabIndex = 8;
            // 
            // btnSendServerMessage
            // 
            this.btnSendServerMessage.Location = new System.Drawing.Point(140, 230);
            this.btnSendServerMessage.Name = "btnSendServerMessage";
            this.btnSendServerMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendServerMessage.TabIndex = 9;
            this.btnSendServerMessage.Text = "Send";
            this.btnSendServerMessage.UseVisualStyleBackColor = true;
            this.btnSendServerMessage.Click += new System.EventHandler(this.btnSendServerMessage_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            // 
            // txtClientMessage
            // 
            this.txtClientMessage.Location = new System.Drawing.Point(547, 155);
            this.txtClientMessage.Name = "txtClientMessage";
            this.txtClientMessage.Size = new System.Drawing.Size(100, 20);
            this.txtClientMessage.TabIndex = 11;
            // 
            // btnSendClientMessage
            // 
            this.btnSendClientMessage.Location = new System.Drawing.Point(667, 152);
            this.btnSendClientMessage.Name = "btnSendClientMessage";
            this.btnSendClientMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendClientMessage.TabIndex = 12;
            this.btnSendClientMessage.Text = "Send";
            this.btnSendClientMessage.UseVisualStyleBackColor = true;
            this.btnSendClientMessage.Click += new System.EventHandler(this.btnSendClientMessage_Click);
            // 
            // btnCloseClient
            // 
            this.btnCloseClient.Location = new System.Drawing.Point(704, 75);
            this.btnCloseClient.Name = "btnCloseClient";
            this.btnCloseClient.Size = new System.Drawing.Size(75, 23);
            this.btnCloseClient.TabIndex = 13;
            this.btnCloseClient.Text = "Close";
            this.btnCloseClient.UseVisualStyleBackColor = true;
            // 
            // btnCloseServer
            // 
            this.btnCloseServer.Location = new System.Drawing.Point(191, 38);
            this.btnCloseServer.Name = "btnCloseServer";
            this.btnCloseServer.Size = new System.Drawing.Size(75, 23);
            this.btnCloseServer.TabIndex = 14;
            this.btnCloseServer.Text = "Close";
            this.btnCloseServer.UseVisualStyleBackColor = true;
            this.btnCloseServer.Click += new System.EventHandler(this.btnCloseServer_Click);
            // 
            // btnBroadcastServerMessage
            // 
            this.btnBroadcastServerMessage.Location = new System.Drawing.Point(221, 232);
            this.btnBroadcastServerMessage.Name = "btnBroadcastServerMessage";
            this.btnBroadcastServerMessage.Size = new System.Drawing.Size(75, 23);
            this.btnBroadcastServerMessage.TabIndex = 15;
            this.btnBroadcastServerMessage.Text = "Broadcast";
            this.btnBroadcastServerMessage.UseVisualStyleBackColor = true;
            this.btnBroadcastServerMessage.Click += new System.EventHandler(this.btnBroadcastServerMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBroadcastServerMessage);
            this.Controls.Add(this.btnCloseServer);
            this.Controls.Add(this.btnCloseClient);
            this.Controls.Add(this.btnSendClientMessage);
            this.Controls.Add(this.txtClientMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSendServerMessage);
            this.Controls.Add(this.txtServerMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstClientReceivedMessages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stServerReceivedMessages);
            this.Controls.Add(this.lstClients);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartClient);
            this.Controls.Add(this.btnStartServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStartClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.ListBox stServerReceivedMessages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstClientReceivedMessages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerMessage;
        private System.Windows.Forms.Button btnSendServerMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClientMessage;
        private System.Windows.Forms.Button btnSendClientMessage;
        private System.Windows.Forms.Button btnCloseClient;
        private System.Windows.Forms.Button btnCloseServer;
        private System.Windows.Forms.Button btnBroadcastServerMessage;
    }
}

