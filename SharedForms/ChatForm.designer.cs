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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChatNav = new SharedForms.ChatListPanel();
            this.panMessage = new System.Windows.Forms.Panel();
            this.smsPanelNew1 = new SharedForms.smsPanelNew();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolUploadPic = new System.Windows.Forms.ToolStripButton();
            this.toolRecordVoice = new System.Windows.Forms.ToolStripButton();
            this.labRecordVoice = new System.Windows.Forms.ToolStripLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
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
            this.panMessage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.splitContainer1);
            this.panelContent.Size = new System.Drawing.Size(1149, 694);
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
            this.splitContainer1.Panel2.Controls.Add(this.panMessage);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainer1.Size = new System.Drawing.Size(1149, 694);
            this.splitContainer1.SplitterDistance = 342;
            this.splitContainer1.TabIndex = 0;
            // 
            // ChatNav
            // 
            this.ChatNav.BackColor = System.Drawing.Color.White;
            this.ChatNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatNav.Location = new System.Drawing.Point(0, 0);
            this.ChatNav.Name = "ChatNav";
            this.ChatNav.SelectedChatItem = null;
            this.ChatNav.Size = new System.Drawing.Size(342, 694);
            this.ChatNav.TabIndex = 0;
            // 
            // panMessage
            // 
            this.panMessage.Controls.Add(this.smsPanelNew1);
            this.panMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMessage.Location = new System.Drawing.Point(0, 36);
            this.panMessage.Name = "panMessage";
            this.panMessage.Size = new System.Drawing.Size(803, 475);
            this.panMessage.TabIndex = 5;
            // 
            // smsPanelNew1
            // 
            this.smsPanelNew1.AutoScroll = true;
            this.smsPanelNew1.BackColor = System.Drawing.Color.White;
            this.smsPanelNew1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsPanelNew1.Location = new System.Drawing.Point(0, 0);
            this.smsPanelNew1.Name = "smsPanelNew1";
            this.smsPanelNew1.Size = new System.Drawing.Size(803, 475);
            this.smsPanelNew1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 30);
            this.panel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolUploadPic,
            this.toolRecordVoice,
            this.labRecordVoice,
            this.ProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(803, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolUploadPic
            // 
            this.toolUploadPic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolUploadPic.Image = global::SharedForms.Resource1.图片;
            this.toolUploadPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUploadPic.Name = "toolUploadPic";
            this.toolUploadPic.Size = new System.Drawing.Size(23, 27);
            this.toolUploadPic.Text = "toolStripButton1";
            this.toolUploadPic.ToolTipText = "上传图片";
            this.toolUploadPic.Click += new System.EventHandler(this.toolUploadPic_Click);
            // 
            // toolRecordVoice
            // 
            this.toolRecordVoice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRecordVoice.Image = global::SharedForms.Resource1.录音;
            this.toolRecordVoice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRecordVoice.Name = "toolRecordVoice";
            this.toolRecordVoice.Size = new System.Drawing.Size(23, 27);
            this.toolRecordVoice.Text = "toolStripButton1";
            this.toolRecordVoice.ToolTipText = "开始录音";
            this.toolRecordVoice.Click += new System.EventHandler(this.toolRecordVoice_Click);
            // 
            // labRecordVoice
            // 
            this.labRecordVoice.Name = "labRecordVoice";
            this.labRecordVoice.Size = new System.Drawing.Size(44, 27);
            this.labRecordVoice.Text = "录音中";
            this.labRecordVoice.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.Tomato;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Padding = new System.Windows.Forms.Padding(7);
            this.ProgressBar.Size = new System.Drawing.Size(114, 27);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.sendBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 541);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 153);
            this.panel2.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(702, 106);
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
            this.btnSend.Location = new System.Drawing.Point(592, 106);
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
            this.sendBox.Size = new System.Drawing.Size(803, 153);
            this.sendBox.TabIndex = 143;
            this.sendBox.Text = "";
            this.sendBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendBox_KeyDown);
            // 
            // panelControl3
            // 
            this.panelControl3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl3.Controls.Add(this.labChatTitle);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(803, 36);
            this.panelControl3.TabIndex = 0;
            // 
            // labChatTitle
            // 
            this.labChatTitle.AutoSize = true;
            this.labChatTitle.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labChatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.labChatTitle.Location = new System.Drawing.Point(24, 9);
            this.labChatTitle.Name = "labChatTitle";
            this.labChatTitle.Size = new System.Drawing.Size(51, 19);
            this.labChatTitle.TabIndex = 0;
            this.labChatTitle.Text = "label1";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 726);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Text = "聊天窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.panelContent.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panMessage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.Label labChatTitle;
        private smsPanelNew smsPanelNew1;
        private System.Windows.Forms.Panel panMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolUploadPic;
        private System.Windows.Forms.ToolStripButton toolRecordVoice;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripLabel labRecordVoice;
    }
}