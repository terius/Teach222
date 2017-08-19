
using Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SharedForms
{
    public class RecordVoice
    {
        //  private Ffmpeg f = (Ffmpeg)null;
        // private AxWindowsMediaPlayer mediaPlayer = (AxWindowsMediaPlayer) null;
        //  private RecordVoice.playVoiceCallBackHandler voicePlayer;

        public RecordVoice()
        {
            // this.f = new Ffmpeg();
            //    this.mediaPlayer = player;
            //  this.voicePlayer = new RecordVoice.playVoiceCallBackHandler(this.playVoiceInvoke);
        }

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);



        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        public void PlayVoice(string name)
        {
            try
            {
                string newFile = name;
                FileInfo fi = new FileInfo(name);
                if (fi.Extension.ToLower() == ".amr")
                {
                    string mp3 = name.Substring(0, name.LastIndexOf('.') + 1) + "mp3";
                    if (File.Exists(mp3))
                    {
                        newFile = mp3;
                    }
                    else
                    {
                        newFile = ConvertAMRToMP3(name);
                    }
                }
                StringBuilder shortpath = new StringBuilder(80);
                int result = GetShortPathName(newFile, shortpath, shortpath.Capacity);
                newFile = shortpath.ToString();
                string buf = string.Empty;

                mciSendString("play " + newFile, buf, buf.Length, 0); //播放
            }
            catch (Exception ex)
            {
                Loger.LogMessage(ex.ToString());
                throw ex;
            }
        }

        private string ConvertAMRToMP3(string filename)
        {
            string mp3 = filename.Replace(".amr", ".mp3");
            string cmdString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe") + " -y -i " + filename + " -ar 8000 -ab 12.2k -ac 1 " + mp3;
            Cmd(cmdString);
            return mp3;
        }


        public void BeginRecord()
        {
            mciSendString("set wave bitpersample 8", "", 0, 0);
            mciSendString("set wave samplespersec 20000", "", 0, 0);
            mciSendString("set wave channels 1", "", 0, 0);
            mciSendString("set wave format tag pcm", "", 0, 0);
            mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
            mciSendString("record movie", "", 0, 0);
        }

        public string StopRecord()
        {
            var filename = DateTime.Now.Ticks.ToString();
            RecordVoice.mciSendString("stop movie", "", 0, 0);
            // string name = this.generateName();
            string str1 = "AudioRecord\\" + filename + ".wav";
            RecordVoice.mciSendString("save movie " + str1, "", 0, 0);
            RecordVoice.mciSendString("close movie", "", 0, 0);
            string str2 = "AudioRecord\\" + filename + ".amr";
            string cmdString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe") + " -y -i " + str1 + " -ar 8000 -ab 12.2k -ac 1 " + str2;
            Cmd(cmdString);
            return str2;
        }

        /// <summary>
        /// 执行Cmd命令
        /// </summary>
        private void Cmd(string c)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

                process.StandardInput.WriteLine(c);
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine("exit");

                StreamReader reader = process.StandardOutput;//截取输出流           

                process.WaitForExit();
            }
            catch
            { }
        }

        //public void playVoice(string voiceName)
        //{
        //  if (this.mediaPlayer == null)
        //    return;
        //  string[] strArray = voiceName.Split('.');
        //  string str1 = strArray[0];
        //  if (strArray.Length > 1 && !strArray[1].Equals("amr"))
        //  {
        //    this.voicePlayer("cache\\video\\" + voiceName);
        //  }
        //  else
        //  {
        //    string str2 = "cache\\video\\" + str1 + ".amr";
        //    string voice = "cache\\video\\" + str1 + ".mp3";
        //    this.f.execute(" -y -i " + str2 + " -ar 8000 -ab 12.2k -ac 1 " + voice);
        //    this.voicePlayer(voice);
        //  }
        //}

        //private void playVoiceInvoke(string voiceName)
        //{
        //  if (this.mediaPlayer == null)
        //    return;
        //  this.mediaPlayer.URL = voiceName;
        //  this.mediaPlayer.Ctlcontrols.play();
        //}

        //private string generateName()
        //{
        //    return DateTime.Now.ToString("MMddHHmmssff", (IFormatProvider)DateTimeFormatInfo.InvariantInfo);
        //}


    }
}
