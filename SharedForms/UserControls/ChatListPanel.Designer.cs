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
            this.panPrivate = new System.Windows.Forms.Panel();
            this.labPrivate = new System.Windows.Forms.Label();
            this.picPrivate = new System.Windows.Forms.PictureBox();
            this.panTeam_content = new System.Windows.Forms.Panel();
            this.panTeam = new System.Windows.Forms.Panel();
            this.labTeam = new System.Windows.Forms.Label();
            this.picTeam = new System.Windows.Forms.PictureBox();
            this.panGroup_content = new System.Windows.Forms.Panel();
            this.panGroup = new System.Windows.Forms.Panel();
            this.labGroup = new System.Windows.Forms.Label();
            this.picGroup = new System.Windows.Forms.PictureBox();
            this.panOut.SuspendLayout();
            this.panPrivate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPrivate)).BeginInit();
            this.panTeam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTeam)).BeginInit();
            this.panGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGroup)).BeginInit();
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
            this.panOut.Size = new System.Drawing.Size(368, 425);
            this.panOut.TabIndex = 1;
            // 
            // panPrivate_content
            // 
            this.panPrivate_content.AutoScroll = true;
            this.panPrivate_content.BackColor = System.Drawing.Color.White;
            this.panPrivate_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate_content.Location = new System.Drawing.Point(0, 212);
            this.panPrivate_content.Name = "panPrivate_content";
            this.panPrivate_content.Size = new System.Drawing.Size(368, 16);
            this.panPrivate_content.TabIndex = 5;
            this.panPrivate_content.Visible = false;
            // 
            // panPrivate
            // 
            this.panPrivate.BackColor = System.Drawing.Color.White;
            this.panPrivate.Controls.Add(this.labPrivate);
            this.panPrivate.Controls.Add(this.picPrivate);
            this.panPrivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panPrivate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPrivate.Location = new System.Drawing.Point(0, 152);
            this.panPrivate.Name = "panPrivate";
            this.panPrivate.Padding = new System.Windows.Forms.Padding(10);
            this.panPrivate.Size = new System.Drawing.Size(368, 60);
            this.panPrivate.TabIndex = 4;
            this.panPrivate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panBottom_MouseClick);
            // 
            // labPrivate
            // 
            this.labPrivate.BackColor = System.Drawing.Color.Transparent;
            this.labPrivate.Dock = System.Windows.Forms.DockStyle.Left;
            this.labPrivate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPrivate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.labPrivate.Location = new System.Drawing.Point(50, 10);
            this.labPrivate.Name = "labPrivate";
            this.labPrivate.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labPrivate.Size = new System.Drawing.Size(80, 40);
            this.labPrivate.TabIndex = 7;
            this.labPrivate.Text = "私聊";
            this.labPrivate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPrivate
            // 
            this.picPrivate.BackColor = System.Drawing.Color.Transparent;
            this.picPrivate.Dock = System.Windows.Forms.DockStyle.Left;
            this.picPrivate.Image = global::SharedForms.Resource1.私聊;
            this.picPrivate.Location = new System.Drawing.Point(10, 10);
            this.picPrivate.Name = "picPrivate";
            this.picPrivate.Size = new System.Drawing.Size(40, 40);
            this.picPrivate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPrivate.TabIndex = 6;
            this.picPrivate.TabStop = false;
            // 
            // panTeam_content
            // 
            this.panTeam_content.AutoScroll = true;
            this.panTeam_content.BackColor = System.Drawing.Color.White;
            this.panTeam_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam_content.Location = new System.Drawing.Point(0, 136);
            this.panTeam_content.Name = "panTeam_content";
            this.panTeam_content.Size = new System.Drawing.Size(368, 16);
            this.panTeam_content.TabIndex = 3;
            this.panTeam_content.Visible = false;
            // 
            // panTeam
            // 
            this.panTeam.BackColor = System.Drawing.Color.White;
            this.panTeam.Controls.Add(this.labTeam);
            this.panTeam.Controls.Add(this.picTeam);
            this.panTeam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panTeam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTeam.Location = new System.Drawing.Point(0, 76);
            this.panTeam.Name = "panTeam";
            this.panTeam.Padding = new System.Windows.Forms.Padding(10);
            this.panTeam.Size = new System.Drawing.Size(368, 60);
            this.panTeam.TabIndex = 2;
            this.panTeam.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panMiddle_MouseClick);
            // 
            // labTeam
            // 
            this.labTeam.BackColor = System.Drawing.Color.Transparent;
            this.labTeam.Dock = System.Windows.Forms.DockStyle.Left;
            this.labTeam.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTeam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.labTeam.Location = new System.Drawing.Point(50, 10);
            this.labTeam.Name = "labTeam";
            this.labTeam.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labTeam.Size = new System.Drawing.Size(80, 40);
            this.labTeam.TabIndex = 5;
            this.labTeam.Text = "群聊";
            this.labTeam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picTeam
            // 
            this.picTeam.BackColor = System.Drawing.Color.Transparent;
            this.picTeam.Dock = System.Windows.Forms.DockStyle.Left;
            this.picTeam.Image = global::SharedForms.Resource1.群聊;
            this.picTeam.Location = new System.Drawing.Point(10, 10);
            this.picTeam.Name = "picTeam";
            this.picTeam.Size = new System.Drawing.Size(40, 40);
            this.picTeam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTeam.TabIndex = 4;
            this.picTeam.TabStop = false;
            // 
            // panGroup_content
            // 
            this.panGroup_content.AutoScroll = true;
            this.panGroup_content.BackColor = System.Drawing.Color.White;
            this.panGroup_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup_content.Location = new System.Drawing.Point(0, 60);
            this.panGroup_content.Name = "panGroup_content";
            this.panGroup_content.Size = new System.Drawing.Size(368, 16);
            this.panGroup_content.TabIndex = 6;
            this.panGroup_content.Visible = false;
            // 
            // panGroup
            // 
            this.panGroup.BackColor = System.Drawing.Color.White;
            this.panGroup.Controls.Add(this.labGroup);
            this.panGroup.Controls.Add(this.picGroup);
            this.panGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panGroup.Location = new System.Drawing.Point(0, 0);
            this.panGroup.Name = "panGroup";
            this.panGroup.Padding = new System.Windows.Forms.Padding(10);
            this.panGroup.Size = new System.Drawing.Size(368, 60);
            this.panGroup.TabIndex = 0;
            this.panGroup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseClick);
            // 
            // labGroup
            // 
            this.labGroup.BackColor = System.Drawing.Color.Transparent;
            this.labGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.labGroup.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.labGroup.Location = new System.Drawing.Point(50, 10);
            this.labGroup.Name = "labGroup";
            this.labGroup.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labGroup.Size = new System.Drawing.Size(80, 40);
            this.labGroup.TabIndex = 4;
            this.labGroup.Text = "所有人";
            this.labGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picGroup
            // 
            this.picGroup.BackColor = System.Drawing.Color.Transparent;
            this.picGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.picGroup.Image = global::SharedForms.Resource1.所有人;
            this.picGroup.Location = new System.Drawing.Point(10, 10);
            this.picGroup.Name = "picGroup";
            this.picGroup.Size = new System.Drawing.Size(40, 40);
            this.picGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picGroup.TabIndex = 3;
            this.picGroup.TabStop = false;
            // 
            // ChatListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panOut);
            this.Name = "ChatListPanel";
            this.Size = new System.Drawing.Size(368, 425);
            this.Load += new System.EventHandler(this.ChatListPanel_Load);
            this.panOut.ResumeLayout(false);
            this.panPrivate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPrivate)).EndInit();
            this.panTeam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTeam)).EndInit();
            this.panGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panOut;
        private System.Windows.Forms.Panel panTeam_content;
        private System.Windows.Forms.Panel panTeam;
        private System.Windows.Forms.Panel panGroup;
        private System.Windows.Forms.Panel panPrivate_content;
        private System.Windows.Forms.Panel panPrivate;
        private System.Windows.Forms.PictureBox picTeam;
        private System.Windows.Forms.Label labGroup;
        private System.Windows.Forms.PictureBox picGroup;
        private System.Windows.Forms.Panel panGroup_content;
        private System.Windows.Forms.Label labTeam;
        private System.Windows.Forms.Label labPrivate;
        private System.Windows.Forms.PictureBox picPrivate;
    }
}
