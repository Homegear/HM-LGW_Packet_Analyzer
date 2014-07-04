namespace HMLGWPacketAnalyzer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtEncryptedData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDecryptedData1 = new System.Windows.Forms.TextBox();
            this.txtDecryptedData2 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Security Key:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(87, 6);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(256, 20);
            this.txtKey.TabIndex = 5;
            this.txtKey.Text = "5P4knCaKDd";
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
            // 
            // txtEncryptedData
            // 
            this.txtEncryptedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEncryptedData.Location = new System.Drawing.Point(0, 0);
            this.txtEncryptedData.MaxLength = 1000000;
            this.txtEncryptedData.Multiline = true;
            this.txtEncryptedData.Name = "txtEncryptedData";
            this.txtEncryptedData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncryptedData.Size = new System.Drawing.Size(701, 215);
            this.txtEncryptedData.TabIndex = 6;
            this.txtEncryptedData.Text = resources.GetString("txtEncryptedData.Text");
            this.txtEncryptedData.TextChanged += new System.EventHandler(this.txtEncryptedData_TextChanged);
            this.txtEncryptedData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(405, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data to decrypt (Wireshark TCP Stream C Array including initial handshake packets" +
    "):";
            // 
            // txtDecryptedData1
            // 
            this.txtDecryptedData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDecryptedData1.Location = new System.Drawing.Point(0, 0);
            this.txtDecryptedData1.Multiline = true;
            this.txtDecryptedData1.Name = "txtDecryptedData1";
            this.txtDecryptedData1.ReadOnly = true;
            this.txtDecryptedData1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDecryptedData1.Size = new System.Drawing.Size(701, 160);
            this.txtDecryptedData1.TabIndex = 8;
            this.txtDecryptedData1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
            // 
            // txtDecryptedData2
            // 
            this.txtDecryptedData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDecryptedData2.Location = new System.Drawing.Point(0, 0);
            this.txtDecryptedData2.Multiline = true;
            this.txtDecryptedData2.Name = "txtDecryptedData2";
            this.txtDecryptedData2.ReadOnly = true;
            this.txtDecryptedData2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDecryptedData2.Size = new System.Drawing.Size(701, 188);
            this.txtDecryptedData2.TabIndex = 13;
            this.txtDecryptedData2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 55);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtEncryptedData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(701, 571);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 14;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtDecryptedData1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtDecryptedData2);
            this.splitContainer2.Size = new System.Drawing.Size(701, 352);
            this.splitContainer2.SplitterDistance = 160;
            this.splitContainer2.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 638);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label3);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "HM-LGW Packet Analyzer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtEncryptedData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDecryptedData1;
        private System.Windows.Forms.TextBox txtDecryptedData2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}

