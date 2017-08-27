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
            this.button1 = new System.Windows.Forms.Button();
            this.panOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // panOut
            // 
            this.panOut.Controls.Add(this.button1);
            this.panOut.Controls.Add(this.panPrivate_content);
            this.panOut.Controls.Add(this.panPrivate);
            this.panOut.Controls.Add(this.panTeam_content);
            this.panOut.Controls.Add(this.panTeam);
            this.panOut.Controls.Add(this.panGroup_content);
            this.panOut.Controls.Add(this.panGroup);
            this.panOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panOut.Location = new System.Drawing.Point(0, 0);
            this.panOut.Margin = new System.Windows.Forms.Padding(4);
            this.panOut.Name = "panOut";
            this.panOut.Size = new System.Drawing.Size(491, 531);
            this.panOut.TabIndex = 1;
            // 
            // panPrivate_content
            // 
            this.panPrivate_content.AutoScroll = true;
            this.panPrivate_content.BackColor = System.Drawing.Color.White;
            this.panPrivate_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate_content.Location = new System.Drawing.Point(0, 232);
            this.panPrivate_content.Margin = new System.Windows.Forms.Padding(4);
            this.panPrivate_content.Name = "panPrivate_content";
            this.panPrivate_content.Size = new System.Drawing.Size(491, 20);
            this.panPrivate_content.TabIndex = 5;
            this.panPrivate_content.Visible = false;
            // 
            // panPrivate
            // 
            this.panPrivate.BackColor = System.Drawing.Color.White;
            this.panPrivate.ChatType = Common.ChatType.PrivateChat;
            this.panPrivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panPrivate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate.Forbit = false;
            this.panPrivate.Location = new System.Drawing.Point(0, 168);
            this.panPrivate.Name = "panPrivate";
            this.panPrivate.Size = new System.Drawing.Size(491, 64);
            this.panPrivate.TabIndex = 9;
            // 
            // panTeam_content
            // 
            this.panTeam_content.AutoScroll = true;
            this.panTeam_content.BackColor = System.Drawing.Color.White;
            this.panTeam_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam_content.Location = new System.Drawing.Point(0, 148);
            this.panTeam_content.Margin = new System.Windows.Forms.Padding(4);
            this.panTeam_content.Name = "panTeam_content";
            this.panTeam_content.Size = new System.Drawing.Size(491, 20);
            this.panTeam_content.TabIndex = 3;
            this.panTeam_content.Visible = false;
            // 
            // panTeam
            // 
            this.panTeam.BackColor = System.Drawing.Color.White;
            this.panTeam.ChatType = Common.ChatType.TeamChat;
            this.panTeam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panTeam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam.Forbit = false;
            this.panTeam.Location = new System.Drawing.Point(0, 84);
            this.panTeam.Name = "panTeam";
            this.panTeam.Size = new System.Drawing.Size(491, 64);
            this.panTeam.TabIndex = 8;
            // 
            // panGroup_content
            // 
            this.panGroup_content.AutoScroll = true;
            this.panGroup_content.BackColor = System.Drawing.Color.White;
            this.panGroup_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup_content.Location = new System.Drawing.Point(0, 64);
            this.panGroup_content.Margin = new System.Windows.Forms.Padding(4);
            this.panGroup_content.Name = "panGroup_content";
            this.panGroup_content.Size = new System.Drawing.Size(491, 20);
            this.panGroup_content.TabIndex = 6;
            this.panGroup_content.Visible = false;
            // 
            // panGroup
            // 
            this.panGroup.BackColor = System.Drawing.Color.White;
            this.panGroup.ChatType = Common.ChatType.GroupChat;
            this.panGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup.Forbit = false;
            this.panGroup.Location = new System.Drawing.Point(0, 0);
            this.panGroup.Name = "panGroup";
            this.panGroup.Size = new System.Drawing.Size(491, 64);
            this.panGroup.TabIndex = 7;
            this.panGroup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panGroup_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChatListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panOut);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChatListPanel";
            this.Size = new System.Drawing.Size(491, 531);
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
        private System.Windows.Forms.Button button1;
    }
}
