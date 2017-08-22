using Helpers;
using Model;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class sms2 : UserControl
    {
        Brush blackBrush = Brushes.Blue;
        Font titleFont = new Font("微软雅黑", 10F, FontStyle.Bold, GraphicsUnit.Point, 134);
        Image imgTech = Resource1.老师;
        Image imgStu = Resource1.学生;
        string _message;//消息
        string _title; //标题
        bool _isMySelf;//是否为自己
        public bool IsMySelf { get { return _isMySelf; } }
        public readonly int SizeWidth = 420;
        Image headIcon;
        string _downloadFileUrl;
        string saveFilePath;
        PictureShow picShow;
        readonly int fullTextWidth = 307;
        Color backColor;
        public sms2(ChatMessage messageInfo, bool isMySelf)
        {
            _title = messageInfo.Title;
            _message = messageInfo.Message;
            _isMySelf = isMySelf;
            _downloadFileUrl = messageInfo.DownloadFileUrl;
            InitializeComponent();
            if (messageInfo.MessageType == Common.MessageType.String)
            {
                this.txtSMS.Show();
                this.txtSMS.Text = _message;
                this.pictureBox1.Hide();
            }
            else
            {
                this.pictureBox1.Show();
                this.pictureBox1.Cursor = Cursors.Hand;
                this.pictureBox1.Click += PictureBox1_Click;
                switch (messageInfo.MessageType)
                {

                    case Common.MessageType.Sound:
                        pictureBox1.Image = Resource1.声音;
                        break;
                    case Common.MessageType.Image:
                        pictureBox1.Image = Resource1.图片;
                        break;
                    case Common.MessageType.Video:
                        pictureBox1.Image = Resource1.视频;
                        break;
                    default:
                        break;
                }
                //  this.txtSMS.Hide();

                //  this.txtLink.Show();
                //  this.txtLink.LinkClicked += TxtLink_LinkClicked;
            }
            if (messageInfo.UserType == Common.ClientRole.Student)
            {
                headIcon = imgStu;
            }
            else
            {
                headIcon = imgTech;
            }

            //  this.Width = 100;

        }

        private Common.MessageType GetFileType(string fileUrl)
        {
            string ext = fileUrl.Substring(fileUrl.LastIndexOf('.') + 1).ToLower();
            switch (ext)
            {
                case "png":
                case "jpg":
                case "bmp":
                case "ico":
                    return Common.MessageType.Image;
                case "mp3":
                case "wav":
                case "amr":
                    return Common.MessageType.Sound;
                case "mp4":
                case "avi":
                case "mpg":
                    return Common.MessageType.Video;
                default:
                    break;
            }
            return Common.MessageType.Image;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {


            var fileType = GetFileType(_downloadFileUrl);
            Action<object, System.ComponentModel.AsyncCompletedEventArgs> onDownload;
            if (fileType == Common.MessageType.Sound)
            {
                //ShowNotify("开始播放", 1000);
                //  PlayMp3FromUrl(_downloadFileUrl);
                var fileName = _downloadFileUrl.Substring(_downloadFileUrl.LastIndexOf("/") + 1);
                var savePath = GlobalVariable.DownloadPath + "\\" + fileName;
                if (!string.IsNullOrWhiteSpace(savePath))
                {
                    if (File.Exists(savePath))
                    {
                        ((ChatForm)this.ParentForm).PlayVoice(savePath);
                        return;
                    }
                }


                onDownload = (ob, eve) =>
                {
                    saveFilePath = eve.UserState.ToString();
                    if (!string.IsNullOrWhiteSpace(saveFilePath))
                    {
                        ((ChatForm)this.ParentForm).PlayVoice(saveFilePath);
                    }

                };

                FileHelper.DownloadFile(_downloadFileUrl, onDownload, savePath);


            }
            else
            {
                string savePath = "";
                if (fileType == Common.MessageType.Image)
                {
                    var fileName = _downloadFileUrl.Substring(_downloadFileUrl.LastIndexOf("/") + 1);
                    savePath = GlobalVariable.DownloadPath + "\\" + fileName;
                    if (File.Exists(savePath))
                    {
                        ShowPic(savePath);
                        return;
                    }
                }
                onDownload = (ob, eve) =>
                {
                    // MessageBox.Show("okoko");
                    saveFilePath = eve.UserState.ToString();
                    if (!string.IsNullOrWhiteSpace(saveFilePath))
                    {
                        if (fileType == Common.MessageType.Image)
                        {
                            ShowPic(saveFilePath);
                        }
                        //else
                        //{
                        //    ShowNotify("下载成功!文件已下载到\r\n" + saveFilePath);
                        //}
                    }

                };


                FileHelper.DownloadFile(_downloadFileUrl, onDownload, savePath);

            }


        }

        private void ShowPic(string filePath)
        {
            if (picShow == null || picShow.IsDisposed)
            {
                picShow = new PictureShow();
            }
            picShow.BringToFront();
            picShow.Show();
            picShow.ShowPic(filePath);
        }

        private void PlayMp3FromUrl(string url)
        {

            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        private void sms2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量 
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            int width = this.Width;
            int startY = 20;
            int height = this.Height - startY - 10;
            Rectangle rectArea;

            if (IsMySelf)
            {
                //标题
                g.DrawString(_title, titleFont, blackBrush, new PointF(0, 0));
                //边角椭圆
                g.FillRoundedRectangle(new SolidBrush(backColor), 0, startY, width - 10 - 32, height, 5);

                //三角形
                Point[] points2 = {
                new Point(width-11-32, height / 2 - 7 +startY),
                new Point(width-11-32, height / 2 + 7+ startY),
                new Point(width-32, height/2 + startY) };
                g.FillPolygon(new SolidBrush(backColor), points2);
                //人员头像
                rectArea = new Rectangle(width - 32, (height - 32) / 2 + startY, 32, 32);
                g.DrawImage(headIcon, rectArea);
            }
            else
            {
                //标题
                g.DrawString(_title, titleFont, blackBrush, new PointF(32 + 11, 0));
                //人员头像
                rectArea = new Rectangle(0, (height - 32) / 2 + startY, 32, 32);
                g.DrawImage(headIcon, rectArea);

                //三角形
                Point[] points2 = {
                new Point(32, height/2+ startY),
                new Point(32+11, height / 2 - 7+ startY),
                new Point(32+11, height / 2 + 7+ startY),
                };
                g.FillPolygon(new SolidBrush(backColor), points2);
                //边角椭圆
                g.FillRoundedRectangle(new SolidBrush(backColor), 32 + 11, startY, width - 10 - 32, height - 1, 5);


            }

        }

        private void sms2_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void sms2_Load(object sender, EventArgs e)
        {


            var textSize = this.CreateGraphics().MeasureString(_message, txtSMS.Font);
            int count = (int)Math.Floor(textSize.Width / fullTextWidth) + (textSize.Width % fullTextWidth == 0 ? 0 : 1);
            this.Height = count * 20 + 10 * 3 + 20 + 10;
            if (IsMySelf)
            {
                this.Padding = new Padding(10, 30, 50, 20);
                backColor = txtSMS.BackColor = Color.FromArgb(158, 234, 106);
            }
            else
            {
                this.Padding = new Padding(50, 30, 10, 20);
                backColor = txtSMS.BackColor = Color.White;
            }

            //   _sizeHeight = 20 + 17 * 2 + _messageHeight;
        }
    }
}
