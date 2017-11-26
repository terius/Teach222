using Declarations;
using Declarations.Media;
using Declarations.Players;
using Implementation;
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
    public partial class NVLCForm : Form
    {
        public NVLCForm()
        {
            InitializeComponent();
        }

        private void NVLCForm_Load(object sender, EventArgs e)
        {

        }

        public void PlayVideo(string url)
        {
            IMediaPlayerFactory factory = new MediaPlayerFactory();
            //:network-caching=300 :rtsp-caching=300
            IMedia media = factory.CreateMedia<IMedia>(url);
          //  IMedia media = factory.CreateMedia<IMedia>(url, new string[] { "network-caching=1000", "rtsp-caching=1000" });

            IVideoPlayer player = factory.CreatePlayer<IVideoPlayer>();
            player.WindowHandle = panel1.Handle;
            player.Open(media);
            player.Events.MediaEnded += Events_MediaEnded;
            player.Events.TimeChanged += Events_TimeChanged;
     //       player.AspectRatio = AspectRatioMode.Default;
            player.Play();

        }

        private void Events_TimeChanged(object sender, Declarations.Events.MediaPlayerTimeChanged e)
        {
            //  throw new NotImplementedException();
        }

        private void Events_MediaEnded(object sender, EventArgs e)
        {
            this.Text = "end" + DateTime.Now.Ticks.ToString();
            //  throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "rtsp://184.72.239.149/vod/mp4://BigBuckBunny_175k.mov";
            PlayVideo(url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "媒体文件 (*.jpg,*.gif,*.bmp,*.png,*.mp3,*.wav,*.amr,*.mp4,*.avi,*.mpg)|*.jpg;*.gif;*.bmp;*.png;*.mp3;*.wav;*.amr;*.mp4;*.avi;*.mpg";
            dlg.Title = "选择媒体文件";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlg.FileName;
                PlayVideo(fileName);
            }
        }

      
    }
}
