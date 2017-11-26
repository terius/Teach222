using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlctest
{
    public partial class TestForm : Form
    {
        VLCPlayer videoPlayer;
        string playUrl = @"D:\qh\0811.mp4";
        public TestForm()
        {
            InitializeComponent();
        }

        private void btnPlayVLCDotNet_Click(object sender, EventArgs e)
        {
            if (videoPlayer == null || videoPlayer.IsDisposed)
            {
                videoPlayer = new VLCPlayer();
            }
            videoPlayer.BringToFront();
            videoPlayer.Show();
            videoPlayer.StartPlayStream(playUrl);
        }

        private void StopPlayVLCDotNet_Click(object sender, EventArgs e)
        {
            if (videoPlayer != null)
            {
                videoPlayer.StopPlay();
            }
        }
    }
}
