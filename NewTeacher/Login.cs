using Common;
using Helpers;
using Model;
using MySocket;
using SharedForms;
using System;
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
            this.ContextMenuStrip = contextMenuStrip1;
            GlobalVariable.client = new MyClient(ProgramType.Teacher);
            GlobalVariable.client.OnUserLoginRes = (message) =>
            {
                var result = JsonHelper.DeserializeObj<LoginResult>(message.DataStr);
                if (result.success)
                {
                    this.InvokeOnUiThreadIfRequired(()=> {
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
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.labVer.Text = "版本：" + version;
            //自动登录
            _userName = "Teacher001";
            string pwd = "123456";
            _displayName = "老师001";
            GlobalVariable.client.Send_UserLogin(_userName, _displayName, pwd, ClientRole.Teacher);
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

            GlobalVariable.client.Send_UserLogin(_userName, _displayName, userPass, ClientRole.Teacher);
        }






        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}

