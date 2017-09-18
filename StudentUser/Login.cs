using Common;
using Helpers;
using Model;
using EduService;
using SharedForms;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentUser
{
    public partial class Login : Form
    {

        string userName, nickName;
        public Login()
        {
            InitializeComponent();
        }
        // string userGuid;

        //   private delegate void messageListCallback(string content);
        //  private messageListCallback messageCallback;
        private void Form2_Load(object sender, EventArgs e)
        {
            StartSocketClient();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.labVer.Text = "版本：" + version;

        }

        private void AutoLogin()
        {
            userName = "stu001";
            nickName = "学生001";
            string password = "pw001";
            LoginRequest q = new LoginRequest
            {
                ClientRole = ClientRole.Student,
                nickName = nickName,
                password = password,
                userName = userName

            };
            GlobalVariable.SendCommand(q, CommandType.UserLogin);
         //   GlobalVariable.client.Send_UserLogin(userName, nickName, password, ClientRole.Student);
        }

        private void StartSocketClient()
        {
            Thread td = new Thread(() =>
            {
                GlobalVariable.client = new EduTCPClient(ProgramType.Student);
                GlobalVariable.client.OnUserLoginRes = (message) =>
                {

                    var result = JsonHelper.DeserializeObj<LoginResult>(message.DataStr);
                    if (result.success)
                    {
                        GlobalVariable.TeacherIP = result.teachIP;
                        DoAction(() =>
                        {
                            // GlobalVariable.client.OnReveieveData -= Client_OnReveieveData;
                            GlobalVariable.LoginUserInfo = new LoginUserInfo
                            {
                                DisplayName = nickName,
                                UserName = userName,
                                UserType = ClientRole.Student,
                                No = "n001"
                            };
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        });


                    }
                    else
                    {
                        MessageBox.Show(result.msg);
                    }

                };
             //   AutoLogin();

            });
            td.IsBackground = true;
            td.Start();

        }

        private void DoAction(Action action)
        {
            this.InvokeOnUiThreadIfRequired(action);
        }





        private void LoginIn()
        {

            userName = textBox1.Text.Trim();
            nickName = "学生" + userName;
            string password = textBox2.Text.Trim();

            if (userName == string.Empty)
            {
                MessageBox.Show("请输入登陆用户名！");
                return;
            }
            if (password == string.Empty)
            {
                MessageBox.Show("请输入登陆密码！");
                return;
            }
            //  userGuid = Guid.NewGuid().ToString();
            LoginRequest request = new LoginRequest { ClientRole = ClientRole.Student, nickName = nickName, password = password, userName = userName };
            GlobalVariable.SendCommand(request,CommandType.UserLogin);
        }




        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginIn();
        }
    }
}

