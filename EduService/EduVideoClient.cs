using DirectShowLib;
using Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EduService
{
    public class EduVideoClient
    {

        private Ffmpeg _ffmpeg = null;
        private string _rtspAddress = null;
        private bool isBeginScreenInteract = false;
        // private ClientConnectTcp _clientConnect;
        private string _serverIp;
        private string _ipSelf;
        private int _portSelf;
        string _fbl;//分辨率

        //  private int widthPixel;
        //  private int heightPixel;

        readonly string RTSPserverIP = System.Configuration.ConfigurationManager.AppSettings["RTSPserverIP"];
        string _videoFilePath;
        public EduVideoClient(string serverIP, string ipSelf, int portSelf)
        {
            _serverIp = serverIP;
            _ipSelf = ipSelf;
            _portSelf = portSelf;
        }

        public EduVideoClient(string videoFilePath)
        {
            _videoFilePath = videoFilePath;
        }


        public string beginScreenInteract(string fbl = null)
        {
            _fbl = fbl;
            if (isBeginScreenInteract)
            {
                stopScreenInteract();
            }
            this._rtspAddress = pushRtspStream(this._serverIp, this._ipSelf, this._portSelf);

            this.isBeginScreenInteract = true;
            return this._rtspAddress;
        }

        public string beginVideoInteract(string fbl = null)
        {
            _fbl = fbl;
            if (isBeginScreenInteract)
            {
                stopScreenInteract();
            }

            _rtspAddress = pushVideoByFFmpeg(_serverIp, _ipSelf, _portSelf);

            isBeginScreenInteract = true;
            return this._rtspAddress;
        }

        public void stopScreenInteract()
        {
            if (!this.isBeginScreenInteract)
                return;
            this._ffmpeg.dispose();
            this.isBeginScreenInteract = false;
        }

        private string GetMicName()
        {
            var capDevices = DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);

            foreach (var item in capDevices)
            {
                if (item.Name.Contains("麦克风"))
                {
                    return item.Name;
                }
            }
            return "";
        }

        private string GetResolution()
        {

            var resolution = Screen.PrimaryScreen.Bounds;
            return resolution.Width + "*" + resolution.Height;
        }

        private string GetVideoName()
        {
            var capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            StringBuilder sb = new StringBuilder();
            foreach (var item in capDevices)
            {
                Loger.LogMessage("找到摄像头：" + item.Name);
                return item.Name;
            }
            return "";
        }

        private string pushRtspStream(string ipServer, string ipSelf, int portSelf)
        {
            var para = GetFFMpegParaAndUrl(ipServer, ipSelf, portSelf);
            var mic = GetMicName();
            var url = "-f gdigrab -i desktop ";
            if (!string.IsNullOrWhiteSpace(mic))
                url += " -f dshow -i audio=\"" + mic + "\" -acodec mp2 -ab 128k";
            url += para[1];
            Loger.LogMessage("视频命令:" + url);
            this._ffmpeg = new Ffmpeg();
            //  this._ffmpeg.MessageReceived += _ffmpeg_MessageReceived;
            this._ffmpeg.beginExecute(url);
            _ffmpeg.WaitComplete();
            // var rtsp = "rtsp://" + ipServer + "/" + nameByIpPort + ".sdp";
            return para[0];
        }



        private string[] GetFFMpegParaAndUrl(string ipServer, string ipSelf, int portSelf)
        {
            if (!string.IsNullOrWhiteSpace(RTSPserverIP))
            {
                ipServer = RTSPserverIP;
            }
            string nameByIpPort = createNameByIpPort(ipSelf, portSelf);
            var rtspUrl = "rtsp://" + ipServer + "/" + nameByIpPort + ".sdp";
            var fbl = string.IsNullOrWhiteSpace(_fbl) ? "1280*720" : _fbl;
            string para = string.Format(" -r 25 -g 20 -s {0} -vcodec libx264 -x264opts bframes=3:b-adapt=0 -b:v 2000k -bufsize 2000k -threads 16 -preset:v ultrafast -tune:v zerolatency -f rtsp rtsp://{1}/{2}.sdp", fbl, ipServer, nameByIpPort);
            return new string[] { rtspUrl, para };
        }

        private string CreateFFmpegVideoRecordParam(bool getResolution = false)
        {
            string res = getResolution ? GetResolution() : "1024*768";
            string para = " -r 25 -g 20 -s " + res + " -vcodec libx264 -x264opts bframes=3:b-adapt=0 -b:v 2000k -bufsize 2000k -threads 16 -preset:v ultrafast -tune:v zerolatency ";
            return para;
        }

        private string CreateLocalVideoFFmpegParam()
        {
            string para = " -r 25 -g 20 -s 640*480 -vcodec libx264 -x264opts bframes=3:b-adapt=0 -b:v 2000k -bufsize 2000k -threads 16 -preset:v ultrafast -tune:v zerolatency ";

            return para;
        }

        //public void BeginRecordVideo(string filename)
        //{

        //    var video = GetVideoName();
        //    if (string.IsNullOrWhiteSpace(video))
        //    {
        //        throw new Exception("未找到摄像头");
        //    }
        //    var url = "-f dshow -i video=\"" + video + "\"";
        //    var mic = GetMicName();
        //    if (!string.IsNullOrWhiteSpace(mic))
        //    {
        //        url += ":audio=\"" + mic + "\" -acodec mp2 -ab 128k";
        //    }
        //    url += CreateFFmpegVideoRecordParam() + filename;
        //    Loger.LogMessage("录制视频命令:" + url);
        //    this._ffmpeg = new Ffmpeg();
        //    this._ffmpeg.beginExecute(url);
        //    _ffmpeg.WaitComplete();
        //}

        public void BeginRecordScreen(string filename)
        {

            var url = "-f gdigrab -i desktop ";
            var mic = GetMicName();
            if (!string.IsNullOrWhiteSpace(mic))
            {
                url += " -f dshow -i audio=\"" + mic + "\" -acodec mp2 -ab 128k";
            }
            url += CreateFFmpegVideoRecordParam(true) + filename;
            Loger.LogMessage("录制视频命令:" + url);
            this._ffmpeg = new Ffmpeg();
            this._ffmpeg.beginExecute(url);
            _ffmpeg.WaitComplete();
        }

        /// <summary>
        /// 开始录制本地摄像头视频
        /// </summary>
        public void BeginRecordLocalVideo()
        {
            var video = GetVideoName();
            if (string.IsNullOrWhiteSpace(video))
            {
                throw new Exception("未找到摄像头");
            }
            var url = "-f dshow -i video=\"" + video + "\"";
            var mic = GetMicName();
            if (!string.IsNullOrWhiteSpace(mic))
            {
                url += " -f dshow -i audio=\"" + mic + "\" -acodec mp2 -ab 128k";
            }
            url += " -r 25 -g 20 -s 1280*720 -vcodec libx264 -preset:v ultrafast -tune:v zerolatency " + _videoFilePath;
            Loger.LogMessage("录制视频命令:" + url);
            this._ffmpeg = new Ffmpeg();
            _ffmpeg.Cmd(url);
            //   this._ffmpeg.beginExecute(url);
            //   _ffmpeg.WaitComplete();
        }

        public void EndRecordVideo()
        {
            if (_ffmpeg != null)
            {

                this._ffmpeg.dispose();

            }
        }

        private string pushVideoByFFmpeg(string ipServer, string ipSelf, int portSelf)
        {
            var para = GetFFMpegParaAndUrl(ipServer, ipSelf, portSelf);
            var video = GetVideoName();
            if (string.IsNullOrWhiteSpace(video))
            {
                throw new Exception("未找到摄像头");
            }
            var url = "-f dshow -i video=\"" + video + "\"";
            var mic = GetMicName();
            if (!string.IsNullOrWhiteSpace(mic))
            {
                url += ":audio=\"" + mic + "\" -acodec mp2 -ab 128k";
            }
            url += para[1];
            Loger.LogMessage("视频命令:" + url);
            this._ffmpeg = new Ffmpeg();
            this._ffmpeg.beginExecute(url);
            _ffmpeg.WaitComplete();
            return para[0];
        }

        private string createNameByIpPort(string ip, int port)
        {
            string[] strArray = ip.Split('.');
            string str = "interact";
            if (strArray.Length > 0)
            {
                for (int index = 0; index < strArray.Length; ++index)
                    str += strArray[index];
            }
            return str + port.ToString();
        }

        public void KillAllFFmpeg()
        {
            Process killFfmpeg = new Process();
            ProcessStartInfo taskkillStartInfo = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM ffmpeg.exe",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            killFfmpeg.StartInfo = taskkillStartInfo;
            killFfmpeg.Start();
        }
    }


    public class Ffmpeg
    {
        private Process myProcess = null;
        ManualResetEvent runDone = new ManualResetEvent(false);
        public void beginExecute(string para)
        {
            try
            {
                new Thread(() =>
                {
                    //KillAllFFMPEG();
                    //Thread.Sleep(200);
                    myProcess = new Process();
                    ProcessStartInfo processStartInfo = new ProcessStartInfo("ffmpeg.exe", para);
                    myProcess.StartInfo = processStartInfo;
                    processStartInfo.UseShellExecute = false;
                    processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    processStartInfo.CreateNoWindow = true;
                    // processStartInfo.RedirectStandardOutput = true;
                    //  processStartInfo.RedirectStandardInput = true;
                    processStartInfo.RedirectStandardError = true;
                    myProcess.ErrorDataReceived += MyProcess_ErrorDataReceived;
                    //  myProcess.OutputDataReceived += MyProcess_OutputDataReceived;

                    myProcess.Start();
                    myProcess.BeginErrorReadLine();
                    //     myProcess.BeginOutputReadLine();
                    runDone.Set();
                    //  this.myProcess.WaitForExit();


                }).Start();
            }
            catch (Exception ex)
            {
                if (myProcess != null) myProcess.Dispose();
                throw ex;
            }
        }


        public void Cmd(string para)
        {
            try
            {
                new Thread(() =>
                {
                    myProcess = new Process();
                    ProcessStartInfo processStartInfo = new ProcessStartInfo("ffmpeg.exe", para);
                    myProcess.StartInfo = processStartInfo;
                    processStartInfo.UseShellExecute = false;//不使用系统外壳程序启动进程
                    processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    processStartInfo.CreateNoWindow = true;//不显示dos程序窗口
                    myProcess.Start();
                    myProcess.WaitForExit();
                    myProcess.Close();//关闭进程
                    myProcess.Dispose();//释放资源
                }).Start();

            }
            catch
            { }
        }

        public void WaitComplete()
        {
            runDone.WaitOne();
        }

        private void MyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if (e.Data != null)
            //{
            //    FileHelper.WriteLog(e.Data.ToString());
            //}

        }

        private void MyProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if (e.Data != null)
            //{
            //    FileHelper.WriteLog(e.Data.ToString());
            //}
        }

        public void dispose()
        {
            //   KillAllFFMPEG();
            if (this.myProcess.HasExited)
                return;
            this.myProcess.Refresh();
            this.myProcess.CloseMainWindow();
            this.myProcess.Kill();
            this.myProcess.Dispose();
            this.myProcess.Close();
        }



        public delegate void MessageReceivedEventHandler(string msg);
    }


}
