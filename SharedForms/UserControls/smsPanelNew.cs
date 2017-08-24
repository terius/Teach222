using Model;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class smsPanelNew : Panel
    {
        private int _lastY = 10;
       // int X = 10;
        sms2 chatItem;
        public smsPanelNew()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            HorizontalScroll.Enabled = false;

            ControlAdded += SmsPanelNew_ControlAdded;
            Resize += SmsPanelNew_Resize;
            MouseEnter += SmsPanelNew_MouseEnter;
        }

        private void SmsPanelNew_MouseEnter(object sender, System.EventArgs e)
        {
            this.Focus();
        }

        private void SmsPanelNew_Resize(object sender, System.EventArgs e)
        {
            sms2 s;
            foreach (Control item in Controls)
            {
                s = (sms2)item;
                if (s.IsMySelf)
                {
                    s.Left = this.Width - s.Width - 20;
                }
            }
        }

        private void SmsPanelNew_ControlAdded(object sender, ControlEventArgs e)
        {
            this.ScrollControlIntoView(e.Control);
        }

        public void AddMessage(ChatMessage messageInfo, bool isMySelf)
        {
            chatItem = new sms2(messageInfo, isMySelf);
            //if (isMySelf)
            //{
            //    X = Width - chatItem.SizeWidth - SystemInformation.VerticalScrollBarWidth - 5;
            //}
            //else
            //{
            //    X = 10;
            //}
            chatItem.Location = new Point(isMySelf ? this.Width - chatItem.Width - 20 : 0, _lastY);
            Controls.Add(chatItem);
            _lastY += chatItem.Height + 10;
        }
    }
}
