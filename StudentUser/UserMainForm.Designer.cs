namespace StudentUser
{
    partial class UserMainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMainForm));
            this.tuopan = new System.Windows.Forms.NotifyIcon(this.components);
            this.MinMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mSignIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mChat = new System.Windows.Forms.ToolStripMenuItem();
            this.mHandUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mPrivateSMS = new System.Windows.Forms.ToolStripMenuItem();
            this.mFileShare = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mSetFBL = new System.Windows.Forms.ToolStripMenuItem();
            this.高清ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标清ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.流畅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tuopan
            // 
            this.tuopan.ContextMenuStrip = this.MinMenu;
            this.tuopan.Icon = ((System.Drawing.Icon)(resources.GetObject("tuopan.Icon")));
            this.tuopan.Visible = true;
            // 
            // MinMenu
            // 
            this.MinMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSignIn,
            this.mChat,
            this.mHandUp,
            this.mPrivateSMS,
            this.mFileShare,
            this.mSetFBL,
            this.toolStripSeparator1,
            this.mExit});
            this.MinMenu.Name = "MinMenu";
            this.MinMenu.Size = new System.Drawing.Size(161, 186);
            // 
            // mSignIn
            // 
            this.mSignIn.Image = global::StudentUser.Properties.Resources.签到;
            this.mSignIn.Name = "mSignIn";
            this.mSignIn.Size = new System.Drawing.Size(160, 22);
            this.mSignIn.Text = "签到";
            this.mSignIn.Click += new System.EventHandler(this.mSignIn_Click);
            // 
            // mChat
            // 
            this.mChat.Image = global::StudentUser.Properties.Resources.聊天;
            this.mChat.Name = "mChat";
            this.mChat.Size = new System.Drawing.Size(160, 22);
            this.mChat.Text = "聊天";
            this.mChat.Click += new System.EventHandler(this.mChat_Click);
            // 
            // mHandUp
            // 
            this.mHandUp.Image = global::StudentUser.Properties.Resources.举手;
            this.mHandUp.Name = "mHandUp";
            this.mHandUp.Size = new System.Drawing.Size(160, 22);
            this.mHandUp.Text = "举手";
            this.mHandUp.Click += new System.EventHandler(this.mHandUp_Click);
            // 
            // mPrivateSMS
            // 
            this.mPrivateSMS.Image = global::StudentUser.Properties.Resources.私信;
            this.mPrivateSMS.Name = "mPrivateSMS";
            this.mPrivateSMS.Size = new System.Drawing.Size(160, 22);
            this.mPrivateSMS.Text = "私信";
            this.mPrivateSMS.Click += new System.EventHandler(this.mPrivateSMS_Click);
            // 
            // mFileShare
            // 
            this.mFileShare.Image = global::StudentUser.Properties.Resources.文件共享;
            this.mFileShare.Name = "mFileShare";
            this.mFileShare.Size = new System.Drawing.Size(160, 22);
            this.mFileShare.Text = "文件共享";
            this.mFileShare.Click += new System.EventHandler(this.mFileShare_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // mExit
            // 
            this.mExit.Image = global::StudentUser.Properties.Resources.退出;
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(160, 22);
            this.mExit.Text = "退出";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // mSetFBL
            // 
            this.mSetFBL.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.高清ToolStripMenuItem,
            this.标清ToolStripMenuItem,
            this.流畅ToolStripMenuItem});
            this.mSetFBL.Name = "mSetFBL";
            this.mSetFBL.Size = new System.Drawing.Size(160, 22);
            this.mSetFBL.Text = "设置视频分辨率";
            // 
            // 高清ToolStripMenuItem
            // 
            this.高清ToolStripMenuItem.Name = "高清ToolStripMenuItem";
            this.高清ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.高清ToolStripMenuItem.Text = "高清";
            this.高清ToolStripMenuItem.Click += new System.EventHandler(this.高清ToolStripMenuItem_Click);
            // 
            // 标清ToolStripMenuItem
            // 
            this.标清ToolStripMenuItem.Name = "标清ToolStripMenuItem";
            this.标清ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.标清ToolStripMenuItem.Text = "标清";
            this.标清ToolStripMenuItem.Click += new System.EventHandler(this.标清ToolStripMenuItem_Click);
            // 
            // 流畅ToolStripMenuItem
            // 
            this.流畅ToolStripMenuItem.Name = "流畅ToolStripMenuItem";
            this.流畅ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.流畅ToolStripMenuItem.Text = "流畅";
            this.流畅ToolStripMenuItem.Click += new System.EventHandler(this.流畅ToolStripMenuItem_Click);
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 405);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(379, 353);
            this.Name = "UserMainForm";
            this.Text = "Student";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserMainForm_FormClosing);
            this.Load += new System.EventHandler(this.UserMainForm_Load);
            this.MinMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon tuopan;
     
       
        private System.Windows.Forms.ContextMenuStrip MinMenu;
        private System.Windows.Forms.ToolStripMenuItem mSignIn;
        private System.Windows.Forms.ToolStripMenuItem mChat;
        private System.Windows.Forms.ToolStripMenuItem mHandUp;
        private System.Windows.Forms.ToolStripMenuItem mPrivateSMS;
        private System.Windows.Forms.ToolStripMenuItem mFileShare;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mSetFBL;
        private System.Windows.Forms.ToolStripMenuItem 高清ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标清ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 流畅ToolStripMenuItem;
    }
}

