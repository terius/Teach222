using Helpers;
using Model;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class sms2 : UserControl
    {
        Brush blackBrush = Brushes.Blue;
        Font titleFont = new Font("微软雅黑", 10F, FontStyle.Bold, GraphicsUnit.Point, 134);
        Image imgTech = Resource1.主机端;
        Image imgStu = Resource1.客户端;
        //   string _message;//消息
        //  string _title; //标题
        bool _isMySelf;//是否为自己
        public bool IsMySelf { get { return _isMySelf; } }
        public readonly int SizeWidth = 420;
        Image headIcon;
        string _downloadFileUrl;
        string saveFilePath;
        PictureShow picShow;
        readonly int fullTextWidth = 307;
        Color backColor;
        int textHeight;
        readonly int bottomPadding = 10;

        ChatMessage _chatMessage;


        public sms2(ChatMessage messageInfo, bool isMySelf)
        {
            _chatMessage = messageInfo;
            //  _title = messageInfo.Title;
            //    _message = messageInfo.Message;
            _isMySelf = isMySelf;
            _downloadFileUrl = _chatMessage.DownloadFileUrl;
            InitializeComponent();
            textHeight = titleFont.Height;

            if (_chatMessage.MessageType == Common.MessageType.String)
            {
                this.txtSMS.Show();
                this.txtSMS.Text = _chatMessage.Message;
                this.pictureBox1.Hide();
            }
            else
            {
                SetPicLocation();
                this.pictureBox1.Show();
                this.pictureBox1.Cursor = Cursors.Hand;
                SetPictureBoxHover();
                this.pictureBox1.Click += PictureBox1_Click;
                switch (_chatMessage.MessageType)
                {

                    case Common.MessageType.Sound:

                        pictureBox1.Image = Resource1.声音;
                        break;
                    case Common.MessageType.Image:
                        CreateSmallPic();
                        //   pictureBox1.Image = Resource1.图片;
                        break;
                    case Common.MessageType.Video:
                        pictureBox1.Image = Resource1.视频;
                        break;
                    default:
                        break;
                }
                this.Width = 200;
                this.txtSMS.Hide();

                //  this.txtLink.Show();
                //  this.txtLink.LinkClicked += TxtLink_LinkClicked;
            }
            if (_chatMessage.UserType == Common.ClientRole.Student)
            {
                headIcon = imgStu;
            }
            else
            {
                headIcon = imgTech;
            }


        }

        private void CreateSmallPic()
        {
            ThreadPool.QueueUserWorkItem((ob) =>
            {
                Image img = null;
                if (!string.IsNullOrWhiteSpace(_chatMessage.LocalFilePath))
                {
                    img = Image.FromFile(_chatMessage.LocalFilePath);
                    //   ScreenCapture sc = new ScreenCapture();
                    var smallImg = FileHelper.ResizeImage(img, 32, 32);
                    //  smallImg.Save(  @"d:\" + DateTime.Now.Ticks.ToString() + ".png");
                    this.pictureBox1.Image = smallImg;
                }
                else
                {
                    GetSavePath();
                    Action<object, System.ComponentModel.AsyncCompletedEventArgs> onDownload
                      = (sender, eve) =>
                     {
                         if (eve.Error != null && !string.IsNullOrWhiteSpace(eve.Error.Message))
                         {
                             GlobalVariable.ShowError(eve.Error.Message);
                             return;
                         }
                         if (!string.IsNullOrWhiteSpace(saveFilePath))
                         {
                             img = Image.FromFile(saveFilePath);
                             //   ScreenCapture sc = new ScreenCapture();
                             var smallImg = FileHelper.ResizeImage(img, 32, 32);
                             //  smallImg.Save(  @"d:\" + DateTime.Now.Ticks.ToString() + ".png");
                             this.pictureBox1.Image = smallImg;
                         }

                     };
                    FileHelper.DownloadFile(_downloadFileUrl, onDownload, null, saveFilePath);
                }

                //var fileName =_downloadFileUrl.Substring(_downloadFileUrl.LastIndexOf("/") + 1);
                //var savePath = GlobalVariable.DownloadPath + "\\" + fileName;
                //if (File.Exists(savePath))
                //{
                //    img = Image.FromFile(savePath);
                //    ScreenCapture sc = new ScreenCapture();
                //    var smallImg = sc.getThumImage(img, 40, 5);
                //    this.pictureBox1.Image = smallImg;

                //}


            });
        }

        private void SetPicLocation()
        {
            if (!_isMySelf)
            {
                pictureBox1.Left = 50;
            }
        }

        private void SetPictureBoxHover()
        {
            this.pictureBox1.MouseEnter += (sender, e) => ((Control)sender).BackColor = Color.White;
            this.pictureBox1.MouseLeave += (sender, e) =>
            {
                ((Control)sender).BackColor = Color.Transparent;
            };

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


        private void GetSavePath()
        {
            if (string.IsNullOrWhiteSpace(saveFilePath))
            {
                var fileName = _downloadFileUrl.Substring(_downloadFileUrl.LastIndexOf("/") + 1);
                saveFilePath = GlobalVariable.DownloadPath + "\\" + fileName;
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {


            var fileType = GetFileType(_downloadFileUrl);
            GetSavePath();
            Action<object, System.ComponentModel.AsyncCompletedEventArgs> onDownload;
            Action<object, DownloadProgressChangedEventArgs> onProgress;

            if (fileType == Common.MessageType.Sound)
            {
                if (!string.IsNullOrWhiteSpace(saveFilePath))
                {
                    if (File.Exists(saveFilePath))
                    {
                        if (progressBar1.Visible)
                        {
                            progressBar1.Hide();
                        }
                        ((ChatForm)this.ParentForm).PlayVoice(saveFilePath);
                        return;
                    }
                }

                progressBar1.Show();
                onDownload = (ob, eve) =>
                {
                    progressBar1.Visible = false;
                    saveFilePath = eve.UserState.ToString();
                    if (!string.IsNullOrWhiteSpace(saveFilePath))
                    {
                        ((ChatForm)this.ParentForm).PlayVoice(saveFilePath);
                    }

                };

                onProgress = (ob, progress) =>
                {
                    var p = (int)(progress.BytesReceived * 100 / progress.TotalBytesToReceive);
                    progressBar1.Value = p;
                    Application.DoEvents();
                };

                FileHelper.DownloadFile(_downloadFileUrl, onDownload, onProgress, saveFilePath);


            }
            else
            {
              
                if (fileType == Common.MessageType.Image)
                {
                    if (File.Exists(saveFilePath))
                    {
                        if (progressBar1.Visible)
                        {
                            progressBar1.Hide();
                        }
                        ShowPic(saveFilePath);
                        return;
                    }
                }

                progressBar1.Show();
                onDownload = (ob, eve) =>
                {
                    progressBar1.Visible = false;
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

                onProgress = (ob, progress) =>
                {
                    var p = (int)(progress.BytesReceived * 100 / progress.TotalBytesToReceive);
                    progressBar1.Value = p;
                    Application.DoEvents();
                };
                FileHelper.DownloadFile(_downloadFileUrl, onDownload, onProgress, saveFilePath);

            }


        }

        private void ShowPic(string filePath)
        {
            progressBar1.Visible = false;
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
            int startY = textHeight;
            int height = this.Height - startY - bottomPadding;
            Rectangle rectArea;

            if (IsMySelf)
            {
                //标题
                g.DrawString(_chatMessage.Title, titleFont, blackBrush, new PointF(0, 0));
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
                g.DrawString(_chatMessage.Title, titleFont, blackBrush, new PointF(32 + 11, 0));
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

            if (txtSMS.Visible)
            {
                var textSize = this.CreateGraphics().MeasureString(_chatMessage.Message, txtSMS.Font);
                //    int messageHeight = txtSMS.Font.Height;
                int count = (int)Math.Floor(textSize.Width / fullTextWidth) + (textSize.Width % fullTextWidth == 0 ? 0 : 1);
                //    messageHeight = messageHeight * count + 5;
                this.Height = txtSMS.Font.Height * count + 5 + 10 * 2 + textHeight + bottomPadding;
            }
            else
            {
                this.Height = this.pictureBox1.Height + 10 * 2 + textHeight + bottomPadding;
            }

            if (IsMySelf)
            {
                this.Padding = new Padding(10, 30, 50, 20);
                backColor = Color.FromArgb(158, 234, 106);
                txtSMS.BackColor = Color.FromArgb(158, 234, 106);
            }
            else
            {
                this.Padding = new Padding(50, 30, 10, 20);
                backColor = txtSMS.BackColor = Color.FromArgb(245, 245, 245);
            }

            //   _sizeHeight = 20 + 17 * 2 + _messageHeight;
        }
    }
}
