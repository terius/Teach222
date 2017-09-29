using Model;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class smsPanel : Panel
    {
        private int _lastY = 10;
        // int X = 10;
        smsItem chatItem;
        public smsPanel()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;
            AutoScroll = true;
            ControlAdded += smsPanel_ControlAdded;
            Resize += smsPanel_Resize;
            MouseEnter += smsPanel_MouseEnter;
            BackColor = Color.White;
        }

        private void smsPanel_MouseEnter(object sender, System.EventArgs e)
        {
            this.Focus();
        }

        private void smsPanel_Resize(object sender, System.EventArgs e)
        {
         
            smsItem s;
            foreach (Control item in Controls)
            {
                s = (smsItem)item;
                if (s.IsMySelf)
                {
                    s.Left = this.Width - s.Width - 20;
                   
                }
            }
           // this.Invalidate();
        }

        /// <summary>
        /// 发送者是否为当前登录人
        /// </summary>
        /// <param name="sendUserName"></param>
        /// <returns></returns>
        private bool IsMySelf(string sendUserName)
        {
            return GlobalVariable.LoginUserInfo.UserName == sendUserName;
        }

        private void smsPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            this.ScrollControlIntoView(e.Control);
        }

        public void AddMessage(ChatMessage messageInfo)
        {
            bool isMySelf = IsMySelf(messageInfo.SendUserName);
            chatItem = new smsItem(messageInfo, isMySelf);
            //if (isMySelf)
            //{
            //    X = Width - chatItem.SizeWidth - SystemInformation.VerticalScrollBarWidth - 5;
            //}
            //else
            //{
            //    X = 10;
            //}
            chatItem.Location = new Point(isMySelf ? this.Width - chatItem.Width - 20 : 0, _lastY - VerticalScroll.Value);
            Controls.Add(chatItem);
            _lastY += chatItem.Height + 10;
        }
    }
}
