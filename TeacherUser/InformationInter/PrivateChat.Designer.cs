﻿namespace TeacherUser.InformationInter
{
    partial class PrivateChat
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
            this.PrivateHistRtb = new System.Windows.Forms.RichTextBox();
            this.PrivateSendBtn = new System.Windows.Forms.Button();
            this.PrivateClearBtn = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.privateChatImageSendBtn = new System.Windows.Forms.Button();
            this.videoChat = new System.Windows.Forms.Button();
            this.reliefPrivateChatBtn = new System.Windows.Forms.Button();
            this.PrivateContentRtb = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrivateHistRtb
            // 
            this.PrivateHistRtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrivateHistRtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrivateHistRtb.Location = new System.Drawing.Point(0, 0);
            this.PrivateHistRtb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PrivateHistRtb.Name = "PrivateHistRtb";
            this.PrivateHistRtb.Size = new System.Drawing.Size(848, 309);
            this.PrivateHistRtb.TabIndex = 0;
            this.PrivateHistRtb.Text = "";
            // 
            // PrivateSendBtn
            // 
            this.PrivateSendBtn.Location = new System.Drawing.Point(284, 18);
            this.PrivateSendBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PrivateSendBtn.Name = "PrivateSendBtn";
            this.PrivateSendBtn.Size = new System.Drawing.Size(100, 29);
            this.PrivateSendBtn.TabIndex = 1;
            this.PrivateSendBtn.Text = "发送文字";
            this.PrivateSendBtn.UseVisualStyleBackColor = true;
            this.PrivateSendBtn.Click += new System.EventHandler(this.PrivateSendBtn_Click);
            // 
            // PrivateClearBtn
            // 
            this.PrivateClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrivateClearBtn.Location = new System.Drawing.Point(608, 18);
            this.PrivateClearBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PrivateClearBtn.Name = "PrivateClearBtn";
            this.PrivateClearBtn.Size = new System.Drawing.Size(100, 29);
            this.PrivateClearBtn.TabIndex = 2;
            this.PrivateClearBtn.Text = "清空";
            this.PrivateClearBtn.UseVisualStyleBackColor = true;
            this.PrivateClearBtn.Click += new System.EventHandler(this.PrivateClearBtn_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PrivateHistRtb);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PrivateContentRtb);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(848, 675);
            this.splitContainer1.SplitterDistance = 309;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // privateChatImageSendBtn
            // 
            this.privateChatImageSendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.privateChatImageSendBtn.Location = new System.Drawing.Point(392, 18);
            this.privateChatImageSendBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.privateChatImageSendBtn.Name = "privateChatImageSendBtn";
            this.privateChatImageSendBtn.Size = new System.Drawing.Size(100, 29);
            this.privateChatImageSendBtn.TabIndex = 8;
            this.privateChatImageSendBtn.Text = "发送图片";
            this.privateChatImageSendBtn.UseVisualStyleBackColor = true;
            this.privateChatImageSendBtn.Click += new System.EventHandler(this.privateChatImageSend_Click);
            // 
            // videoChat
            // 
            this.videoChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.videoChat.Location = new System.Drawing.Point(716, 18);
            this.videoChat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.videoChat.Name = "videoChat";
            this.videoChat.Size = new System.Drawing.Size(100, 29);
            this.videoChat.TabIndex = 7;
            this.videoChat.Text = "视频聊天";
            this.videoChat.UseVisualStyleBackColor = true;
            this.videoChat.Click += new System.EventHandler(this.videoChat_Click);
            // 
            // reliefPrivateChatBtn
            // 
            this.reliefPrivateChatBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reliefPrivateChatBtn.Location = new System.Drawing.Point(500, 18);
            this.reliefPrivateChatBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reliefPrivateChatBtn.Name = "reliefPrivateChatBtn";
            this.reliefPrivateChatBtn.Size = new System.Drawing.Size(100, 29);
            this.reliefPrivateChatBtn.TabIndex = 6;
            this.reliefPrivateChatBtn.Text = "解除禁止";
            this.reliefPrivateChatBtn.UseVisualStyleBackColor = true;
            this.reliefPrivateChatBtn.Click += new System.EventHandler(this.reliefPrivateChatBtn_Click);
            // 
            // PrivateContentRtb
            // 
            this.PrivateContentRtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrivateContentRtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrivateContentRtb.Location = new System.Drawing.Point(0, 0);
            this.PrivateContentRtb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PrivateContentRtb.Name = "PrivateContentRtb";
            this.PrivateContentRtb.Size = new System.Drawing.Size(848, 296);
            this.PrivateContentRtb.TabIndex = 5;
            this.PrivateContentRtb.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.videoChat);
            this.panel1.Controls.Add(this.privateChatImageSendBtn);
            this.panel1.Controls.Add(this.PrivateSendBtn);
            this.panel1.Controls.Add(this.PrivateClearBtn);
            this.panel1.Controls.Add(this.reliefPrivateChatBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 65);
            this.panel1.TabIndex = 9;
            // 
            // PrivateChat
            // 
            this.AcceptButton = this.PrivateSendBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 675);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PrivateChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrivateChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrivateChat_FormClosing);
            this.Load += new System.EventHandler(this.PrivateChat_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox PrivateHistRtb;
        private System.Windows.Forms.Button PrivateSendBtn;
        private System.Windows.Forms.Button PrivateClearBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox PrivateContentRtb;
        private System.Windows.Forms.Button reliefPrivateChatBtn;
        private System.Windows.Forms.Button videoChat;
        private System.Windows.Forms.Button privateChatImageSendBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
    }
}