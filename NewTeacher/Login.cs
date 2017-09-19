using Common;
using EduService;
using Helpers;
using Model;
using SharedForms;
using System;
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
            if (GlobalVariable.IsHuiShenXiTong)
            {
                this.Text = label6.Text = "会审系统";
                label5.Text = "指挥室登录";
                label4.Hide();
            }

            StartSocketClient();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.labVer.Text = "版本：" + version;
            //   ScreenCapture sc = new ScreenCapture();
            // sc.CaptureScreenToFile(@"D:\" + DateTime.Now.Ticks.ToString() + ".png", ImageFormat.Png);
        }

        private void AutoLogin()
        {
            //自动登录
            _userName = "Teacher001";
            string pwd = "123456";
            _displayName = "主机端001";
            LoginIn(_userName, _displayName, pwd);
        }





        private void StartSocketClient()
        {
            Thread td = new Thread(() =>
            {

                GlobalVariable.client = new EduTCPClient(ProgramType.Teacher);
                GlobalVariable.client.OnClentIsConnecting += Client_OnClentIsConnecting;
                GlobalVariable.client.OnUserLoginRes = (message) =>
                {
                    var result = JsonHelper.DeserializeObj<LoginResult>(message.DataStr);
                    if (result.success)
                    {
                        this.InvokeOnUiThreadIfRequired(() =>
                        {
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
                //  AutoLogin();
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
            _displayName = (GlobalVariable.IsHuiShenXiTong ? "指挥室" : "教师") + "001";
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

        private void LoginIn(string userName, string nickName, string password)
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

