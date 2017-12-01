using Common;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class VideoShow : MyForm
    {
        private string param = ":network-caching=300 :rtsp-caching=300";// ":aspect-ratio=4:3:network-caching=300:rtsp-caching=300";//:aspect-ratio=16:9
        ProgramType _type;
        string _userName;
        public VideoShow(ProgramType type, string userName)
        {
            _type = type;
            _userName = userName;
            InitializeComponent();
            //if (_type == ProgramType.Student)
            //{
            //    // this.WindowState = FormWindowState.Maximized;
            //    //  this.TopMost = true;
            
            //}
            //else
            //{
            //    FormBorderStyle = FormBorderStyle.Sizable;
            //}
            axVLCPlugin21.video.aspectRatio = GetScreenAspectRatio();
        

        }

      


        public void PlayVideo(string url)
        {
            var uri = new Uri(url);
            
            var convertedURI = uri.AbsoluteUri;
            axVLCPlugin21.playlist.items.clear();
            axVLCPlugin21.playlist.add(convertedURI);
       
            //Thread.Sleep(1000);
            try
            {
                axVLCPlugin21.playlist.play();
            }
            catch (Exception ex)
            {
                throw ex;
            }

          

        }

        private void VideoShow_Load(object sender, EventArgs e)
        {
          //  label1.Text = _userName;
            //  axVLCPlugin21.Toolbar = false;
            // axVLCPlugin21.AllowDrop = false;
            //  this.FormBorderStyle = FormBorderStyle.Sizable;
            // this.axVLCPlugin21.FullscreenEnabled = false;
   
            //  var url = @"E:\1.mp4";
            //  PlayVideo(url);
        }

        private void VideoShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_userName))
            {
                GlobalVariable.client.Send_StopStudentShow(_userName);
            }
            if (this.axVLCPlugin21.playlist.isPlaying)
                this.axVLCPlugin21.playlist.stop();
            this.axVLCPlugin21.Dispose();
        }

        private string GetScreenAspectRatio()
        {
            int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            int deskWidth = Screen.PrimaryScreen.Bounds.Width;

            int gcd = GCD(deskWidth, deskHeight);

            return (deskWidth / gcd) + ":" + (deskHeight / gcd);
        }

        private int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private void VideoShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (_type == ProgramType.Teacher)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }
            }
        }
    }
}
