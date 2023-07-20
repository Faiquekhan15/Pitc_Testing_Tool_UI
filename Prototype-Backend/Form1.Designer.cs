namespace MDC_Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.btnClientService = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelClient = new System.Windows.Forms.Label();
            this.IPBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox_ConnectedClients = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Clear_Logs = new System.Windows.Forms.Button();
            this.serverPort = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(44, 18);
            this.start.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(112, 35);
            this.start.TabIndex = 0;
            this.start.Text = "Start Server";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(165, 18);
            this.Stop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(112, 35);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop Server";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // btnClientService
            // 
            this.btnClientService.Location = new System.Drawing.Point(44, 132);
            this.btnClientService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClientService.Name = "btnClientService";
            this.btnClientService.Size = new System.Drawing.Size(180, 35);
            this.btnClientService.TabIndex = 2;
            this.btnClientService.Text = "Begin Client Service";
            this.btnClientService.UseVisualStyleBackColor = true;
            this.btnClientService.Click += new System.EventHandler(this.btnClientService_Click);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(39, 63);
            this.labelServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(112, 20);
            this.labelServer.TabIndex = 3;
            this.labelServer.Text = "Server Started";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Service Started";
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Location = new System.Drawing.Point(202, 91);
            this.labelClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(74, 20);
            this.labelClient.TabIndex = 10;
            this.labelClient.Text = "-------------";
            // 
            // IPBox
            // 
            this.IPBox.FormattingEnabled = true;
            this.IPBox.Location = new System.Drawing.Point(388, 18);
            this.IPBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(160, 28);
            this.IPBox.TabIndex = 19;
            this.IPBox.Text = "209.150.146.236";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Server Port";
            // 
            // listBox_ConnectedClients
            // 
            this.listBox_ConnectedClients.FormattingEnabled = true;
            this.listBox_ConnectedClients.ItemHeight = 20;
            this.listBox_ConnectedClients.Location = new System.Drawing.Point(44, 205);
            this.listBox_ConnectedClients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_ConnectedClients.Name = "listBox_ConnectedClients";
            this.listBox_ConnectedClients.ScrollAlwaysVisible = true;
            this.listBox_ConnectedClients.Size = new System.Drawing.Size(1116, 344);
            this.listBox_ConnectedClients.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Static IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(411, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Meter Serial Number:";
            // 
            // Clear_Logs
            // 
            this.Clear_Logs.Location = new System.Drawing.Point(753, 123);
            this.Clear_Logs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Clear_Logs.Name = "Clear_Logs";
            this.Clear_Logs.Size = new System.Drawing.Size(112, 35);
            this.Clear_Logs.TabIndex = 28;
            this.Clear_Logs.Text = "Clear Logs";
            this.Clear_Logs.UseVisualStyleBackColor = true;
            this.Clear_Logs.Click += new System.EventHandler(this.Clear_Logs_Click);
            // 
            // serverPort
            // 
            this.serverPort.FormattingEnabled = true;
            this.serverPort.Items.AddRange(new object[] {
            "9001",
            "9002",
            "9003",
            "9004",
            "9005",
            "9006",
            "9007",
            "9008",
            "9009"});
            this.serverPort.Location = new System.Drawing.Point(659, 26);
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(121, 28);
            this.serverPort.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 569);
            this.Controls.Add(this.serverPort);
            this.Controls.Add(this.Clear_Logs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox_ConnectedClients);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.btnClientService);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Prototype Testing Collector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button btnClientService;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.ComboBox IPBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox_ConnectedClients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Clear_Logs;
        private System.Windows.Forms.ComboBox serverPort;
    }
}

