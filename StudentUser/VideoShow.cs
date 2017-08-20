using System;
using System.Windows.Forms;

namespace StudentUser
{
    public partial class VideoShow : Form
    {
        private string param = ":network-caching=300 :start-time=300 :no-video-title-show";// ":aspect-ratio=4:3:network-caching=300:rtsp-caching=300";//:aspect-ratio=16:9
        public VideoShow()
        {
            InitializeComponent();

        }

        public void PlayVideo(string url)
        {
            var uri = new Uri(url);
            var convertedURI = uri.AbsoluteUri;
           
            axVLCPlugin21.playlist.add(convertedURI,"", param);
            axVLCPlugin21.playlist.play();

        }
    }
}
