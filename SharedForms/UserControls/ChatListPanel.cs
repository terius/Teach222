using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms.UserControls
{
    public partial class ChatListPanel : UserControl
    {
        public ChatListPanel()
        {
            InitializeComponent();


        }

        Panel chatItem;
        int lastY = 0;
        int lastRightPanelVerticalScrollValue = 0;

        private void ChatListPanel_Load(object sender, System.EventArgs e)
        {
            var contentHeight = this.panOut.Height - panTop.Height * 3;
            panTop_content.Height = contentHeight;
            panMiddle_content.Height = contentHeight;
            panBottom_content.Height = contentHeight;
            panTop_content.MouseEnter += (obj, eve) =>
            {
                panTop_content.Focus();
            };
            panTop_content.MouseLeave += (obj, eve) =>
            {

            };
            SetPanelHover(this.panTop);
            SetPanelHover(this.panMiddle);
            SetPanelHover(this.panBottom);
            // panTop_content.AutoScroll = panMiddle_content.AutoScroll = panBottom_content.AutoScroll = true;
            panTop_content.MouseWheel += (obj, eve) =>
         {
             var scroll = this.panTop_content.VerticalScroll;
             if (!(panTop_content.VerticalScroll.Visible == false ||
             (panTop_content.VerticalScroll.Value == 0 && eve.Delta > 0) ||
             (panTop_content.VerticalScroll.Value == lastRightPanelVerticalScrollValue && eve.Delta < 0)))
             {

                 lastRightPanelVerticalScrollValue = scroll.Value;
                 if (eve.Delta > 0) //滑轮向上
                 {
                     if (scroll.Maximum > 100)
                     {
                         scroll.Value = scroll.Value - eve.Delta >= 0 ? scroll.Value - eve.Delta : 0;
                     }
                 }
                 if (eve.Delta < 0)  //滑轮向下
                 {
                     if (scroll.Maximum > 100)
                     {
                         scroll.Value = scroll.Value - eve.Delta <= scroll.Maximum ? scroll.Value - eve.Delta : 0;
                     }
                 }

             }

         };
            panTop_content.ControlAdded += (obj, eve) =>
            {

            };


        }

        private void SetPanelHover(Control control)
        {
            control.MouseMove += (sender, e) => ((Control)sender).BackColor = Color.FromArgb(245, 245, 245);
            control.MouseLeave += (sender, e) => ((Control)sender).BackColor = Color.FromArgb(255, 255, 255);

        }

        public Panel GroupPanel { get { return this.panTop; } }
        public Panel TeamPanel { get { return this.panMiddle; } }
        public Panel PrivatePanel { get { return this.panBottom; } }


        public void ClearChatItem(int index)
        {
            switch (index)
            {
                case 0:
                    this.panTop.Controls.Clear();
                    break;
                case 1:
                    this.panMiddle.Controls.Clear();
                    break;
                case 2:
                    this.panBottom.Controls.Clear();
                    break;
                default:
                    break;
            }
        }

        public void SetDefaultImage(int index)
        {
            switch (index)
            {
                case 0:
                    this.picTop.Image = Resource1.所有人;
                    this.panTop_content.Enabled = true;
                    break;
                case 1:
                    this.picMiddle.Image = Resource1.群聊;
                    this.panMiddle_content.Enabled = true;
                    break;
                case 2:
                    this.picBottom.Image = Resource1.私聊;
                    this.panBottom_content.Enabled = true;
                    break;
                default:
                    break;
            }
        }


        public void SetFobbidImage(int index)
        {
            switch (index)
            {
                case 0:
                    this.picTop.Image = Resource1.禁止;
                    this.panTop_content.Enabled = false;
                    break;
                case 1:
                    this.picMiddle.Image = Resource1.禁止;
                    this.panMiddle_content.Enabled = false;
                    break;
                case 2:
                    this.picBottom.Image = Resource1.禁止;
                    this.panBottom_content.Enabled = false;
                    break;
                default:
                    break;
            }
        }


        private Panel CreateChatItem()
        {
            var chatItem1 = new Panel();
            chatItem1.BackColor = Color.Lime;
            chatItem1.Width = this.panOut.Width - 20;
            chatItem1.Height = 40;
            Label lab = new Label();
            lab.Text = DateTime.Now.Ticks.ToString();
            chatItem1.Controls.Add(lab);
            chatItem1.Padding = new Padding(10, 10, 10, 10);
            // chatItem1.Dock = DockStyle.Top;
            return chatItem1;
        }

        private void panTop_MouseClick(object sender, MouseEventArgs e)
        {
            ShowContent(panTop_content);
        }

        private void ShowContent(Panel panContent)
        {
            if (panContent.Visible)
            {
                panContent.Visible = false;
            }
            else
            {
                panContent.Visible = true;
            }
        }

        private void panMiddle_MouseClick(object sender, MouseEventArgs e)
        {
            ShowContent(panMiddle_content);
        }

        private void panBottom_MouseClick(object sender, MouseEventArgs e)
        {
            ShowContent(panBottom_content);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chatItem = CreateChatItem();
            chatItem.Top = lastY;
            this.panTop_content.Controls.Add(chatItem);
            lastY += chatItem.Height + 20;

        }


    }
}
