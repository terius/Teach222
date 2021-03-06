﻿

using AxAXVLC;
using System;
using System.Threading;

namespace StudentUser
{
    public class VLCPlus
    {
        private string param = ":network-caching=300:rtsp-caching=300:no-video-title-show";//:aspect-ratio=16:9
        public AxVLCPlugin2 axVLCPlugin2;
        private int Width;
        private int Height;
        private playVideoCallBackHandler rtspPlayer;
        public playFailedCallBackHandler playFailedHandler;

        public VLCPlus(AxVLCPlugin2 axVLCPlugin21)
        {
            this.axVLCPlugin2 = axVLCPlugin21;
            this.Width = axVLCPlugin21.Width;
            this.Height = axVLCPlugin21.Height;
            this.rtspPlayer = new playVideoCallBackHandler(this.playVideo);
        }

        public VLCPlus(AxVLCPlugin2 axVLCPlugin21, int width, int height)
        {
            this.axVLCPlugin2 = axVLCPlugin21;
            this.Width = width;
            this.Height = height;
            this.rtspPlayer = new playVideoCallBackHandler(this.startPlay);
        }

        public void startPlay(string rtsp)
        {
            this.axVLCPlugin2.Invoke((Delegate)this.rtspPlayer, (object)rtsp);
        }

        public void PlayVideo(string url)
        {
            var uri = new Uri(url);
            var convertedURI = uri.AbsoluteUri;
            axVLCPlugin2.playlist.add(convertedURI);
            axVLCPlugin2.playlist.play();
        }

        private void playVideo(string rtsp)
        {
            this.axVLCPlugin2.Width = this.Width;
            this.axVLCPlugin2.Height = this.Height;
            this.axVLCPlugin2.Toolbar = false;
            this.axVLCPlugin2.AllowDrop = false;
            this.axVLCPlugin2.playlist.items.clear();
            this.axVLCPlugin2.FullscreenEnabled = false;
            this.axVLCPlugin2.playlist.add(rtsp, (object)"", (object)this.param);
            try
            {
                this.axVLCPlugin2.playlist.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread.Sleep(1000);
            int num = 1;
            while (!this.axVLCPlugin2.playlist.isPlaying)
            {
                this.axVLCPlugin2.playlist.add(rtsp, (object)"", (object)this.param);
                this.axVLCPlugin2.playlist.next();
                this.axVLCPlugin2.playlist.items.clear();
                if (num == 100)
                {
                    this.playFailedHandler.BeginInvoke((AsyncCallback)null, (object)null);
                    this.dispose();
                    break;
                }
                ++num;
                Thread.Sleep(num * 1000);
            }
        }

        public void dispose()
        {
            if (this.axVLCPlugin2.playlist.isPlaying)
                this.axVLCPlugin2.playlist.stop();
            this.axVLCPlugin2.Dispose();
        }

        private delegate void playVideoCallBackHandler(string rtsp);

        public delegate void playFailedCallBackHandler();
    }
}
