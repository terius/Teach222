using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace vlctest
{
    public partial class NAudioForm : Form
    {
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        string audioFileName;
        public NAudioForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;

            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += WaveSource_DataAvailable;
            waveSource.RecordingStopped += WaveSource_RecordingStopped;
            audioFileName = @"c:\audios\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".wav";

            waveFile = new WaveFileWriter(audioFileName, waveSource.WaveFormat);

            waveSource.StartRecording();
        }

        private void WaveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
            ConvertToAmr();
            StartBtn.Enabled = true;
        }

        private void ConvertToAmr()
        {
            string amrFile = audioFileName.Substring(0, audioFileName.LastIndexOf('.') + 1) + "amr";
            string cmdString = "-y -i " + audioFileName + " -ar 8000 -ab 12.2k -ac 1 " + amrFile;
            Cmd(cmdString);
        }


        private void WaveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;

            waveSource.StopRecording();
        }

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
