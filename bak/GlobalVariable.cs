using Common;
using Helpers;
using Model;
using EduService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using System.Diagnostics;

namespace SharedForms
{
    public static class GlobalVariable
    {

        #region 字段和属性
        /// <summary>
        /// 文件根目录
        /// </summary>
        public static readonly string BaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
        /// <summary>
        /// 下载文件存放目录
        /// </summary>
        public static readonly string DownloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\DownloadFiles");
        /// <summary>
        /// 音频文件存放目录
        /// </summary>
        public static readonly string AudioRecordPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\AudioRecord");



        /// <summary>
        /// 临时文件存放目录
        /// </summary>
        public static readonly string TempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\Temp");
        /// <summary>
        /// 视频文件存放目录
        /// </summary>
        public static readonly string VideoRecordPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\VideoRecord");
        /// <summary>
        /// FFmpeg程序目录
        /// </summary>
        public static readonly string FFmpegFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");
        /// <summary>
        /// 主机端IP
        /// </summary>
        public static string TeacherIP { get; set; }
        /// <summary>
        /// TCP客户端
        /// </summary>
        public static EduTCPClient client;
        /// <summary>
        /// 是否为会审系统
        /// </summary>
        public static readonly bool IsHuiShenXiTong = true;
        /// <summary>
        /// 登录用户
        /// </summary>
        public static LoginUserInfo LoginUserInfo;
        private static List<ChatStore> _chatList;
        public static List<ChatStore> ChatList
        {
            get
            {
                if (_chatList == null)
                {
                    _chatList = new List<ChatStore>();
                }
                return _chatList;
            }

            //set
            //{
            //    chatList = value;
            //}
        }
        public static bool IsTeamChatChanged { get; set; }



        public static IList<User> OnlineUserList { get; set; }
        private static IList<Team> _teamList = new List<Team>();
        public static IList<Team> TeamList { get { return _teamList; } }
        #endregion


        #region 新方法
        /// <summary>
        /// 更新群组成员在线状态
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isOnline"></param>
        public static void UpdateTeamMemberOnline(string userName, bool isOnline)
        {
            foreach (var _team in _teamList)
            {
                _team.UpdateOnline(userName, isOnline);
            }
        }

        /// <summary>
        /// 更新群组成员在线状态
        /// </summary>
        public static void UpdateAllTeamMemberOnline()
        {
            foreach (var _team in _teamList)
            {
                foreach (var user in OnlineUserList)
                {
                    _team.UpdateOnline(user.UserName, user.IsOnline);
                }

            }
        }

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public static bool CreateTeam(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
            {
                ShowError("组名不能为空！");
                return false;
            }


            if (_teamList.Any(d => d.TeamName == teamName))
            {
                ShowError("组名不能重复！");
                return false;
            }

            Team info = new Team();
            info.TeamId = Guid.NewGuid().ToString();
            info.TeamName = teamName;
            info.AddMember(LoginUserInfo.UserName, LoginUserInfo.DisplayName);
            _teamList.Add(info);
            ShowSuccess("分组建立成功");
            IsTeamChatChanged = true;
            return true;
        }

        /// <summary>
        /// 更改群组名称
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="newName"></param>
        public static bool UpdateTeamName(string teamId, string newName)
        {
            if (_teamList.Any(d => d.TeamName == newName && d.TeamId != teamId))
            {
                ShowError("组名不能重复！");
                return false;
            }

            var team = _teamList.FirstOrDefault(d => d.TeamId == teamId);
            if (team == null)
            {
                ShowError("未找到分组信息！");
                return false;
            }
            team.UpdateTeamName(newName);
            IsTeamChatChanged = true;
            return true;
        }

        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="teamGuid"></param>
        /// <param name="sendDelCommand"></param>
        /// <returns></returns>
        public static bool RemoveTeam(string teamGuid, Action<string, IList<User>> sendDelCommand)
        {
            var team = _teamList.FirstOrDefault(d => d.TeamId == teamGuid);
            if (team == null)
            {
                ShowError("未找到群组信息！");
                return false;
            }
            sendDelCommand(teamGuid, team.TeamMembers);
            _teamList.Remove(team);
            IsTeamChatChanged = true;
            return true;
        }

        /// <summary>
        /// 删除群组成员
        /// </summary>
        /// <param name="teamGuid"></param>
        /// <param name="userName"></param>
        /// <param name="isDeleteTeam"></param>
        /// <returns></returns>
        public static bool RemoveTeamMember(string teamGuid, string userName, bool isDeleteTeam = false)
        {
            var info = _teamList.FirstOrDefault(d => d.TeamId == teamGuid);
            if (info == null)
            {
                ShowError("未找到要删除的分组信息");
                return false;
            }
            if (isDeleteTeam)
            {
                IsTeamChatChanged = true;
                return _teamList.Remove(info);
            }

            var rs = info.RemoveMember(userName);
            if (rs)
            {
                IsTeamChatChanged = true;
            }
            return rs;
        }

        /// <summary>
        /// 添加群组成员
        /// </summary>
        /// <param name="users"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static bool AddTeamMembers(IList<User> users, string teamId)
        {
            var team = FindTeamById(teamId);
            if (team == null)
            {
                ShowError("群组不存在");
                return false;
            }
            team.AddMembers(users);
            IsTeamChatChanged = true;
            return true;
        }

        private static Team FindTeamById(string teamId)
        {
            return _teamList.FirstOrDefault(d => d.TeamId == teamId);
        }

        public static void LoadTeamList(TeacherTeam teamList)
        {

            IsTeamChatChanged = true;
            //  var list = GetTeamChatList();
            foreach (TeamInfo teamInfo in teamList.TeamInfos)
            {
                var team = _teamList.FirstOrDefault(d => d.TeamId == teamInfo.groupid);
                if (team != null)
                {
                    team.TeamName = teamInfo.groupname;
                    team.UpdateTeamMembers(teamInfo.groupuserList);
                }
                else
                {
                    Team info = teamInfo.ConvertToTeam();
                    _teamList.Add(info);

                    //ChatStore info = new ChatStore();
                    //info.ChatDisplayName = teamInfo.groupname;
                    //info.ChatStartTime = DateTime.Now;
                    //info.ChatType = ChatType.TeamChat;
                    //info.ChatUserName = teamInfo.groupid;
                    //info.UserType = ClientRole.Teacher;
                    //info.MessageList = new List<ChatMessage>();
                    //info.TeamMembers = teamInfo.groupuserList;
                    //ChatList.Add(info);
                }
            }



        }

        /// <summary>
        /// 获取某一群组成员列表
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static string GetMemberNames(string teamId)
        {
            var team = FindTeamById(teamId);
            if (team == null)
            {
                return "";
            }
            return string.Join(",", team.TeamMembers.Select(d => d.UserName));
        }

        public static string GetTeamName(string teamId)
        {
            var team = FindTeamById(teamId);
            return team == null ? "" : team.TeamName;
        }

        public static void CreateChatStore(string userName, ChatType chatType)
        {
            if (_chatList.Any(d => d.ChatUserName == userName))
            {
                return;
            }

            ChatStore info = new ChatStore();
            //   info.ChatDisplayName = "全体成员";
            //   info.ChatStartTime = DateTime.Now;
            info.ChatType = chatType;
            info.ChatUserName = userName;
            //  info.UserType = ClientRole.Teacher;
            info.MessageList = new List<ChatMessage>();
            _chatList.Add(info);

        }

        #endregion

        #region 聊天
        public static ChatMessage CreateChatMessage(ReceieveMessage message)
        {
            ChatMessage chatMessage = null;
            switch (message.Action)
            {
                case (int)CommandType.PrivateChat:
                    var privateRequest = JsonHelper.DeserializeObj<PrivateChatRequest>(message.DataStr);
                    chatMessage = privateRequest.ToChatMessage();
                    break;
                case (int)CommandType.TeamChat:
                    var teamRequest = JsonHelper.DeserializeObj<TeamChatRequest>(message.DataStr);
                    chatMessage = teamRequest.ToChatMessage();
                    break;
                case (int)CommandType.GroupChat:
                    var groupRequest = JsonHelper.DeserializeObj<GroupChatRequest>(message.DataStr);
                    chatMessage = groupRequest.ToChatMessage();
                    break;
                default:
                    break;
            }
            AddNewChat(chatMessage);
            return chatMessage;
        }
        #endregion

        #region 群组
        //public static void UpdateTeamOnline(IList<OnlineUserResponse> onLineList)
        //{
        //    var list = GetTeamChatList();
        //    foreach (var onlineUser in onLineList)
        //    {

        //        foreach (ChatStore item in list)
        //        {
        //            foreach (TeamMember mem in item.TeamMembers)
        //            {
        //                if (mem.UserName == onlineUser.username)
        //                {
        //                    mem.IsOnline = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //}

        #endregion




        //public static MyClient client
        //{
        //    get
        //    {
        //        if (_client == null || !_client.Connected)
        //        {
        //            _client = new MyClient();
        //        }
        //        return _client;

        //    }
        //}

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        public static void SendCommand(IsendRequest request, CommandType type)
        {
            if (client == null || !client.ClientIsConnected)
            {
                ShowWarnning("服务端未连接成功，请稍候再试");
                return;
            }
            switch (type)
            {
                case CommandType.UserLogin:
                    client.Send_UserLogin((LoginRequest)request);
                    break;
                case CommandType.UserLoginRes:
                    break;
                case CommandType.OnlineList:
                    break;
                case CommandType.BeginCall:
                    break;
                case CommandType.EndCall:
                    break;
                case CommandType.ScreenInteract:
                    break;
                case CommandType.StopScreenInteract:
                    break;
                case CommandType.Quiet:
                    break;
                case CommandType.StopQuiet:
                    break;
                case CommandType.LockScreen:
                    break;
                case CommandType.StopLockScreen:
                    break;
                case CommandType.PrivateChat:
                    break;
                case CommandType.GroupChat:
                    break;
                case CommandType.CreateTeam:
                    break;
                case CommandType.TeamChat:
                    break;
                case CommandType.OneUserLogIn:
                    break;
                case CommandType.UserLoginOut:
                    break;
                case CommandType.StudentCall:
                    break;
                case CommandType.StudentInMainForm:
                    break;
                case CommandType.CallStudentShow:
                    break;
                case CommandType.StopStudentShow:
                    break;
                case CommandType.ForbidPrivateChat:
                    break;
                case CommandType.AllowPrivateChat:
                    break;
                case CommandType.ForbidTeamChat:
                    break;
                case CommandType.AllowTeamChat:
                    break;
                case CommandType.SendMessageWithFile:
                    break;
                case CommandType.TeacherLoginIn:
                    break;
                case CommandType.TeacherLoginOut:
                    break;
                case CommandType.CallStudentShowForMySelf:
                    break;
                case CommandType.StudentShowToTeacher:
                    break;
                case CommandType.CallStudentShowVideoToTeacher:
                    break;
                case CommandType.XinTiao:
                    break;
                case CommandType.DeleteUserInGroup:
                    client.Send_DeleteGroupMember((DeleteTeamMemberRequest)request);
                    break;
                default:
                    break;
            }
        }





        public static void CreateTestLoginInfo()
        {
            LoginUserInfo = new LoginUserInfo();
            LoginUserInfo.UserName = "teach0011111";
            LoginUserInfo.UserType = ClientRole.Teacher;
            LoginUserInfo.DisplayName = "teach0011111";
            LoginUserInfo.AllowPrivateChat = true;
            LoginUserInfo.AllowTeamChat = true;
            LoginUserInfo.No = "u001";

        }



        //   public static List<ChatStore> ChatList { get; set; }



  

        //public static void AddNewChat(AddChatRequest request)
        //{
        //    ChatStore info = ChatList.FirstOrDefault(d => d.ChatUserName == request.UserName);

        //    if (info == null)
        //    {
        //        info = new ChatStore();
        //        info.ChatDisplayName = request.DisplayName;
        //        info.ChatStartTime = DateTime.Now;
        //        info.ChatType = request.ChatType;
        //        info.ChatUserName = request.UserName;
        //        info.UserType = request.UserType;
        //        info.MessageList = new List<ChatMessage>();
        //        ChatList.Add(info);
        //    }

        //    if (!string.IsNullOrWhiteSpace(request.Message))
        //    {
        //        //ChatBoxContent content = new ChatBoxContent(request.Message, messageFont, messageColor);
        //        var message = request.ToChatMessage();// new ChatMessage(request.UserName, request.DisplayName, LoginUserInfo.UserName, content);

        //        if (info.NewMessageList == null)
        //        {
        //            info.NewMessageList = new List<ChatMessage>();

        //        }
        //        info.NewMessageList.Add(message);
        //    }

        //}

        private static ChatStore CreateChatStore(ChatMessage request)
        {
            ChatStore info = _chatList.FirstOrDefault(d => d.ChatUserName == request.SendUserName);

            if (info == null)
            {
                info = new ChatStore();
                //  info.ChatDisplayName = request.SendDisplayName;
                // info.ChatStartTime = DateTime.Now;
                info.ChatType = request.ChatType;
                info.ChatUserName = request.SendUserName;
                //    info.UserType = request.UserType;
                info.MessageList = new List<ChatMessage>();
                _chatList.Add(info);
            }
            return info;
        }

        public static void AddNewChat(ChatMessage request)
        {
            ChatStore info = CreateChatStore(request);

            if (!string.IsNullOrWhiteSpace(request.Message))
            {
                //ChatBoxContent content = new ChatBoxContent(request.Message, messageFont, messageColor);
                //   var message = request.ToChatMessage();// new ChatMessage(request.UserName, request.DisplayName, LoginUserInfo.UserName, content);

                if (info.NewMessageList == null)
                {
                    info.NewMessageList = new List<ChatMessage>();

                }
                request.SendTime = DateTime.Now;
                info.NewMessageList.Add(request);
            }

        }


        //public static void RefreshTeamMember(string userName, bool isOnline)
        //{
        //    var list = GetTeamChatList();
        //    foreach (ChatStore item in list)
        //    {
        //        foreach (TeamMember mem in item.TeamMembers)
        //        {
        //            if (mem.UserName == userName)
        //            {
        //                mem.IsOnline = isOnline;
        //                break;
        //            }
        //        }
        //    }
        //}

        public static ChatType GetChatType(string userName)
        {
            var info = _chatList.FirstOrDefault(d => d.ChatUserName == userName);
            return info.ChatType;
        }


        //public static ChatMessage ToChatMessage(this AddChatRequest request)
        //{

        //    //   ChatBoxContent content = new ChatBoxContent(request.Message, messageFont, messageColor);
        //    return new ChatMessage(request.UserName, request.DisplayName, LoginUserInfo.UserName, request.Message, request.UserType);
        //}

        static Font messageFont = new Font("微软雅黑", 9);
        static Color messageColor = Color.FromArgb(255, 32, 32, 32);
        //private static void SaveChatMessage(AddChatRequest request)
        //{
        //    if (!string.IsNullOrWhiteSpace(request.Message))
        //    {
        //        ChatBoxContent content = new ChatBoxContent(request.Message, messageFont, messageColor);
        //        var message = new ChatMessage(request.UserName, request.DisplayName, LoginUserInfo.UserName, content);
        //        SaveNewChatMessage(message, false);
        //    }
        //}

        //public static ChatStore GetChatStore(string userName)
        //{
        //    return ChatList.FirstOrDefault(d => d.ChatUserName == userName);
        //}
        //public static void SaveNewChatMessage(ChatMessage message, bool isSend)
        //{
        //    string userName = isSend ? message.ReceieveUserName : message.SendUserName;

        //    var chat = ChatList.FirstOrDefault(d => d.ChatUserName == userName);
        //    if (chat == null)
        //    {
        //        return;
        //    }
        //    if (chat.MessageList == null)
        //    {
        //        chat.MessageList = new List<ChatMessage>();
        //    }
        //    chat.MessageList.Add(message);
        //}



        public static void SaveChatMessage(smsPanel content, string userName)
        {
            var chatstore = _chatList.FirstOrDefault(d => d.ChatUserName == userName);
            if (chatstore != null)
            {
                chatstore.HistoryMessagePanel = content;
                chatstore.NewMessageList = null;
            }
        }


        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarnning(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowSuccess(string msg)
        {
            MessageBox.Show(msg, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //public static IList<ChatStore> GetTeamChatList()
        //{
        //    return ChatList.Where(d => d.ChatType == ChatType.TeamChat).ToList();
        //}
        public static Team GetNewestTeamChat()
        {
            return _teamList.Last();
        }

        //public static IList<string> GetTeamMemberDisplayNames(string userName)
        //{
        //    var chatStore = ChatList.FirstOrDefault(d => d.ChatUserName == userName);
        //    return chatStore.TeamMembers.Select(d => d.DisplayName).ToList();
        //}

        //public static ChatStore GetChatStoreByUserName(string userName)
        //{
        //    return ChatList.FirstOrDefault(d => d.ChatUserName == userName);
        //}
        //public static IList<ChatMessage> GetNewMessageList(string userName)
        //{
        //    var chat = ChatList.FirstOrDefault(d => d.ChatUserName == userName);
        //    if (chat != null)
        //    {
        //        return chat.NewMessageList;
        //    }
        //    return null;
        //}


        //public static bool CreateTeamChat(string teamName)
        //{
        //    if (string.IsNullOrWhiteSpace(teamName))
        //    {
        //        ShowError("组名不能为空！");
        //        return false;
        //    }


        //    if (ChatList.Any(d => d.ChatDisplayName == teamName))
        //    {
        //        ShowError("组名不能重复！");
        //        return false;
        //    }


        //    ChatStore info = new ChatStore();
        //    info.ChatDisplayName = teamName;
        //    info.ChatStartTime = DateTime.Now;
        //    info.ChatType = ChatType.TeamChat;
        //    info.ChatUserName = Guid.NewGuid().ToString();
        //    info.UserType = ClientRole.Teacher;
        //    info.MessageList = new List<ChatMessage>();
        //    ChatList.Add(info);
        //    ShowSuccess("分组建立成功");
        //    IsTeamChatChanged = true;
        //    AddLoginUserToMember(info.ChatUserName);
        //    return true;
        //}

        public static ChatStore CreateGroupChat(string groupId)
        {
            if (_chatList.Any(d => d.ChatType == ChatType.GroupChat))
            {
                return null;
            }


            ChatStore info = new ChatStore();
            //   info.ChatDisplayName = "全体成员";
            //   info.ChatStartTime = DateTime.Now;
            info.ChatType = ChatType.GroupChat;
            info.ChatUserName = groupId;
            //    info.UserType = ClientRole.Teacher;
            info.MessageList = new List<ChatMessage>();
            _chatList.Add(info);
            IsTeamChatChanged = true;
            return info;
        }



        //public static bool AddTeamMember(CheckedListViewItemCollection mems, string guid)
        //{

        //    var info = ChatList.FirstOrDefault(d => d.ChatUserName == guid && d.ChatType == ChatType.TeamChat);
        //    if (info == null)
        //    {
        //        ShowError("未找到添加的分组信息");
        //        return false;
        //    }
        //    string userName = "";
        //    string displayName = "";
        //    foreach (ListViewItem item in mems)
        //    {
        //        userName = item.SubItems[1].Text;
        //        displayName = item.Text;
        //        if (!info.TeamMembers.Any(d => d.UserName == userName))
        //        {
        //            info.TeamMembers.Add(new TeamMember { UserName = userName, DisplayName = displayName, IsOnline = true });
        //        }
        //    }
        //    IsTeamChatChanged = true;
        //    //  ShowSuccess("分组成员添加成功");
        //    return true;
        //}




        //private static bool AddLoginUserToMember(string guid)
        //{
        //    var info = ChatList.FirstOrDefault(d => d.ChatUserName == guid && d.ChatType == ChatType.TeamChat);
        //    if (info == null)
        //    {
        //        ShowError("未找到添加的分组信息");
        //        return false;
        //    }
        //    if (!info.TeamMembers.Any(d => d.UserName == LoginUserInfo.UserName))
        //    {
        //        info.TeamMembers.Add(new TeamMember { UserName = LoginUserInfo.UserName, DisplayName = LoginUserInfo.DisplayName, IsOnline = true });
        //    }
        //    return true;
        //}

        //public static bool DelTeamMember(string teamGuid, string userName, bool isDeleteTeam = false)
        //{
        //    var info = ChatList.FirstOrDefault(d => d.ChatUserName == teamGuid && d.ChatType == ChatType.TeamChat);
        //    if (info == null)
        //    {
        //        ShowError("未找到要删除的分组信息");
        //        return false;
        //    }
        //    if (isDeleteTeam)
        //    {
        //        IsTeamChatChanged = true;
        //        return ChatList.Remove(info);
        //    }

        //    var mem = info.TeamMembers.FirstOrDefault(d => d.UserName == userName);
        //    if (mem == null)
        //    {
        //        return false;
        //    }
        //    IsTeamChatChanged = true;
        //    return info.TeamMembers.Remove(mem);
        //}

        public static bool DelTeamMember(DeleteTeamMemberRequest info)
        {
            return RemoveTeamMember(info.TeamId, info.UserName, info.IsDeleteTeam);
        }

        //public static bool EditTeamName(string teamGuid, string newName)
        //{
        //    if (ChatList.Any(d => d.ChatDisplayName == newName && d.ChatUserName != teamGuid))
        //    {
        //        ShowError("组名不能重复！");
        //        return false;
        //    }
        //    var item = ChatList.FirstOrDefault(d => d.ChatUserName == teamGuid);
        //    if (item == null)
        //    {
        //        ShowError("未找到分组信息！");
        //        return false;
        //    }

        //    item.ChatDisplayName = newName;
        //    IsTeamChatChanged = true;
        //    return true;

        //}

        //public static bool DelTeam(string teamGuid, Action<string, IList<TeamMember>> sendDelCommand)
        //{
        //    var item = ChatList.FirstOrDefault(d => d.ChatUserName == teamGuid);
        //    if (item == null)
        //    {
        //        ShowError("未找到分组信息！");
        //        return false;
        //    }
        //    sendDelCommand(teamGuid, item.TeamMembers);
        //    ChatList.Remove(item);
        //    IsTeamChatChanged = true;
        //    return true;
        //}

        //public static void RefleshTeamList(TeacherTeam teamList)
        //{

        //    IsTeamChatChanged = true;
        //    var list = GetTeamChatList();

        //    foreach (TeamInfo teamInfo in teamList.TeamInfos)
        //    {
        //        var chatStore = list.FirstOrDefault(d => d.ChatUserName == teamInfo.groupid);
        //        if (chatStore != null)
        //        {
        //            chatStore.ChatDisplayName = teamInfo.groupname;
        //            chatStore.TeamMembers = teamInfo.groupuserList;
        //        }
        //        else
        //        {
        //            ChatStore info = new ChatStore();
        //            info.ChatDisplayName = teamInfo.groupname;
        //            info.ChatStartTime = DateTime.Now;
        //            info.ChatType = ChatType.TeamChat;
        //            info.ChatUserName = teamInfo.groupid;
        //            info.UserType = ClientRole.Teacher;
        //            info.MessageList = new List<ChatMessage>();
        //            info.TeamMembers = teamInfo.groupuserList;
        //            ChatList.Add(info);
        //        }
        //    }



        //}

        public static bool CheckChatFormIsOpened()
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == "ChatForm")
                {
                    return true;
                }
            }
            return false;
        }


        //public static TeacherTeam GetTeacherTeamFromChatStore()
        //{
        //    var team = new TeacherTeam();
        //    team.DisplayName = LoginUserInfo.DisplayName;
        //    team.UserName = LoginUserInfo.UserName;
        //    team.TeamInfos = new List<TeamInfo>();
        //  //  var list = GetTeamChatList();
        //    TeamInfo info;
        //    foreach (var item in _teamList)
        //    {
        //        info = new TeamInfo();
        //        info.groupname = item.TeamName;
        //        info.groupid = item.TeamId;
        //        info.groupuserList = item.TeamMembers.ConvertToTeamMember();
        //        team.TeamInfos.Add(info);
        //    }
        //    return team;
        //}


        public static void SendCommand_CreateOrUpdateTeam()
        {
            var request = _teamList.ConvertToTeacherTeam(LoginUserInfo.UserName, LoginUserInfo.DisplayName);
            CreateTeamXMLFile(request);
            client.Send_CreateTeam(request);
            //TeacherTeam request = new TeacherTeam();
            //request.DisplayName = LoginUserInfo.DisplayName;
            //request.UserName = LoginUserInfo.UserName;
            //request.TeamInfos = new List<TeamInfo>();
            //var list = GetTeamChatList();
            //SaveTeamXML(list);
            //TeamInfo info;
            //foreach (ChatStore item in list)
            //{
            //    info = new TeamInfo();
            //    info.groupname = item.ChatDisplayName;
            //    info.groupid = item.ChatUserName;
            //    info.groupuserList = item.TeamMembers.ToList();
            //    request.TeamInfos.Add(info);
            //}
            //client.Send_CreateTeam(request);
        }

        //public static void SaveTeamInfoToFile()
        //{
        //    var request = _teamList.ConvertToTeacherTeam(LoginUserInfo.UserName, LoginUserInfo.DisplayName);
        //    CreateTeamXMLFile(request);
        //}

        private static void CreateTeamXMLFile(TeacherTeam info)
        {
            //var info = new TeacherTeam();
            //info.DisplayName = LoginUserInfo.DisplayName;
            //info.UserName = LoginUserInfo.UserName;
            //info.TeamInfos = new List<TeamInfo>();
            //foreach (ChatStore chat in teamChatList)
            //{
            //    TeamInfo team = new TeamInfo();
            //    team.groupid = chat.ChatUserName;
            //    team.groupname = chat.ChatDisplayName;
            //    team.groupuserList = chat.TeamMembers.ToList();
            //    info.TeamInfos.Add(team);
            //}

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TeamXML");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.Combine(path, "群组" + info.UserName + ".xml");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            XmlHelper.SerializerToFile(info, fileName);

        }

        //private static void CreateTeamXMLFile()
        //{
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TeamXML");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    string fileName = Path.Combine(path, "群组" + LoginUserInfo.UserName + ".xml");
        //    if (File.Exists(fileName))
        //    {
        //        File.Delete(fileName);
        //    }
        //    XmlHelper.SerializerToFile(_teamList, fileName);
        //}

        public static void LoadTeamFromXML()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TeamXML");
            if (!Directory.Exists(path))
            {
                return;
            }
            string fileName = Path.Combine(path, "群组" + LoginUserInfo.UserName + ".xml");
            if (!File.Exists(fileName))
            {
                return;
            }
            var teams = XmlHelper.DeserializeFromFile<TeacherTeam>(fileName);
            //  TeacherTeam loadTeam = new TeacherTeam();
            //  loadTeam.TeamInfos = info.Teams;
            LoadTeamList(teams);

        }

        static NotifyForm notifyForm;
        public static void ShowNotifyMessage(string message, int dueSecond = 5)
        {

            if (notifyForm == null || notifyForm.IsDisposed)
            {
                notifyForm = new NotifyForm(message, dueSecond);
            }
            else
            {
                notifyForm.SetMessage(message, dueSecond);
            }
            notifyForm.Show();
        }


        //public static string GetTeamNameByTeamId(string teamId)
        //{
        //    var chatStore = ChatList.FirstOrDefault(d => d.ChatUserName == teamId && d.ChatType == ChatType.TeamChat);
        //    return chatStore == null ? "" : chatStore.ChatDisplayName;
        //}

        public static void ShowChatMessageNotify(ChatMessage message, ChatForm chatForm)
        {
            string text = "";
            switch (message.ChatType)
            {
                case ChatType.PrivateChat:
                    text = message.SendDisplayName + "给您发送了一条私聊信息";
                    break;
                case ChatType.GroupChat:
                    text = message.SendDisplayName + "发送了一条全体信息";
                    break;
                case ChatType.TeamChat:
                    text = message.SendDisplayName + "在群组【" + GetTeamName(message.SendUserName) + "】发送了一条信息";
                    break;
                default:
                    break;
            }
            ShowNotifyMessage(text);
            notifyForm.SetMessageOpen(chatForm);
        }


        //public static void CloseNotifyMessage()
        //{
        //    if (notifyForm != null)
        //    {
        //        notifyForm.Close();
        //    }
        //}


        public static string BeginRecordVideo()
        {
            string fileName = Path.Combine(VideoRecordPath, DateTime.Now.Ticks.ToString() + ".mpg");

            client.BeginRecordVideo(fileName);
            return fileName;
        }
        public static void EndRecordVideo()
        {
            client.EndRecordVideo();
        }


        public static void KillAllFFmpeg()
        {
            Process killFfmpeg = new Process();
            ProcessStartInfo taskkillStartInfo = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM ffmpeg.exe",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            killFfmpeg.StartInfo = taskkillStartInfo;
            killFfmpeg.Start();
            killFfmpeg.WaitForExit();
        }




    }
}
