using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatListPanel : UserControl
    {

        public ChatListPanel()
        {
            // SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            panGroup_content.MouseEnter += (obj, eve) =>
            {
                panGroup_content.Focus();
            };
            panGroup_content.MouseLeave += (obj, eve) =>
            {

            };
            //  SetPanelHover(this.panGroup);
            //   SetPanelHover(this.panTeam);
            //   SetPanelHover(this.panPrivate);
            // panTop_content.AutoScroll = panMiddle_content.AutoScroll = panBottom_content.AutoScroll = true;
            panGroup_content.MouseWheel += (obj, eve) =>
            {
                var scroll = this.panGroup_content.VerticalScroll;
                if (!(panGroup_content.VerticalScroll.Visible == false ||
                (panGroup_content.VerticalScroll.Value == 0 && eve.Delta > 0) ||
                (panGroup_content.VerticalScroll.Value == lastRightPanelVerticalScrollValue && eve.Delta < 0)))
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
            panGroup_content.ControlAdded += (obj, eve) =>
            {

            };

            panGroup.MouseClick += (sender, e) => {
              
            };

        }

        private void PanGroup_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        int groupLastY = 0;
        int teamLastY = 0;
        int privateLastY = 0;
        int lastRightPanelVerticalScrollValue = 0;

        private void ChatListPanel_Load(object sender, System.EventArgs e)
        {
            var contentHeight = this.panOut.Height - panGroup.Height * 3;
            panGroup_content.Height = panTeam_content.Height = panPrivate_content.Height = contentHeight;
            panGroup_content.BackColor = panTeam_content.BackColor = panPrivate_content.BackColor = Color.FromArgb(235, 235, 235);
        }

        //IList<ChatItem> chatItemList;

        //public IList<ChatItem> ChatItemList
        //{
        //    get
        //    {
        //        return chatItemList;

        //    }
        //}


        public void AddChatItem(ChatItem item)
        {
            switch (item.ChatType)
            {
                case Common.ChatType.PrivateChat:
                    panPrivate_content.Controls.Add(item);
                    item.Top = privateLastY;
                    privateLastY += item.Height + 5;

                    break;
                case Common.ChatType.GroupChat:
                    panGroup_content.Controls.Add(item);
                    item.Top = groupLastY;
                    groupLastY += item.Height + 5;

                    break;
                case Common.ChatType.TeamChat:
                    panTeam_content.Controls.Add(item);
                    item.Top = teamLastY;
                    teamLastY += item.Height + 5;

                    break;
                default:
                    break;
            }

        }

        private void SetPanelHover(Control control)
        {
            control.MouseEnter += (sender, e) => ((Control)sender).BackColor = Color.Red;
            control.MouseLeave += (sender, e) => ((Control)sender).BackColor = Color.FromArgb(255, 255, 255);
            foreach (Control item in control.Controls)
            {
                item.MouseMove += (sender, e) =>
                {
                    item.BackColor = item.Parent.BackColor = Color.Red;

                };
                item.MouseLeave += (sender, e) =>
                {
                    item.BackColor = Color.Transparent;
                    item.Parent.BackColor = Color.FromArgb(255, 255, 255);
                };
            }


        }

        //public Panel GroupPanel { get { return this.panTop; } }
        //public Panel TeamPanel { get { return this.panMiddle; } }
        //public Panel PrivatePanel { get { return this.panBottom; } }


        public void ClearChatItem(int index)
        {
            switch (index)
            {
                case 0:
                    this.panGroup_content.Controls.Clear();
                    break;
                case 1:
                    this.panTeam_content.Controls.Clear();
                    break;
                case 2:
                    this.panPrivate_content.Controls.Clear();
                    break;
                default:
                    break;
            }
        }



        //public void SetDefaultImage(int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            this.picGroup.Image = Resource1.所有人;
        //            this.panGroup_content.Enabled = true;
        //            break;
        //        case 1:
        //            this.picTeam.Image = Resource1.群聊;
        //            this.panTeam_content.Enabled = true;
        //            break;
        //        case 2:
        //            this.picPrivate.Image = Resource1.私聊;
        //            this.panPrivate_content.Enabled = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}


        //public void SetFobbidImage(int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            this.picGroup.Image = Resource1.禁止;
        //            this.panGroup_content.Enabled = false;
        //            break;
        //        case 1:
        //            this.picTeam.Image = Resource1.禁止;
        //            this.panTeam_content.Enabled = false;
        //            break;
        //        case 2:
        //            this.picPrivate.Image = Resource1.禁止;
        //            this.panPrivate_content.Enabled = false;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void SetAllowOrFobbitChat(ChatType chatType, bool allow)
        {
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    if (allow)
                    {
                     //   this.picPrivate.Image = Resource1.私聊;
                        this.panPrivate_content.Enabled = true;
                    }
                    else
                    {
                    //  this.picPrivate.Image = Resource1.禁止;
                        this.panPrivate_content.Enabled = false;
                    }
                    break;
                case ChatType.GroupChat:
                    break;
                case ChatType.TeamChat:
                    if (allow)
                    {
                        //this.picTeam.Image = Resource1.群聊;
                        this.panTeam_content.Enabled = true;
                    }
                    else
                    {
                      //  this.picTeam.Image = Resource1.禁止;
                        this.panTeam_content.Enabled = false;
                    }
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
            ShowContent(panGroup_content);
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
            if (e.Button == MouseButtons.Left)
            {
                ShowContent(panTeam_content);
            }
        }

        private void panBottom_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowContent(panPrivate_content);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    chatItem = CreateChatItem();
        //    chatItem.Top = lastY;
        //    this.panTop_content.Controls.Add(chatItem);
        //    lastY += chatItem.Height + 20;

        //}

        /// <summary>
        /// 刷新群组信息
        /// </summary>
        public void ReflashTeamChat()
        {
            if (GlobalVariable.IsTeamChatChanged)
            {
                GlobalVariable.IsTeamChatChanged = false;
                var list = GlobalVariable.GetTeamChatList();
                this.panTeam_content.Controls.Clear();
                foreach (ChatStore item in list)
                {
                    CreateItem(item);
                }
            }
        }

        public ChatItem CreateItem(ChatStore store)
        {
            ChatItem item = new ChatItem(store.ChatUserName,
                store.ChatDisplayName, store.ChatType, store.UserType);
            this.AddChatItem(item);
            return item;
        }

        public ChatItem CreateItem(ChatMessage request)
        {
            ChatItem item = new ChatItem(request.SendUserName,
                 request.SendDisplayName, request.ChatType, request.UserType);
            this.AddChatItem(item);
            return item;
        }

        public void CreateNewGroupChat(string groupId)
        {
            var groupChat = GlobalVariable.CreateGroupChat(groupId);
            if (groupChat != null)
            {
                CreateItem(groupChat);
            }
        }


        private ChatItem SelectChatItemInPanels(string userName)
        {
            foreach (ChatItem item in panGroup_content.Controls)
            {
                if (item.UserName == userName)
                {
                    return item;
                }
            }

            foreach (ChatItem item in panTeam_content.Controls)
            {
                if (item.UserName == userName)
                {
                    return item;
                }
            }
            foreach (ChatItem item in panPrivate_content.Controls)
            {
                if (item.UserName == userName)
                {
                    return item;
                }
            }
            return null;
        }


        public ChatItem GetChatItem(ChatMessage message)
        {
            var item = SelectChatItemInPanels(message.SendUserName);
            if (item == null)
            {
                return CreateItem(message);
            }
            return item;
        }

        public ChatItem SelectedChatItem { get; set; }

        private void panGroup_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowContent(this.panGroup_content);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panGroup.Forbit)
            {
                panGroup.Forbit = false;
       
            }
            else
            {
                panGroup.Forbit = true;
            }
            panGroup.Invalidate();
        }
    }
}
