using NAudio.Wave;
using System;
using System.IO;

namespace SharedForms
{
    public class AudioRecorder
    {

        WaveIn sourceStream;
        WaveFileWriter waveWriter;
        readonly int InputDeviceIndex;

        public AudioRecorder()
        {
            this.InputDeviceIndex = GetMicDeviceId();
        }

        private int GetMicDeviceId()
        {
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                if (deviceInfo.ProductName.Contains("麦克风"))
                {
                    return waveInDevice;
                }
                //   Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
            }
            return 0;
        }



        public void StartRecording(string filePath)
        {
            sourceStream = new WaveIn();
            sourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(this.InputDeviceIndex).Channels);
            sourceStream.DataAvailable += this.SourceStreamDataAvailable;
         //   sourceStream.RecordingStopped += SourceStream_RecordingStopped;
            waveWriter = new WaveFileWriter(filePath, sourceStream.WaveFormat);

            sourceStream.StartRecording();

            //sourceStream = new WaveIn
            //{
            //    DeviceNumber = this.InputDeviceIndex,
            //    WaveFormat =
            //        new WaveFormat(44100, WaveIn.GetCapabilities(this.InputDeviceIndex).Channels)
            //};

            //sourceStream.DataAvailable += this.SourceStreamDataAvailable;

            //FileInfo fi = new FileInfo(filePath);
            //if (!Directory.Exists(fi.DirectoryName))
            //{
            //    Directory.CreateDirectory(fi.DirectoryName);
            //}

            //waveWriter = new WaveFileWriter(filePath, sourceStream.WaveFormat);
            //sourceStream.StartRecording();
        }

        private void SourceStream_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (sourceStream != null)
            {
                sourceStream.Dispose();
                sourceStream = null;
            }

            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
         
        }

        private void SourceStreamDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;
            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        public void EndRecord()
        {
            // sourceStream.StopRecording();


            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (this.waveWriter == null)
            {
                return;
            }
            this.waveWriter.Dispose();
            this.waveWriter = null;

        }
    }
}
