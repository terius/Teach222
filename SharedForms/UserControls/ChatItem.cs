using Common;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatItem : UserControl
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public ChatType ChatType { get; set; }
        Image headIcon, newMsgIcon;
        Font titleFont = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
        SolidBrush brush = new SolidBrush(Color.FromArgb(55, 152, 249));
        Color defaultBackColor = Color.FromArgb(250, 250, 250);
        Color defaultSelectColor = Color.FromArgb(200, 200, 200);
        public bool IsSelected { get; set; }
        ChatListPanel _parentPanel;
        public bool FromClick { get; set; }
        public ChatItem(ChatListPanel parent, string userName, string displayName, ChatType chatType, ClientRole userType)
        {
            _parentPanel = parent;
            InitializeComponent();
            this.Height = 24 + 10 * 2;
            this.BackColor = defaultBackColor;
            this.HorizontalScroll.Enabled = false;
            this.Cursor = Cursors.Hand;
            SetChatPanelHover();
            this.MouseClick += ChatItem_MouseClick;
            Name = "item_" + userName;
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    switch (userType)
                    {
                        case ClientRole.Teacher:
                        case ClientRole.Assistant:
                            headIcon = Resource1.老师24;
                            break;
                        case ClientRole.Student:
                            headIcon = Resource1.学生24;
                            break;
                        default:
                            break;
                    }
                    break;
                case ChatType.GroupChat:
                    headIcon = Resource1.所有人24;
                    break;
                case ChatType.TeamChat:
                    headIcon = Resource1.群组24;
                    var childList = GlobalVariable.GetTeamMemberDisplayNames(userName);
                    // Caption = displayName + " 【" + childList.Count + "】";
                    //   Hint = string.Join("\r\n", childList);
                    // this.ToolTipText = string.Join("\r\n", childList);

                    break;
                default:
                    break;
            }

            this.UserName = userName;
            this.DisplayName = displayName;
            this.ChatType = chatType;

        }

        private void ChatItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.BackColor = defaultSelectColor;
                IsSelected = true;
                _parentPanel.SetSelectChatItem(this, true);
            }
        }

        private void SetChatPanelHover()
        {
            this.MouseEnter += (sender, e) => ((Control)sender).BackColor = defaultSelectColor;
            this.MouseLeave += (sender, e) =>
            {
                if (!IsSelected)
                {
                    ((Control)sender).BackColor = defaultBackColor;
                }
            };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //switch (ChatType)
            //{
            //    case ChatType.PrivateChat:
            //        headIcon = Resource1.私聊;
            //        title = "私聊";
            //        break;
            //    case ChatType.GroupChat:
            //        headIcon = Resource1.所有人;
            //        title = "所有人";
            //        break;
            //    case ChatType.TeamChat:
            //        headIcon = Resource1.群聊;
            //        title = "群聊";
            //        break;
            //    default:
            //        break;
            //}

            var g = e.Graphics;
            int fontHeight = titleFont.Height;
            int x = 10;
            var rectArea = new Rectangle(x, (this.Height - 24) / 2, 24, 24);
            x += 24 + 10;
            g.DrawImage(headIcon, rectArea);
            g.DrawString(DisplayName, titleFont, brush, x, (this.Height - fontHeight) / 2);
            if (newMsgIcon != null)
            {
                g.DrawImage(newMsgIcon, new Rectangle(this.Width - 34, (this.Height - 24) / 2, 24, 24));
            }
            //  g.FillRectangle(brush, ClientRectangle);
            //  g.DrawLine(Pens.Red, 0, ClientSize.Height - 1, ClientSize.Width - 1, ClientSize.Height - 1);

        }


        public void SetNewMessagePic()
        {
            // this.picNewMessage.Image = Resource1.新消息24;
        }

        public ChatStore GetChatStore()
        {
            return GlobalVariable.ChatList.FirstOrDefault(d => d.ChatUserName == UserName);
        }

        public string GetTeamMemUserNames()
        {
            var chat = GetChatStore();
            return string.Join(",", chat.TeamMembers.Select(d => d.UserName));
        }
    }
}
