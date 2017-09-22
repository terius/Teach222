
using Helper;
using Helpers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SharedForms
{
    public class RecordVoice
    {
        public const int MM_MCINOTIFY = 0x3B9;
        //  private Ffmpeg f = (Ffmpeg)null;
        // private AxWindowsMediaPlayer mediaPlayer = (AxWindowsMediaPlayer) null;
        //  private RecordVoice.playVoiceCallBackHandler voicePlayer;
        string _audioRecordPath = "Files\\AudioRecord\\";
        public RecordVoice()
        {
            // this.f = new Ffmpeg();
            //    this.mediaPlayer = player;
            //  this.voicePlayer = new RecordVoice.playVoiceCallBackHandler(this.playVoiceInvoke);
        }

        //[DllImport("winmm.dll", CharSet = CharSet.Auto)]
        //public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);



        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);


        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("winmm.dll")]
        private static extern long mciSendString(
          string command,
          StringBuilder returnValue,
          int returnLength,
          IntPtr winHandle);

        public void PlayVoice(string name, IntPtr formHandle)
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
                Play(newFile, formHandle);
                //StringBuilder shortpath = new StringBuilder(180);
                //int result = GetShortPathName(newFile, shortpath, shortpath.Capacity);
                //newFile = shortpath.ToString();
                //string buf = string.Empty;

                //mciSendString("play " + newFile, buf, buf.Length, 0); //播放
            }
            catch (Exception ex)
            {
                Loger.LogMessage(ex.ToString());
                throw ex;
            }
        }



        bool isOpen = false;
        string mediaName = "media";
        private void ClosePlayer()
        {
            if (isOpen)
            {
                String playCommand = "Close " + mediaName;
                mciSendString(playCommand, null, 0, IntPtr.Zero);
                isOpen = false;
            }
        }


        private void OpenMediaFile(string fileName)
        {
            ClosePlayer();
            string playCommand = "Open \"" + fileName + "\" type mpegvideo alias " + mediaName;
            mciSendString(playCommand, null, 0, IntPtr.Zero);
            isOpen = true;
        }


        private void PlayMediaFile(IntPtr formHandle)
        {
            if (isOpen)
            {
                string playCommand = "Play " + mediaName + " notify";
                mciSendString(playCommand, null, 0, formHandle);
            }
        }


        public void Play(string fileName,IntPtr formHandle)
        {
            OpenMediaFile(fileName);
            PlayMediaFile(formHandle);
        }



        private string ConvertAMRToMP3(string filename)
        {
            string mp3 = filename.Replace(".amr", ".mp3");
            string cmdString = "-y -i " + filename + " -ar 8000 -ab 12.2k -ac 1 " + mp3;
            Cmd(cmdString);
            return mp3;
        }


        public void BeginRecord2()
        {
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
        }

        public string StopRecord2()
        {
            var filename = DateTime.Now.Ticks.ToString();
            string wavFile = _audioRecordPath + filename + ".wav";
            mciSendString(@"save recsound " + wavFile, "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);
            string amrFile = _audioRecordPath + filename + ".amr";
            string cmdString = "-y -i " + wavFile + " -ar 8000 -ab 12.2k -ac 1 " + amrFile;
            Cmd(cmdString);
            return amrFile;
        }

        public void CancelRecord()
        {
            mciSendString("close recsound ", "", 0, 0);
        }

        //public void BeginRecord()
        //{
        //    mciSendString("set wave bitpersample 8", "", 0, 0);
        //    mciSendString("set wave samplespersec 20000", "", 0, 0);
        //    mciSendString("set wave channels 1", "", 0, 0);
        //    mciSendString("set wave format tag pcm", "", 0, 0);
        //    mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
        //    mciSendString("record movie", "", 0, 0);
        //}

        //public string StopRecord()
        //{
        //    var filename = DateTime.Now.Ticks.ToString();
        //    mciSendString("stop movie", "", 0, 0);
        //    // string name = this.generateName();
        //    string str1 = _audioRecordPath + filename + ".wav";
        //    mciSendString("save movie " + str1, "", 0, 0);
        //    mciSendString("close movie", "", 0, 0);
        //    string str2 = _audioRecordPath + filename + ".amr";
        //    string cmdString = "-y -i " + str1 + " -ar 8000 -ab 12.2k -ac 1 " + str2;
        //    Cmd(cmdString);
        //    return str2;
        //}

        /// <summary>
        /// 执行Cmd命令
        /// </summary>
        private void Cmd(string para)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo("ffmpeg.exe", para);
                process.StartInfo = processStartInfo;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                processStartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                //old
                //Process process = new Process();
                //process.StartInfo.FileName = "cmd.exe";
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.RedirectStandardInput = true;
                //process.Start();
                //process.StandardInput.WriteLine(c);
                //process.StandardInput.AutoFlush = true;
                //process.StandardInput.WriteLine("exit");
                //process.WaitForExit();

            }
            catch
            { }
        }


    }
}
