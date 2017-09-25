using Common;
using Helpers;
using Model;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
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
            labChatTitle.Text = "";
            ChatNav.SelectChatItem += ChatNav_SelectChatItem;
            ChatNav.CreateNewGroupChat(groupId);
            InitProgressBar();
          

        }

    

        private void ChatNav_SelectChatItem(object sender, ChatItem chatItem)
        {
           
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
            selectUserName = chatItem.UserName;
        }


        #region 方法
        public void PlayVoice(string fileName,IntPtr handle)
        {
            if (recordVoice == null)
            {
                recordVoice = new RecordVoice();
            }
            recordVoice.PlayVoice(fileName, handle);
        }

        private void InitProgressBar()
        {
            ProgressBar.Minimum = 0;
            //设置一个最大值
            ProgressBar.Maximum = 100;
            //设置步长，即每次增加的数
            ProgressBar.Step = 1;

            // progressBarControl1.PercentView = true;
            ProgressBar.Visible = false;
        }

     
       
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
                    chatItem.ShowNewMessageIcon();
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


        public void UploadFileToALL(string uploadFile = null, bool isTempFile = false)
        {
            try
            {
                if (ChatNav.SelectedChatItem == null || string.IsNullOrWhiteSpace(selectUserName))
                {
                    GlobalVariable.ShowWarnning("请先选择聊天对象");
                    return;
                }
                if (string.IsNullOrWhiteSpace(uploadFile))
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "媒体文件 (*.jpg,*.gif,*.bmp,*.png,*.mp3,*.wav,*.amr,*.mp4,*.avi,*.mpg)|*.jpg;*.gif;*.bmp;*.png;*.mp3;*.wav;*.amr;*.mp4;*.avi;*.mpg";
                    dlg.Title = "选择媒体文件";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        uploadFile = dlg.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                if (!File.Exists(uploadFile))
                {
                    GlobalVariable.ShowWarnning("文件不存在");
                    return;
                }

                toolStrip1.Enabled = false;
                ProgressBar.Visible = true;
                //  ShowNotify("上传中，请稍候。。。");
                FileHelper.UploadFile(uploadFile, UploadFileServer, (sender, completeEventArgs) =>
                {
                    if (completeEventArgs.Error != null && !string.IsNullOrWhiteSpace(completeEventArgs.Error.Message))
                    {
                        this.toolStrip1.Enabled = true;
                        ProgressBar.Visible = false;
                        throw new Exception(completeEventArgs.Error.Message);
                    }
                    string result = Encoding.UTF8.GetString(completeEventArgs.Result);
                    UploadResult uploadResult = JsonHelper.DeserializeObj<UploadResult>(result);
                    if (uploadResult.error == 0)
                    {
                        uploadResult.url = "http://" + ServerIp + ":8080" + uploadResult.url;
                    }
                    else
                    {
                        GlobalVariable.ShowError(uploadResult.message);
                        this.toolStrip1.Enabled = true;
                        ProgressBar.Visible = false;
                        return;
                    }
                    FileInfo fi = new FileInfo(uploadFile);
                    var uploadtext = _myDisplayName + "上传了文件:" + fi.Name;
                    var messageType = GetMessageType(fi.Extension.ToLower());
                    var message = new ChatMessage(_myUserName, _myDisplayName, selectUserName, uploadtext, GlobalVariable.LoginUserInfo.UserType, messageType);
                    message.DownloadFileUrl = uploadResult.url;
                    message.LocalFilePath = uploadFile;
                    if (SendMessageCommand(message))
                    {
                        AppendMessage(message, true);
                        GlobalVariable.SaveChatMessage(smsPanelNew1, selectUserName);
                     //   ShowNotify("上传成功");
                    }
                    toolStrip1.Enabled = true;
                    ProgressBar.Visible = false;
                    //this.btnUploadFile.Enabled = true;
                    //btnRecordAudio.Enabled = true;
                    //progressBarControl1.Visible = false;
                    if (isTempFile)
                    {
                        File.Delete(uploadFile);
                        if (FileHelper.GetFileExt(uploadFile) == "amr")
                        {
                            File.Delete(uploadFile.Substring(0, uploadFile.LastIndexOf('.') + 1) + "wav");
                        }
                    }
                }, (ob, progress) =>
                {
                    var p = (int)(progress.BytesSent * 100 / progress.TotalBytesToSend);
                    ProgressBar.Value = p;
                    Application.DoEvents();

                });


            }
            catch (Exception)
            {
                this.toolStrip1.Enabled = true;
                ProgressBar.Visible = false;
                throw;
            }
        }

        private MessageType GetMessageType(string ext)
        {
            switch (ext)
            {
                case ".jpg":
                case ".gif":
                case ".bmp":
                case ".png":
                    return MessageType.Image;

                case ".mp3":
                case ".wav":
                case ".amr":
                    return MessageType.Sound;

                case ".mp4":
                case ".avi":
                case ".mpg":
                    return MessageType.Video;
                default:
                    return MessageType.String;
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

        private void toolUploadPic_Click(object sender, EventArgs e)
        {
            UploadFileToALL();
        }

        private void toolRecordVoice_Click(object sender, EventArgs e)
        {
            if (!labRecordVoice.Visible)
            {
                toolRecordVoice.Image = Resource1.录音中;
                labRecordVoice.Visible = true;
                toolCancelRecordVoice.Visible = true;
                toolRecordVoice.ToolTipText = "点击结束录音并上传";
                Application.DoEvents();
               
                if (recordVoice == null)
                {
                    recordVoice = new RecordVoice();
                }
                recordVoice.BeginRecord2();
                //if (audioRecorder == null)
                //{
                //    audioRecorder = new AudioRecorder();
                //}
                //audioRecorder.StartRecording(saveAudioFile);
            }
            else
            {
                toolRecordVoice.Image = Resource1.录音;
                labRecordVoice.Visible = false;
                toolCancelRecordVoice.Visible = false;
                toolRecordVoice.ToolTipText = "点击开始录音";
                Application.DoEvents();
                // ((ToolTipItem)btnRecordAudio.SuperTip.Items[0]).Text = "点击按钮开始录音";
                string saveFile = recordVoice.StopRecord2();
                // audioRecorder.EndRecord();
                Thread.Sleep(500);

                UploadFileToALL(saveFile, false);
            }
        }

        private void labRecordVoice_Click(object sender, EventArgs e)
        {

        }

        private void toolCancelRecordVoice_Click(object sender, EventArgs e)
        {
            toolRecordVoice.Image = Resource1.录音;
            labRecordVoice.Visible = false;
            toolCancelRecordVoice.Visible = false;
            toolRecordVoice.ToolTipText = "点击开始录音";
            Application.DoEvents();
            recordVoice.CancelRecord();
        }

        private void ChatForm_VisibleChanged(object sender, EventArgs e)
        {
           //if (this.Visible)
           // {
           //     this.IsHide = false;
           // }
        }
    }
}
