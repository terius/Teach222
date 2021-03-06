﻿using Common;
using Model;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatListPanel : UserControl
    {
        Color defaultBackColor = Color.FromArgb(250, 250, 250);
        //   Color chatItemDefaultBackColor = Color.FromArgb(60, 179, 113);
        ChatTypePanel selectedPan;
        Color defaultSelectChatItemColor = Color.FromArgb(200, 200, 200);
        public event EventHandler<ChatItem> SelectChatItem;
        public ChatItem SelectedChatItem { get; set; }
        int chatPanelHeight;
        public ChatListPanel()
        {
            // SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            panGroup_content.BackColor = panTeam_content.BackColor = panPrivate_content.BackColor = Color.FromArgb(240, 255, 240);


            panGroup_content.HorizontalScroll.Enabled = panTeam_content.HorizontalScroll.Enabled = panPrivate_content.HorizontalScroll.Enabled = false;//好像没用

            #region 注册事件
            panGroup.MouseClick += PanGroup_MouseClick;
            panTeam.MouseClick += PanGroup_MouseClick;
            panPrivate.MouseClick += PanGroup_MouseClick;
            panGroup_content.MouseEnter += (obj, eve) =>
            {
                panGroup_content.Focus();
            };
            panGroup_content.MouseLeave += (obj, eve) =>
            {

            };
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
            panGroup_content.Resize += Chat_content_Resize;
            panTeam_content.Resize += Chat_content_Resize;
            panPrivate_content.Resize += Chat_content_Resize;
            panOut.Resize += PanOut_Resize;
            #endregion

        }

        private void PanOut_Resize(object sender, EventArgs e)
        {
            var height = panOut.Height - panGroup.Height * 3;
            if (panGroup_content.Height > 0)
            {
                panGroup_content.Height = height;
            }
            else if (panTeam_content.Height > 0)
            {
                panTeam_content.Height = height;
            }
            else if (panPrivate_content.Height > 0)
            {
                panPrivate_content.Height = height;
            }
        }

        private void Chat_content_Resize(object sender, EventArgs e)
        {
            var control = ((Control)sender);
            int height = control.Height;
            if (height > 0)
            {
                foreach (Control item in control.Controls)
                {
                    item.Width = panGroup.Width;
                }

            }
        }

        public void SetSelectPanel(ChatTypePanel panel)
        {

        }

        private void PanGroup_MouseClick(object sender, MouseEventArgs e)
        {
            var chatType = ((ChatTypePanel)sender).ChatType;
            if (selectedPan != null && selectedPan.ChatType != chatType)
            {
                selectedPan.BackColor = defaultBackColor;
            }
            selectedPan = ((ChatTypePanel)sender);
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    panGroup.IsSelected = false;
                    panTeam.IsSelected = false;
                    break;
                case ChatType.GroupChat:
                    panPrivate.IsSelected = false;
                    panTeam.IsSelected = false;
                    break;
                case ChatType.TeamChat:
                    panPrivate.IsSelected = false;
                    panGroup.IsSelected = false;
                    break;
                default:
                    break;
            }
            ShowOrHideContent(chatType);
        }

        int groupLastY = 0;
        int teamLastY = 0;
        int privateLastY = 0;
        int lastRightPanelVerticalScrollValue = 0;

        private void ChatListPanel_Load(object sender, System.EventArgs e)
        {
            //  GetChatPanelHeight();
            // panGroup_content.Height = panTeam_content.Height = panPrivate_content.Height = chatPanelHeight;

        }

        bool showNewMessageIcon;
        public void ShowNewMessageIcon(ChatType chatType)
        {
            if (!showNewMessageIcon)
            {
                showNewMessageIcon = true;
                switch (chatType)
                {
                    case ChatType.PrivateChat:
                        panPrivate.ShowNewMessageIcon();
                        break;
                    case ChatType.GroupChat:
                        panGroup.ShowNewMessageIcon();
                        break;
                    case ChatType.TeamChat:
                        panTeam.ShowNewMessageIcon();
                        break;
                    default:
                        break;
                }
            }
        }



        public void HideNewMessageIcon(ChatType chatType)
        {
            if (showNewMessageIcon)
            {
                showNewMessageIcon = false;
                switch (chatType)
                {
                    case ChatType.PrivateChat:
                        panPrivate.HideNewMessageIcon();
                        break;
                    case ChatType.GroupChat:
                        panGroup.HideNewMessageIcon();
                        break;
                    case ChatType.TeamChat:
                        panTeam.HideNewMessageIcon();
                        break;
                    default:
                        break;
                }
            }
        }



        //private int GetChatPanelHeightOld(ChatType chatType)
        //{
        //    int otherHeight = this.panOut.Height - panGroup.Height * 3;
        //    int leftHeight = 0;
        //    switch (chatType)
        //    {
        //        case ChatType.PrivateChat:
        //            if (panGroup_content.Height == otherHeight)
        //            {
        //                panGroup_content.Height = panGroup_content.Height / 2;
        //            }
        //            if (panTeam_content.Height == otherHeight)
        //            {
        //                panTeam_content.Height = panTeam_content.Height / 2;
        //            }
        //            leftHeight = otherHeight - panGroup_content.Height - panTeam_content.Height;
        //            break;
        //        case ChatType.GroupChat:
        //            if (panPrivate_content.Height == otherHeight)
        //            {
        //                panPrivate_content.Height = panPrivate_content.Height / 2;
        //            }
        //            if (panTeam_content.Height == otherHeight)
        //            {
        //                panTeam_content.Height = panTeam_content.Height / 2;
        //            }
        //            leftHeight = otherHeight - panPrivate_content.Height - panTeam_content.Height;
        //            break;
        //        case ChatType.TeamChat:
        //            if (panGroup_content.Height == otherHeight)
        //            {
        //                panGroup_content.Height = panGroup_content.Height / 2;
        //            }
        //            if (panPrivate_content.Height == otherHeight)
        //            {
        //                panPrivate_content.Height = panPrivate_content.Height / 2;
        //            }
        //            leftHeight = otherHeight - panGroup_content.Height - panPrivate_content.Height;
        //            break;
        //        default:
        //            break;
        //    }
        //    if (leftHeight == 0)
        //    {
        //        leftHeight = otherHeight / 2;
        //    }
        //    return leftHeight;
        //}

        //IList<ChatItem> chatItemList;

        //public IList<ChatItem> ChatItemList
        //{
        //    get
        //    {
        //        return chatItemList;

        //    }
        //}

        private void ClearTeam()
        {
            this.panTeam_content.Controls.Clear();
            teamLastY = 0;
        }


        public void AddChatItem(ChatItem item)
        {
            item.Width = panGroup.Width;
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


        private int ShowChatContent(ChatType chatType)
        {
            int otherHeight = this.panOut.Height - panGroup.Height * 3;
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    if (panPrivate_content.Height == 0)
                    {
                        panGroup_content.Height = panTeam_content.Height = 0;
                        panPrivate_content.Height = otherHeight;
                    }
                    break;
                case ChatType.GroupChat:
                    if (panGroup_content.Height == 0)
                    {
                        panPrivate_content.Height = panTeam_content.Height = 0;
                        panGroup_content.Height = otherHeight;
                    }
                    break;
                case ChatType.TeamChat:
                    if (panTeam_content.Height == 0)
                    {
                        panGroup_content.Height = panPrivate_content.Height = 0;
                        panTeam_content.Height = otherHeight;
                    }
                    break;
                default:
                    break;
            }
            return otherHeight;
        }



        private void ShowOrHideContent(ChatType chatType)
        {
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    if (panPrivate_content.Height > 0)
                    {
                        panPrivate_content.Height = 0;
                    }
                    else
                    {
                        ShowChatContent(chatType);
                    }
                    break;
                case ChatType.GroupChat:
                    if (panGroup_content.Height > 0)
                    {
                        panGroup_content.Height = 0;
                    }
                    else
                    {
                        ShowChatContent(chatType);
                    }
                    break;
                case ChatType.TeamChat:
                    if (panTeam_content.Height > 0)
                    {
                        panTeam_content.Height = 0;
                    }
                    else
                    {
                        ShowChatContent(chatType);
                    }
                    break;
                default:
                    break;
            }

        }


        //private void ShowContent(ChatType chatType)
        //{
        //    switch (chatType)
        //    {
        //        case ChatType.PrivateChat:
        //            if (panPrivate_content.Height == 0)
        //            {
        //                panPrivate_content.Height = ShowChatContent(chatType);
        //            }

        //            break;
        //        case ChatType.GroupChat:
        //            if (panGroup_content.Height == 0)
        //            {
        //                panGroup_content.Height = ShowChatContent(chatType);
        //            }

        //            break;
        //        case ChatType.TeamChat:
        //            if (panTeam_content.Height == 0)
        //            {
        //                panTeam_content.Height = ShowChatContent(chatType);
        //            }
        //            break;
        //        default:
        //            break;
        //    }

        //}




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
                // var list = GlobalVariable.GetTeamChatList();
                ClearTeam();
                foreach (var team in GlobalVariable.TeamList)
                {
                    if (team.CheckUserIsInTeam(GlobalVariable.LoginUserInfo.UserName))
                    {
                        CreateTeamChatItem(team);
                    }
                }
            }
        }


        public ChatItem CreateTeamChatItem(Team team)
        {
            ChatItem item = new ChatItem(this, team.TeamId,
                team.TeamName, ChatType.TeamChat, ClientRole.Student);
            this.AddChatItem(item);
            return item;
        }

        public ChatItem CreateGroupChatItem(ChatStore store)
        {
            ChatItem item = new ChatItem(this, store.ChatUserName,
                "全体成员", ChatType.GroupChat, ClientRole.Student);
            this.AddChatItem(item);
            return item;
        }

        public ChatItem CreateItem(ChatMessage request)
        {
            ChatItem item = new ChatItem(this, request.SendUserName,
                 request.SendDisplayName, request.ChatType, request.UserType);
            this.AddChatItem(item);
            return item;
        }

        public void CreateALLGroupChat(string groupId)
        {
            var groupChat = GlobalVariable.CreateGroupChat(groupId);
            if (groupChat != null)
            {
                CreateGroupChatItem(groupChat);
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

        public void SetSelectChatItem(ChatItem item, bool fromClick)
        {
            ShowChatContent(item.ChatType);
            if (SelectedChatItem != null && SelectedChatItem.UserName != item.UserName)
            {
                SelectedChatItem.BackColor = defaultBackColor;
                SelectedChatItem.IsSelected = false;
            }
            item.IsSelected = true;
            SelectedChatItem = item;
            SelectedChatItem.BackColor = defaultSelectChatItemColor;
            SelectedChatItem.FromClick = fromClick;
            SelectChatItem(this, SelectedChatItem);
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
