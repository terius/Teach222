namespace SharedForms
{
    partial class VideoShow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoShow));
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Size = new System.Drawing.Size(809, 504);
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(1, 31);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(809, 504);
            this.axVLCPlugin21.TabIndex = 0;
            // 
            // VideoShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 536);
            this.Controls.Add(this.axVLCPlugin21);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "VideoShow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "aaaa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoShow_FormClosing);
            this.Load += new System.EventHandler(this.VideoShow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VideoShow_KeyDown);
            this.Controls.SetChildIndex(this.panelContent, 0);
            this.Controls.SetChildIndex(this.axVLCPlugin21, 0);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
    }
}