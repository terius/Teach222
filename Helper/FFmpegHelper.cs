using Helpers;
using System;
using System.Diagnostics;
using System.Threading;

namespace Helper
{
    public class FFmpegHelper
    {
        // private Process myProcess = null;

        public static void Run(string para)
        {
            Process myProcess = null;
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
                    //    runDone.Set();
                    myProcess.WaitForExit();


                }).Start();
            }
            catch (Exception ex)
            {
                if (myProcess != null) myProcess.Dispose();
                throw ex;
            }
        }

        private static void MyProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if (e.Data != null)
            //{
            //    FileHelper.WriteLog(e.Data.ToString());
            //}
        }
    }
}
