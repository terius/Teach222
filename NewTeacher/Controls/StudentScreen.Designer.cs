namespace NewTeacher.Controls
{
    partial class StudentScreen
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
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.labName = new System.Windows.Forms.Label();
            this.labCallShow = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // picScreen
            // 
            this.picScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Margin = new System.Windows.Forms.Padding(0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(300, 180);
            this.picScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picScreen.TabIndex = 0;
            this.picScreen.TabStop = false;
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.labName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labName.ForeColor = System.Drawing.Color.White;
            this.labName.Location = new System.Drawing.Point(0, 180);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(300, 40);
            this.labName.TabIndex = 0;
            this.labName.Text = "label1";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCallShow
            // 
            this.labCallShow.AutoSize = true;
            this.labCallShow.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCallShow.ForeColor = System.Drawing.Color.MediumBlue;
            this.labCallShow.Location = new System.Drawing.Point(72, 79);
            this.labCallShow.Name = "labCallShow";
            this.labCallShow.Size = new System.Drawing.Size(138, 21);
            this.labCallShow.TabIndex = 2;
            this.labCallShow.Text = "已呼叫该客户端演示";
            this.labCallShow.Visible = false;
            // 
            // StudentScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labCallShow);
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.labName);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "StudentScreen";
            this.Size = new System.Drawing.Size(300, 220);
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labCallShow;
    }
}
