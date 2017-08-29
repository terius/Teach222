namespace SharedForms
{
    partial class ChatListPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panOut = new System.Windows.Forms.Panel();
            this.panPrivate_content = new System.Windows.Forms.Panel();
            this.panPrivate = new SharedForms.ChatTypePanel();
            this.panTeam_content = new System.Windows.Forms.Panel();
            this.panTeam = new SharedForms.ChatTypePanel();
            this.panGroup_content = new System.Windows.Forms.Panel();
            this.panGroup = new SharedForms.ChatTypePanel();
            this.panOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // panOut
            // 
            this.panOut.Controls.Add(this.panPrivate_content);
            this.panOut.Controls.Add(this.panPrivate);
            this.panOut.Controls.Add(this.panTeam_content);
            this.panOut.Controls.Add(this.panTeam);
            this.panOut.Controls.Add(this.panGroup_content);
            this.panOut.Controls.Add(this.panGroup);
            this.panOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panOut.Location = new System.Drawing.Point(0, 0);
            this.panOut.Name = "panOut";
            this.panOut.Size = new System.Drawing.Size(268, 425);
            this.panOut.TabIndex = 1;
            // 
            // panPrivate_content
            // 
            this.panPrivate_content.AutoScroll = true;
            this.panPrivate_content.BackColor = System.Drawing.Color.White;
            this.panPrivate_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate_content.Location = new System.Drawing.Point(0, 153);
            this.panPrivate_content.Name = "panPrivate_content";
            this.panPrivate_content.Size = new System.Drawing.Size(268, 0);
            this.panPrivate_content.TabIndex = 5;
            // 
            // panPrivate
            // 
            this.panPrivate.BackColor = System.Drawing.Color.White;
            this.panPrivate.ChatType = Common.ChatType.PrivateChat;
            this.panPrivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panPrivate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate.Forbit = false;
            this.panPrivate.IsSelected = false;
            this.panPrivate.Location = new System.Drawing.Point(0, 102);
            this.panPrivate.Margin = new System.Windows.Forms.Padding(2);
            this.panPrivate.Name = "panPrivate";
            this.panPrivate.Size = new System.Drawing.Size(268, 51);
            this.panPrivate.TabIndex = 9;
            // 
            // panTeam_content
            // 
            this.panTeam_content.AutoScroll = true;
            this.panTeam_content.BackColor = System.Drawing.Color.White;
            this.panTeam_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam_content.Location = new System.Drawing.Point(0, 102);
            this.panTeam_content.Name = "panTeam_content";
            this.panTeam_content.Size = new System.Drawing.Size(268, 0);
            this.panTeam_content.TabIndex = 3;
            // 
            // panTeam
            // 
            this.panTeam.BackColor = System.Drawing.Color.White;
            this.panTeam.ChatType = Common.ChatType.TeamChat;
            this.panTeam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panTeam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam.Forbit = false;
            this.panTeam.IsSelected = false;
            this.panTeam.Location = new System.Drawing.Point(0, 51);
            this.panTeam.Margin = new System.Windows.Forms.Padding(2);
            this.panTeam.Name = "panTeam";
            this.panTeam.Size = new System.Drawing.Size(268, 51);
            this.panTeam.TabIndex = 8;
            // 
            // panGroup_content
            // 
            this.panGroup_content.AutoScroll = true;
            this.panGroup_content.BackColor = System.Drawing.Color.White;
            this.panGroup_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup_content.Location = new System.Drawing.Point(0, 51);
            this.panGroup_content.Name = "panGroup_content";
            this.panGroup_content.Size = new System.Drawing.Size(268, 0);
            this.panGroup_content.TabIndex = 6;
            // 
            // panGroup
            // 
            this.panGroup.BackColor = System.Drawing.Color.White;
            this.panGroup.ChatType = Common.ChatType.GroupChat;
            this.panGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup.Forbit = false;
            this.panGroup.IsSelected = false;
            this.panGroup.Location = new System.Drawing.Point(0, 0);
            this.panGroup.Margin = new System.Windows.Forms.Padding(2);
            this.panGroup.Name = "panGroup";
            this.panGroup.Size = new System.Drawing.Size(268, 51);
            this.panGroup.TabIndex = 7;
            // 
            // ChatListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panOut);
            this.Name = "ChatListPanel";
            this.Size = new System.Drawing.Size(268, 425);
            this.Load += new System.EventHandler(this.ChatListPanel_Load);
            this.panOut.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panOut;
        private System.Windows.Forms.Panel panTeam_content;
        private System.Windows.Forms.Panel panPrivate_content;
        private System.Windows.Forms.Panel panGroup_content;
        private ChatTypePanel panGroup;
        private ChatTypePanel panTeam;
        private ChatTypePanel panPrivate;
    }
}
