namespace vlctest
{
    partial class NAudioForm
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
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(58, 40);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(110, 70);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "录音";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(208, 40);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(110, 70);
            this.StopBtn.TabIndex = 1;
            this.StopBtn.Text = "停止";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // NAudioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 412);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Name = "NAudioForm";
            this.Text = "NAudioForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
    }
}