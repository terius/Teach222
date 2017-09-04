using Common;
using Helpers;
using Model;
using MySocket;
using SharedForms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace NewTeacher
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string _userName, _displayName;
        private void Form2_Load(object sender, EventArgs e)
        {
            StartSocketClient();
          
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.labVer.Text = "版本：" + version;
            VaryQualityLevel();
        }

        private void AutoLogin()
        {
            //自动登录
            _userName = "Teacher001";
            string pwd = "123456";
            _displayName = "老师001";
            LoginIn(_userName, _displayName, pwd);
        }

        private void VaryQualityLevel()
        {
            // Get a bitmap. The using statement ensures objects  
            // are automatically disposed from memory after use.  
            using (Bitmap bmp1 = new Bitmap(@"C:\Users\Public\Pictures\Sample Pictures\Jellyfish.jpg"))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(@"c:\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);

                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(@"C:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

                // Save the bitmap as a JPG file with zero quality level compression.  
                myEncoderParameter = new EncoderParameter(myEncoder, 0L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(@"C:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void StartSocketClient()
        {
            Thread td = new Thread(() => {

                GlobalVariable.client = new MyClient(ProgramType.Teacher);
                GlobalVariable.client.OnClentIsConnecting += Client_OnClentIsConnecting;
                GlobalVariable.client.OnUserLoginRes = (message) =>
                {
                    var result = JsonHelper.DeserializeObj<LoginResult>(message.DataStr);
                    if (result.success)
                    {
                        this.InvokeOnUiThreadIfRequired(() => {
                            GlobalVariable.LoginUserInfo = new LoginUserInfo
                            {
                                DisplayName = _displayName,
                                UserName = _userName,
                                UserType = ClientRole.Teacher,
                                No = "999"
                            };
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        });

                    }
                    else
                    {
                        GlobalVariable.ShowError(result.msg);
                    }


                };
                AutoLogin();
            });
            td.IsBackground = true;
            td.Start();
        }

        private void Client_OnClentIsConnecting(object sender, EventArgs e)
        {
            MessageBox.Show("正在连接中，请稍后再试");
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)//登录 
        {
            _userName = this.textBox1.Text.Trim();
            _displayName = "教师888";
            string userPass = this.textBox2.Text.Trim();
            if (_userName == string.Empty)
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            if (userPass == string.Empty)
            {
                MessageBox.Show("请输入密码！");
                return;
            }
            LoginIn(_userName, _displayName, userPass);


        }

        private void LoginIn(string userName,string nickName,string password)
        {
            LoginRequest q = new LoginRequest
            {
                ClientRole = ClientRole.Teacher,
                nickName = nickName,
                password = password,
                userName = userName

            };
            GlobalVariable.SendCommand(q, CommandType.UserLogin);
        }






        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

