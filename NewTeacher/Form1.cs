using Common;
using Model;
using MySocket;
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
            GlobalVariable.client.OnReveieveData += Client_OnReveieveData;
            GlobalVariable.client.Send_OnlineList();
        }

        private void Client_OnReveieveData(ReceieveMessage message)
        {
            MessageBox.Show(message.DataStr);

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
    }

}
