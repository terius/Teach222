using Common;
using Model;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatForm : MyForm
    {
        #region 变量
        /// <summary>
        /// 当前用户姓名
        /// </summary>
        string _myDisplayName = GlobalVariable.LoginUserInfo.DisplayName;
        /// <summary>
        /// 当前用户登录名
        /// </summary>
        string _myUserName = GlobalVariable.LoginUserInfo.UserName;

        /// <summary>
        /// 选择聊天的用户名
        /// </summary>
        string selectUserName = "";

        /// <summary>
        /// 页面是否关闭或隐藏
        /// </summary>
        public bool IsHide { get; set; }
        string groupId = "allpeople";
        readonly string UploadFileServer = System.Configuration.ConfigurationManager.AppSettings["UploadFileServer"];
        readonly string ServerIp = System.Configuration.ConfigurationManager.AppSettings["serverIP"];
        //  AudioRecorder audioRecorder;
        RecordVoice recordVoice;
        // string saveAudioFile = "";
        //   string saveAudioFilePath = "";

        #endregion
        public ChatForm()
        {
            InitializeComponent();
            ChatNav.SelectChatItem += ChatNav_SelectChatItem;
            ChatNav.CreateNewGroupChat(groupId);
            InitProgressBar();
            CheckPath();
        }

        private void ChatNav_SelectChatItem(object sender, ChatItem chatItem)
        {
            selectUserName = chatItem.UserName;
            this.labChatTitle.Text = "与【" + chatItem.DisplayName + "】的对话：";
            //  chatItem.Caption = chatItem.DisplayName;
            if (chatItem.FromClick && chatItem.UserName == selectUserName)
            {
                return;
            }
            //  chatItem.SmallImage = chatItem.DefaultImg;
            if (chatItem.UserName != selectUserName)
            {
                LoadChatMessage(chatItem);
            }
            AppendNewMessage(chatItem);
          
        }


        #region 方法
        public void PlayVoice(string fileName)
        {
            if (recordVoice == null)
            {
                recordVoice = new RecordVoice();
            }
            recordVoice.PlayVoice(fileName);
        }

        private void InitProgressBar()
        {
            progressBarControl1.Minimum = 0;
            //设置一个最大值
            progressBarControl1.Maximum = 100;
            //设置步长，即每次增加的数
            progressBarControl1.Step = 1;

            // progressBarControl1.PercentView = true;
            progressBarControl1.Visible = false;
        }

        private void CheckPath()
        {
            if (!Directory.Exists(GlobalVariable.AudioRecordPath))
            {
                Directory.CreateDirectory(GlobalVariable.AudioRecordPath);
            }


            if (!Directory.Exists(GlobalVariable.DownloadPath))
            {
                Directory.CreateDirectory(GlobalVariable.DownloadPath);
            }
        }
        #endregion

        #region 方法
        ///// <summary>
        ///// 创建聊天对象
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="isCreate"></param>
        //public void CreateChatItems(AddChatRequest request, bool fromReceieveMessage)
        //{
        //    this.Text = GlobalVariable.LoginUserInfo.DisplayName + " 的聊天窗口";
        //    IsHide = false;
        //    ReflashTeamChat();
        //    ChatItem chatItem = GetItemInChatListBox(request.UserName);
        //    if (chatItem == null)
        //    {
        //        chatItem = ChatNav.CreateItem(request);

        //    }

        //    if (fromReceieveMessage)
        //    {
        //        if (!string.IsNullOrWhiteSpace(selectUserName) && chatItem.UserName != selectUserName)
        //        {
        //            //chatItem.Caption = chatItem.DisplayName + " 有新消息！";
        //            chatItem.SmallImage = Resource1.新消息24;
        //        }
        //        else
        //        {
        //            ChatItemSelected(chatItem, false);
        //        }
        //    }
        //    else
        //    {
        //        ChatItemSelected(chatItem, false);
        //    }
        //}


        public void ReflashTeamChat()
        {
            ChatNav.ReflashTeamChat();
        }



        public void CreateChatItems(ChatMessage request, bool fromReceieveMessage)
        {
            this.Text = GlobalVariable.LoginUserInfo.DisplayName + " 的聊天窗口";
            IsHide = false;
            ChatNav.ReflashTeamChat();
            var chatItem = ChatNav.GetChatItem(request);

            //var chatItem = GetItemInChatListBox(request.SendUserName);
            //if (chatItem == null)
            //{
            //    chatItem = ChatNav.CreateItem(request);

            //}

            if (fromReceieveMessage)
            {
                if (!string.IsNullOrWhiteSpace(selectUserName) && chatItem.UserName != selectUserName)
                {
                    chatItem.SetNewMessagePic();
                    //chatItem.Caption = chatItem.DisplayName + " 有新消息！";
                    //  chatItem.SmallImage = Resource1.新消息24;
                }
                else
                {
                    ChatItemSelected(chatItem);
                }
            }
            else
            {
                ChatItemSelected(chatItem);
            }
        }


        public void ChangeAllowChat(ChatType chatType, bool allow)
        {
            ChatNav.SetAllowOrFobbitChat(chatType, allow);
            //switch (chatType)
            //{
            //    case ChatType.PrivateChat:
            //        if (allow)
            //        {
            //            ChatNav.Groups[2].LargeImage = Resource1.私;
            //        }
            //        else
            //        {
            //            ChatNav.Groups[2].LargeImage = Resource1.禁止;
            //        }
            //        break;
            //    case ChatType.GroupChat:
            //        break;
            //    case ChatType.TeamChat:
            //        if (allow)
            //        {
            //            ChatNav.Groups[1].LargeImage = Resource1.群组;
            //        }
            //        else
            //        {
            //            ChatNav.Groups[1].LargeImage = Resource1.禁止;
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// 聊天列表对象被选中
        /// </summary>
        /// <param name="chatItem"></param>
        private void ChatItemSelected(ChatItem chatItem)
        {

            ChatNav.SetSelectChatItem(chatItem, false);
        }




        ///// <summary>
        ///// 获取当前聊天对象
        ///// </summary>
        ///// <param name="chatUserName"></param>
        ///// <returns></returns>
        //private ChatItem GetItemInChatListBox(string userName)
        //{
        //    foreach (ChatItem item in ChatNav.ChatItemList)
        //    {
        //        if (item.UserName == userName)
        //        {
        //            return item;
        //        }
        //    }

        //    return null;
        //}


        /// <summary>
        /// 显示新的聊天信息
        /// </summary>
        /// <param name="subItem"></param>
        private void AppendNewMessage(ChatItem subItem)
        {
            var selectChatStore = subItem.GetChatStore();
            if (selectChatStore != null)
            {
                if (selectChatStore.NewMessageList != null)
                {

                    foreach (ChatMessage item in selectChatStore.NewMessageList)
                    {
                        AppendMessage(item, false);
                    }
                    GlobalVariable.SaveChatMessage(smsPanelNew1, subItem.UserName);
                }
            }
        }



        /// <summary>
        /// 加载聊天历史记录
        /// </summary>
        private void LoadChatMessage(ChatItem subItem)
        {
            var chatStore = subItem.GetChatStore();
            if (chatStore == null)
            {
                return;
            }
            if (chatStore.HistoryContentNew == null)
            {
                chatStore.HistoryContentNew = new smsPanelNew();
                panMessage.Controls.Add(chatStore.HistoryContentNew);
            }
            smsPanelNew1 = chatStore.HistoryContentNew;
            chatStore.HistoryContentNew.BringToFront();

        }




        //private void ChatForm_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    g.SmoothingMode = SmoothingMode.HighQuality;
        //    //  this.chatBox_history.BackColor = Color.WhiteSmoke;
        //    ////全屏蒙浓遮罩层
        //    //g.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 255, 255)), new Rectangle(0, 0, this.Width, this.chatBox_history.Top));
        //    //g.FillRectangle(new SolidBrush(Color.FromArgb(80, 255, 255, 255)), new Rectangle(0, this.chatBox_history.Top, this.chatBox_history.Width + this.chatBox_history.Left, this.Height - this.chatBox_history.Top));

        //    //线条
        //    // g.DrawLine(new Pen(Color.FromArgb(180, 198, 221)), new Point(0, this.chatBox_history.Top - 1), new Point(chatBox_history.Right, this.chatBox_history.Top - 1));
        //    //   g.DrawLine(new Pen(Color.FromArgb(180, 198, 221)), new Point(0, this.chatBox_history.Bottom), new Point(chatBox_history.Right, this.chatBox_history.Bottom));
        //}


        /// <summary>
        /// 发送聊天信息
        /// </summary>
        /// <param name="receieveUserName"></param>
        /// <param name="msg"></param>
        private bool SendMessageCommand(ChatMessage chatMessage)
        {

            var chatType = GlobalVariable.GetChatType(chatMessage.ReceieveUserName);
            if (chatType == ChatType.PrivateChat)
            {

                if (!GlobalVariable.LoginUserInfo.AllowPrivateChat)
                {
                    GlobalVariable.ShowError("您不允许发送私聊信息");
                    return false;
                }
                PrivateChatRequest request = new PrivateChatRequest();
                request.guid = Guid.NewGuid().ToString();
                request.msg = chatMessage.Message;
                request.receivename = chatMessage.ReceieveUserName;
                request.SendDisplayName = GlobalVariable.LoginUserInfo.DisplayName;
                request.SendUserName = GlobalVariable.LoginUserInfo.UserName;
                request.clientRole = GlobalVariable.LoginUserInfo.UserType;
                // if (chatMessage.MessageType != MessageType.String)
                //  {
                request.MessageType = chatMessage.MessageType;
                request.DownloadFileUrl = chatMessage.DownloadFileUrl;
                // }
                GlobalVariable.client.Send_PrivateChat(request);

            }
            else if (chatType == ChatType.TeamChat)
            {
                if (!GlobalVariable.LoginUserInfo.AllowTeamChat)
                {
                    GlobalVariable.ShowError("您不允许发送群聊信息");
                    return false;
                }
                var chat = GlobalVariable.GetChatStoreByUserName(chatMessage.ReceieveUserName);
                TeamChatRequest request = new TeamChatRequest();
                request.groupname = chat.ChatDisplayName;
                request.groupuserList = chat.GetUserNames();
                request.msg = chatMessage.Message;
                request.username = GlobalVariable.LoginUserInfo.UserName;
                request.groupid = chatMessage.ReceieveUserName;
                request.SendDisplayName = GlobalVariable.LoginUserInfo.DisplayName;
                request.clientRole = GlobalVariable.LoginUserInfo.UserType;
                request.MessageType = chatMessage.MessageType;
                request.DownloadFileUrl = chatMessage.DownloadFileUrl;
                GlobalVariable.client.Send_TeamChat(request);
                //  GlobalVariable.client.SendMessage(request, CommandType.TeamChat);
            }
            else if (chatType == ChatType.GroupChat)
            {
                var request = new GroupChatRequest();
                request.msg = chatMessage.Message;
                request.SendDisplayName = GlobalVariable.LoginUserInfo.DisplayName;
                request.SendUserName = groupId;
                request.clientRole = GlobalVariable.LoginUserInfo.UserType;
                request.MessageType = chatMessage.MessageType;
                request.DownloadFileUrl = chatMessage.DownloadFileUrl;
                GlobalVariable.client.Send_GroupChat(request);
            }
            //   GlobalVariable.AddPrivateChatToChatList(_userName, GlobalVariable.LoginUserInfo.DisplayName, msg);
            return true;

        }





        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <param name="isInput"></param>
        private void AppendMessage(ChatMessage chatMessage, bool isInput)
        {


            //  sms2 sms = new sms2(chatMessage, !isMySelf);
            //   sms.Location = GetNewPoint(panel1, sms.Width, !isMySelf);
            //    this.panel1.Controls.Add(sms);
            smsPanelNew1.AddMessage(chatMessage);
            if (isInput)
            {
                //清空发送输入框
                this.sendBox.Text = string.Empty;
                this.sendBox.Focus();
            }
        }



        // AlertControl messagebox;

        #endregion

        private void sendBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSendMessage();
                e.Handled = true;
            }
        }

        private void BtnSendMessage()
        {
            string content = sendBox.Text;
            //发送内容为空时，不做响应
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            if (ChatNav.SelectedChatItem == null || string.IsNullOrWhiteSpace(selectUserName))
            {
                GlobalVariable.ShowWarnning("请先选择聊天对象");
                return;
            }

            var message = new ChatMessage(_myUserName, _myDisplayName, selectUserName, content, GlobalVariable.LoginUserInfo.UserType);
            if (SendMessageCommand(message))
            {
                AppendMessage(message, true);
                GlobalVariable.SaveChatMessage(smsPanelNew1, selectUserName);
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsHide = true;
            this.Hide();
            e.Cancel = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            BtnSendMessage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.IsHide = true;
            this.Hide();
        }
    }
}
