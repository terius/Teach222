using Common;
using EduService;
using Model;
using SharedForms;
using System;
using System.Windows.Forms;

namespace NewTeacher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //GlobalVariable.CreateTestLoginInfo();
            //ChatForm frm = new ChatForm();
            //frm.Show();
            //GlobalVariable.CreateTestLoginInfo();
            //GlobalVariable.client = new MyClient(ProgramType.Teacher);
            //GlobalVariable.client.OnReveieveData += Client_OnReveieveData;
            //GlobalVariable.client.Send_UserLogin(GlobalVariable.LoginUserInfo.UserName, GlobalVariable.LoginUserInfo.DisplayName, "111", ClientRole.Teacher);
            //GlobalVariable.client.Send_OnlineList();
        }

        private void Client_OnReveieveData(ReceieveMessage message)
        {
            this.richTextBox1.InvokeOnUiThreadIfRequired(() =>
            {
                richTextBox1.AppendText(message.DataStr + "\r\n\r\n");

            });

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            PrivateChatRequest q = new PrivateChatRequest();
            q.clientRole = ClientRole.Teacher;
            q.guid = Guid.NewGuid().ToString();
            q.MessageType = MessageType.String;
            q.msg = DateTime.Now.Ticks.ToString();
            q.receivename = "stu001";
            q.SendDisplayName = GlobalVariable.LoginUserInfo.DisplayName;
            q.SendUserName = GlobalVariable.LoginUserInfo.UserName;

            GlobalVariable.client.Send_PrivateChat(q);
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == Media.MM_MCINOTIFY)
        //    {
        //     //   RefreshStop(false);
        //    }
        //    base.WndProc(ref m);
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
       
        }
    }

}
