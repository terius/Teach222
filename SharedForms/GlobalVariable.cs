using Common;
using EduService;
using Helpers;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
        public static readonly bool IsHuiShenXiTong = System.Configuration.ConfigurationManager.AppSettings["IsHuiShen"] == "1" ? true : false;


        static readonly string msgSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "msg.wav");

        public static string SystemTitle
        {
            get
            {
                if (IsHuiShenXiTong)
                {
                    return "会审系统";
                }
                return "在线教育系统";
            }
        }

        public static string MasterTitle
        {
            get
            {
                if (IsHuiShenXiTong)
                {
                    return "指挥室";
                }
                return "老师";
            }
        }


        public static string ClientTitle
        {
            get
            {
                if (IsHuiShenXiTong)
                {
                    return "审讯室";
                }
                return "学生";
            }
        }



        /// <summary>
        /// 登录用户
        /// </summary>
        public static LoginUserInfo LoginUserInfo;
        private static List<ChatStore> _chatList = new List<ChatStore>();
        public static List<ChatStore> ChatList
        {
            get
            {
                return _chatList;
            }
        }
        public static bool IsTeamChatChanged { get; set; }
        public static IList<User> OnlineUserList { get; set; }
        private static IList<Team> _teamList = new List<Team>();
        public static IList<Team> TeamList { get { return _teamList; } }
        #endregion


        #region 群组方法
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

        /// <summary>
        /// 加载群组列表
        /// </summary>
        /// <param name="teachTeam"></param>
        public static void LoadTeamList(TeacherTeam teachTeam)
        {

            IsTeamChatChanged = true;
            //  var list = GetTeamChatList();
            foreach (TeamInfo teamInfo in teachTeam.TeamInfos)
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
                }
            }



        }

        /// <summary>
        /// 获取某一群组成员姓名列表
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



        #endregion

        ///// <summary>
        ///// 创建ChatStore
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="chatType"></param>
        //public static void CreateChatStore(string userName, ChatType chatType)
        //{
        //    if (_chatList.Any(d => d.ChatUserName == userName))
        //    {
        //        return;
        //    }
        //    ChatStore info = new ChatStore();
        //    info.ChatType = chatType;
        //    info.ChatUserName = userName;
        //    info.MessageList = new List<ChatMessage>();
        //    _chatList.Add(info);

        //}


        public static ChatStore GetOrCreateChatStore(string sendUserName, ChatType chatType)
        {
            ChatStore info = _chatList.FirstOrDefault(d => d.ChatUserName == sendUserName);

            if (info == null)
            {
                info = new ChatStore();
                info.ChatType = chatType;
                info.ChatUserName = sendUserName;
                info.MessageList = new List<ChatMessage>();
                _chatList.Add(info);
            }
            return info;
        }

        public static ChatMessage CreateChatMessage(ReceieveMessage message)
        {
            PlayMsgVoice();
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

        static System.Media.SoundPlayer player = new System.Media.SoundPlayer(msgSound);
        public static void PlayMsgVoice()
        {
            player.Play();
        }




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


        /// <summary>
        /// 测试用
        /// </summary>
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




        public static void AddNewChat(ChatMessage request)
        {
            ChatStore info = GetOrCreateChatStore(request.SendUserName, request.ChatType);
            if (!string.IsNullOrWhiteSpace(request.Message))
            {
                if (info.NewMessageList == null)
                {
                    info.NewMessageList = new List<ChatMessage>();

                }
                request.SendTime = DateTime.Now;
                info.NewMessageList.Add(request);
            }

        }




        public static ChatType GetChatType(string userName)
        {
            var info = _chatList.FirstOrDefault(d => d.ChatUserName == userName);
            return info.ChatType;
        }




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

        public static Team GetNewestTeamChat()
        {
            return _teamList.Last();
        }


        public static ChatStore CreateGroupChat(string groupId)
        {
            if (_chatList.Any(d => d.ChatType == ChatType.GroupChat))
            {
                return null;
            }
            ChatStore info = new ChatStore();
            info.ChatType = ChatType.GroupChat;
            info.ChatUserName = groupId;
            info.MessageList = new List<ChatMessage>();
            _chatList.Add(info);
            IsTeamChatChanged = true;
            return info;
        }


        public static bool DelTeamMember(DeleteTeamMemberRequest info)
        {
            return RemoveTeamMember(info.TeamId, info.UserName, info.IsDeleteTeam);
        }


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



        public static void SendCommand_CreateOrUpdateTeam()
        {
            var request = _teamList.ConvertToTeacherTeam(LoginUserInfo.UserName, LoginUserInfo.DisplayName);
            CreateTeamXMLFile(request);
            client.Send_CreateTeam(request);
        }


        private static void CreateTeamXMLFile(TeacherTeam info)
        {

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


        public static void BeginRecordVideo()
        {
            string fileName = Path.Combine(VideoRecordPath, DateTime.Now.Ticks.ToString() + ".mpg");
            client.BeginRecordVideo(fileName);

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
