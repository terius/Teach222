namespace SharedForms.UserControls
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
            this.panBottom_content = new System.Windows.Forms.Panel();
            this.panBottom = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.picBottom = new System.Windows.Forms.PictureBox();
            this.panMiddle_content = new System.Windows.Forms.Panel();
            this.panMiddle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.picMiddle = new System.Windows.Forms.PictureBox();
            this.panTop_content = new System.Windows.Forms.Panel();
            this.panTop = new System.Windows.Forms.Panel();
            this.Chn_1 = new System.Windows.Forms.Label();
            this.picTop = new System.Windows.Forms.PictureBox();
            this.panOut.SuspendLayout();
            this.panBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBottom)).BeginInit();
            this.panMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMiddle)).BeginInit();
            this.panTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).BeginInit();
            this.SuspendLayout();
            // 
            // panOut
            // 
            this.panOut.Controls.Add(this.panBottom_content);
            this.panOut.Controls.Add(this.panBottom);
            this.panOut.Controls.Add(this.panMiddle_content);
            this.panOut.Controls.Add(this.panMiddle);
            this.panOut.Controls.Add(this.panTop_content);
            this.panOut.Controls.Add(this.panTop);
            this.panOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panOut.Location = new System.Drawing.Point(0, 0);
            this.panOut.Name = "panOut";
            this.panOut.Size = new System.Drawing.Size(368, 425);
            this.panOut.TabIndex = 1;
            // 
            // panBottom_content
            // 
            this.panBottom_content.AutoScroll = true;
            this.panBottom_content.BackColor = System.Drawing.Color.White;
            this.panBottom_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panBottom_content.Location = new System.Drawing.Point(0, 212);
            this.panBottom_content.Name = "panBottom_content";
            this.panBottom_content.Size = new System.Drawing.Size(368, 16);
            this.panBottom_content.TabIndex = 5;
            this.panBottom_content.Visible = false;
            // 
            // panBottom
            // 
            this.panBottom.BackColor = System.Drawing.Color.White;
            this.panBottom.Controls.Add(this.label3);
            this.panBottom.Controls.Add(this.picBottom);
            this.panBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panBottom.Location = new System.Drawing.Point(0, 152);
            this.panBottom.Name = "panBottom";
            this.panBottom.Padding = new System.Windows.Forms.Padding(10);
            this.panBottom.Size = new System.Drawing.Size(368, 60);
            this.panBottom.TabIndex = 4;
            this.panBottom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panBottom_MouseClick);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.label3.Location = new System.Drawing.Point(50, 10);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(80, 40);
            this.label3.TabIndex = 7;
            this.label3.Text = "私聊";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBottom
            // 
            this.picBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.picBottom.Image = global::SharedForms.Resource1.私聊;
            this.picBottom.Location = new System.Drawing.Point(10, 10);
            this.picBottom.Name = "picBottom";
            this.picBottom.Size = new System.Drawing.Size(40, 40);
            this.picBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBottom.TabIndex = 6;
            this.picBottom.TabStop = false;
            // 
            // panMiddle_content
            // 
            this.panMiddle_content.AutoScroll = true;
            this.panMiddle_content.BackColor = System.Drawing.Color.White;
            this.panMiddle_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMiddle_content.Location = new System.Drawing.Point(0, 136);
            this.panMiddle_content.Name = "panMiddle_content";
            this.panMiddle_content.Size = new System.Drawing.Size(368, 16);
            this.panMiddle_content.TabIndex = 3;
            this.panMiddle_content.Visible = false;
            // 
            // panMiddle
            // 
            this.panMiddle.BackColor = System.Drawing.Color.White;
            this.panMiddle.Controls.Add(this.label2);
            this.panMiddle.Controls.Add(this.picMiddle);
            this.panMiddle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panMiddle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMiddle.Location = new System.Drawing.Point(0, 76);
            this.panMiddle.Name = "panMiddle";
            this.panMiddle.Padding = new System.Windows.Forms.Padding(10);
            this.panMiddle.Size = new System.Drawing.Size(368, 60);
            this.panMiddle.TabIndex = 2;
            this.panMiddle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panMiddle_MouseClick);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.label2.Location = new System.Drawing.Point(50, 10);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(80, 40);
            this.label2.TabIndex = 5;
            this.label2.Text = "群聊";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picMiddle
            // 
            this.picMiddle.Dock = System.Windows.Forms.DockStyle.Left;
            this.picMiddle.Image = global::SharedForms.Resource1.群聊;
            this.picMiddle.Location = new System.Drawing.Point(10, 10);
            this.picMiddle.Name = "picMiddle";
            this.picMiddle.Size = new System.Drawing.Size(40, 40);
            this.picMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picMiddle.TabIndex = 4;
            this.picMiddle.TabStop = false;
            // 
            // panTop_content
            // 
            this.panTop_content.AutoScroll = true;
            this.panTop_content.BackColor = System.Drawing.Color.White;
            this.panTop_content.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop_content.Location = new System.Drawing.Point(0, 60);
            this.panTop_content.Name = "panTop_content";
            this.panTop_content.Size = new System.Drawing.Size(368, 16);
            this.panTop_content.TabIndex = 6;
            this.panTop_content.Visible = false;
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.White;
            this.panTop.Controls.Add(this.Chn_1);
            this.panTop.Controls.Add(this.picTop);
            this.panTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Padding = new System.Windows.Forms.Padding(10);
            this.panTop.Size = new System.Drawing.Size(368, 60);
            this.panTop.TabIndex = 0;
            this.panTop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseClick);
            // 
            // Chn_1
            // 
            this.Chn_1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Chn_1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Chn_1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.Chn_1.Location = new System.Drawing.Point(50, 10);
            this.Chn_1.Name = "Chn_1";
            this.Chn_1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Chn_1.Size = new System.Drawing.Size(80, 40);
            this.Chn_1.TabIndex = 4;
            this.Chn_1.Text = "所有人";
            this.Chn_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picTop
            // 
            this.picTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.picTop.Image = global::SharedForms.Resource1.所有人;
            this.picTop.Location = new System.Drawing.Point(10, 10);
            this.picTop.Name = "picTop";
            this.picTop.Size = new System.Drawing.Size(40, 40);
            this.picTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTop.TabIndex = 3;
            this.picTop.TabStop = false;
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
            this.panBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBottom)).EndInit();
            this.panMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMiddle)).EndInit();
            this.panTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panOut;
        private System.Windows.Forms.Panel panMiddle_content;
        private System.Windows.Forms.Panel panMiddle;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Panel panBottom_content;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.PictureBox picMiddle;
        private System.Windows.Forms.Label Chn_1;
        private System.Windows.Forms.PictureBox picTop;
        private System.Windows.Forms.Panel panTop_content;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBottom;
    }
}
