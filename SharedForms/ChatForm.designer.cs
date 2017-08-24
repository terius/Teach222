namespace SharedForms
{
    partial class ChatForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChatNav = new SharedForms.ChatListPanel();
            this.smsPanelNew1 = new SharedForms.smsPanelNew();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBarControl1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.sendBox = new System.Windows.Forms.RichTextBox();
            this.panelControl3 = new System.Windows.Forms.Panel();
            this.labChatTitle = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.splitContainer1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ChatNav);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.smsPanelNew1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainer1.Size = new System.Drawing.Size(791, 591);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 0;
            // 
            // ChatNav
            // 
            this.ChatNav.BackColor = System.Drawing.Color.White;
            this.ChatNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatNav.Location = new System.Drawing.Point(0, 0);
            this.ChatNav.Name = "ChatNav";
            this.ChatNav.SelectedChatItem = null;
            this.ChatNav.Size = new System.Drawing.Size(236, 591);
            this.ChatNav.TabIndex = 0;
            // 
            // smsPanelNew1
            // 
            this.smsPanelNew1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.smsPanelNew1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsPanelNew1.Location = new System.Drawing.Point(0, 36);
            this.smsPanelNew1.Name = "smsPanelNew1";
            this.smsPanelNew1.Size = new System.Drawing.Size(551, 363);
            this.smsPanelNew1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.progressBarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 39);
            this.panel1.TabIndex = 2;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(371, 10);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(137, 23);
            this.progressBarControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.sendBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 438);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 153);
            this.panel2.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(450, 106);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            this.btnClose.TabIndex = 145;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(340, 106);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(87, 32);
            this.btnSend.TabIndex = 144;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // sendBox
            // 
            this.sendBox.BackColor = System.Drawing.Color.White;
            this.sendBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sendBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendBox.Location = new System.Drawing.Point(0, 0);
            this.sendBox.Name = "sendBox";
            this.sendBox.Size = new System.Drawing.Size(551, 153);
            this.sendBox.TabIndex = 143;
            this.sendBox.Text = "";
            this.sendBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendBox_KeyDown);
            // 
            // panelControl3
            // 
            this.panelControl3.BackColor = System.Drawing.Color.White;
            this.panelControl3.Controls.Add(this.labChatTitle);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(551, 36);
            this.panelControl3.TabIndex = 0;
            // 
            // labChatTitle
            // 
            this.labChatTitle.AutoSize = true;
            this.labChatTitle.Location = new System.Drawing.Point(99, 12);
            this.labChatTitle.Name = "labChatTitle";
            this.labChatTitle.Size = new System.Drawing.Size(41, 12);
            this.labChatTitle.TabIndex = 0;
            this.labChatTitle.Text = "label1";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 623);
            this.Name = "ChatForm";
            this.Text = "聊天窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.panelContent.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ChatListPanel ChatNav;
        private System.Windows.Forms.Panel panelControl3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox sendBox;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ProgressBar progressBarControl1;
        private System.Windows.Forms.Label labChatTitle;
        private smsPanelNew smsPanelNew1;
    }
}