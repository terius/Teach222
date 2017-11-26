namespace vlctest
{
    partial class TestForm
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
            this.btnPlayVLCDotNet = new System.Windows.Forms.Button();
            this.StopPlayVLCDotNet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlayVLCDotNet
            // 
            this.btnPlayVLCDotNet.Location = new System.Drawing.Point(6, 20);
            this.btnPlayVLCDotNet.Name = "btnPlayVLCDotNet";
            this.btnPlayVLCDotNet.Size = new System.Drawing.Size(119, 40);
            this.btnPlayVLCDotNet.TabIndex = 0;
            this.btnPlayVLCDotNet.Text = "播放VLCDotNet";
            this.btnPlayVLCDotNet.UseVisualStyleBackColor = true;
            this.btnPlayVLCDotNet.Click += new System.EventHandler(this.btnPlayVLCDotNet_Click);
            // 
            // StopPlayVLCDotNet
            // 
            this.StopPlayVLCDotNet.Location = new System.Drawing.Point(143, 20);
            this.StopPlayVLCDotNet.Name = "StopPlayVLCDotNet";
            this.StopPlayVLCDotNet.Size = new System.Drawing.Size(94, 40);
            this.StopPlayVLCDotNet.TabIndex = 1;
            this.StopPlayVLCDotNet.Text = "停止播放";
            this.StopPlayVLCDotNet.UseVisualStyleBackColor = true;
            this.StopPlayVLCDotNet.Click += new System.EventHandler(this.StopPlayVLCDotNet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPlayVLCDotNet);
            this.groupBox1.Controls.Add(this.StopPlayVLCDotNet);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 83);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VLCDotNet";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 313);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayVLCDotNet;
        private System.Windows.Forms.Button StopPlayVLCDotNet;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}