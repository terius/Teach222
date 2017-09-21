using Common;
using Helpers;
using Model;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EduService
{
    public class EduTCPClient
    {
        EasyClient client;
        public delegate void ReceiveHandle(ReceieveMessage message);
        // internal ReceiveHandle _delegate;
        public event ReceiveHandle OnReveieveData;
        public event EventHandler OnClentIsConnecting;
        //{
        //    add { _delegate += value; }
        //    remove { _delegate -= value; }
        //}
        readonly string serverIP = System.Configuration.ConfigurationManager.AppSettings["serverIP"];
        readonly int serverPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["serverPort"]);
        bool _connected;
        EduVideoClient _screenInteract;
    
        ProgramType programType;
        bool isConnecting = false;
        bool _ffmpegIsRun;

        


        public bool ClientIsConnected
        {
            get
            {
                if (client == null)
                {
                    return false;
                }
                return client.IsConnected;
            }


        }


 

        private void Client_Error(object sender, ErrorEventArgs e)
        {
           ConnectToServer();
        }

        private void ConnectToServer()
        {
            if (!isConnecting)
            {
                isConnecting = true;
                int rootNum = 0;
                do
                {
                    if (rootNum >= 5)
                    {
                        isConnecting = false;
                        break;
                        throw new Exception("服务已断开连接");

                    }
                    _connected = client.ConnectAsync(new IPEndPoint(IPAddress.Parse(serverIP), serverPort)).Result;
                    rootNum++;

                } while (client == null || !client.IsConnected);
                isConnecting = false;
            }


        }

        public EduTCPClient(ProgramType _programType)
        {
            programType = _programType;
            client = new EasyClient();
            client.Initialize(new MyReceiveFilter(), (response) =>
            {
                try
                {
                    string text = StringHelper.GetEnumDescription((CommandType)response.Action);
                    Loger.LogMessage("收到消息【" + ((CommandType)response.Action).ToString() + " " + text + "】：" + JsonHelper.SerializeObj(response));
                    OnReveieveData(response);
                }
                catch (Exception ex)
                {
                    Loger.LogMessage(ex.ToString());
                }

            });
            if (programType == ProgramType.Student)
            {
                OnReveieveData += Student_OnReveieveData;
            }
            else
            {
                OnReveieveData += Teacher_OnReveieveData;
            }
            //  client.Error += Client_Error;
            ConnectToServer();
        }



        public void DueLostMessage()
        {
            foreach (var lostMsg in UnDueMessages)
            {
                OnReveieveData(lostMsg);
            }
            UnDueMessages.Clear();
        }



        private IList<ReceieveMessage> UnDueMessages = new List<ReceieveMessage>();
        public Action<ReceieveMessage> OnUserLoginRes;
        public Action<ReceieveMessage> OnTeacherLoginIn;
        public Action<ReceieveMessage> OnTeacherLoginOut;
        public Action<ReceieveMessage> OnBeginCall;
        public Action<ReceieveMessage> OnPrivateChat;
        public Action<ReceieveMessage> OnScreenInteract;
        public Action<ReceieveMessage> OnStopScreenInteract;
        public Action<ReceieveMessage> OnLockScreen;
        public Action<ReceieveMessage> OnStopLockScreen;
        public Action<ReceieveMessage> OnQuiet;
        public Action<ReceieveMessage> OnStopQuiet;
        public Action<ReceieveMessage> OnTeamChat;
        public Action<ReceieveMessage> OnGroupChat;
        public Action<ReceieveMessage> OnEndCall;
        public Action<ReceieveMessage> OnCreateTeam;
        public Action<ReceieveMessage> OnCallStudentShow;
        public Action<ReceieveMessage> OnCallStudentShowForTeacher;
        public Action<ReceieveMessage> OnCallStudentShowVideoForTeacher;
        public Action<ReceieveMessage> OnStopStudentShow;
        public Action<ReceieveMessage> OnForbidPrivateChat;
        public Action<ReceieveMessage> OnAllowPrivateChat;
        public Action<ReceieveMessage> OnForbidTeamChat;
        public Action<ReceieveMessage> OnAllowTeamChat;

        //主机端
        public Action<ReceieveMessage> OnOnlineList;
        public Action<ReceieveMessage> OnOneUserLogIn;
        public Action<ReceieveMessage> OnStudentCall;
        public Action<ReceieveMessage> OnUserLoginOut;

        private void Student_OnReveieveData(ReceieveMessage message)
        {
            switch (message.Action)
            {
                case (int)CommandType.UserLoginRes:
                    DueMessage(OnUserLoginRes, message);

                    break;
                case (int)CommandType.TeacherLoginIn://主机端登录
                    DueMessage(OnTeacherLoginIn, message);
                    break;
                case (int)CommandType.TeacherLoginOut://主机端登出
                    DueMessage(OnTeacherLoginOut, message);
                    break;
                case (int)CommandType.ScreenInteract://推送视频流
                    DueMessage(OnScreenInteract, message);
                    break;
                case (int)CommandType.StopScreenInteract://停止视频流
                    DueMessage(OnStopScreenInteract, message);
                    break;
                case (int)CommandType.LockScreen://锁屏
                    DueMessage(OnLockScreen, message);
                    break;
                case (int)CommandType.StopLockScreen://终止锁屏
                    DueMessage(OnStopLockScreen, message);
                    break;
                case (int)CommandType.Quiet://屏幕肃静
                    DueMessage(OnQuiet, message);
                    break;
                case (int)CommandType.StopQuiet://终止屏幕肃静
                    DueMessage(OnStopQuiet, message);
                    break;
                case (int)CommandType.PrivateChat://收到私聊信息
                    DueMessage(OnPrivateChat, message);
                    break;
                case (int)CommandType.TeamChat://收到组聊信息
                    DueMessage(OnTeamChat, message);
                    break;
                case (int)CommandType.GroupChat://收到群聊信息
                    DueMessage(OnGroupChat, message);
                    break;
                case (int)CommandType.BeginCall://开始点名
                    DueMessage(OnBeginCall, message);
                    break;
                case (int)CommandType.EndCall://结束点名
                    DueMessage(OnEndCall, message);
                    break;
                case (int)CommandType.CreateTeam://收到创建群组信息
                    DueMessage(OnCreateTeam, message);
                    break;
                case (int)CommandType.CallStudentShow://收到请求客户端演示
                    DueMessage(OnCallStudentShow, message);
                    break;
                case (int)CommandType.CallStudentShowForMySelf://收到请求客户端演示
                    DueMessage(OnCallStudentShowForTeacher, message);
                    break;
                case (int)CommandType.CallStudentShowVideoToTeacher://收到请求客户端演示视频
                    DueMessage(OnCallStudentShowVideoForTeacher, message);
                    break;
                case (int)CommandType.StopStudentShow://停止演示
                    DueMessage(OnStopStudentShow, message);
                    break;
                case (int)CommandType.ForbidPrivateChat://收到禁止私聊
                    DueMessage(OnForbidPrivateChat, message);
                    break;
                case (int)CommandType.ForbidTeamChat://收到禁止群聊
                    DueMessage(OnForbidTeamChat, message);
                    break;
                case (int)CommandType.AllowPrivateChat://收到允许私聊
                    DueMessage(OnAllowPrivateChat, message);
                    break;
                case (int)CommandType.AllowTeamChat://收到允许群聊
                    DueMessage(OnAllowTeamChat, message);
                    break;
                default:
                    break;
            }

        }


        private void Teacher_OnReveieveData(ReceieveMessage message)
        {
            switch ((CommandType)message.Action)
            {
                case CommandType.UserLoginRes:
                    DueMessage(OnUserLoginRes, message);
                    break;
                case CommandType.OnlineList:
                    DueMessage(OnOnlineList, message);
                    break;

                case CommandType.ScreenInteract:
                    DueMessage(OnScreenInteract, message);
                    break;
                case CommandType.StopScreenInteract:
                    DueMessage(OnStopScreenInteract, message);
                    break;

                case CommandType.PrivateChat:
                    DueMessage(OnPrivateChat, message);
                    break;
                case CommandType.GroupChat:
                    DueMessage(OnGroupChat, message);
                    break;

                case CommandType.TeamChat:
                    DueMessage(OnTeamChat, message);
                    break;
                case CommandType.OneUserLogIn:
                    DueMessage(OnOneUserLogIn, message);
                    break;
                case CommandType.UserLoginOut:
                    DueMessage(OnUserLoginOut, message);
                    break;
                case CommandType.StudentCall:
                    DueMessage(OnStudentCall, message);
                    break;
                case CommandType.StudentShowToTeacher:
                    DueMessage(OnScreenInteract, message);
                    break;
                default:
                    break;
            }

        }

        private void DueMessage(Action<ReceieveMessage> action, ReceieveMessage message)
        {
            if (action == null)
            {
                UnDueMessages.Add(message);
                return;
            }
            action(message);
        }



        //public bool IsEventHandlerRegistered(Delegate prospectiveHandler)
        //{
        //    if (this.OnReveieveData != null)
        //    {
        //        foreach (Delegate existingHandler in this.OnReveieveData.GetInvocationList())
        //        {
        //            if (existingHandler == prospectiveHandler)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}


        public void SendMessage<T>(SendMessage<T> message) where T : class, new()
        {
            if (!client.IsConnected)
            {
                OnClentIsConnecting?.Invoke(this, null);
                ConnectToServer();
                //  CreateSocketClient();
            }
            if (client.IsConnected)
            {
                string text = StringHelper.GetEnumDescription((CommandType)message.Action);
                Loger.LogMessage("发送信息【" + ((CommandType)message.Action).ToString() + " " + text + "】：" + JsonHelper.SerializeObj(message));
                var messageByte = CreateSendMessageByte(message);
                client.Send(messageByte);
            }



        }


        private void SendXinTiao()
        {
            ThreadPool.QueueUserWorkItem((ob) =>
            {
                while (true)
                {
                    if (client.IsConnected)
                    {
                        SendMessageNoPara(CommandType.XinTiao);
                    }
                    Thread.Sleep(5000);
                }

            });

        }

        public async Task<bool> Close()
        {
            return await client.Close();
        }

        public void CreateScreenInteract()
        {
            if (_screenInteract == null)
            {
                var local = (IPEndPoint)client.LocalEndPoint;
                var ipv4 = local.Address.ToString();
                string localIP = ipv4.Substring(ipv4.LastIndexOf(':') + 1);// this.client.LocalEndPoint
                int localPort = local.Port;// local.Port;// this.client.LocalEndPoint.AddressFamily.;
                _screenInteract = new EduVideoClient(serverIP, localIP, localPort);
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void StopScreenInteract()
        {
            if (_screenInteract == null)
            {
                return;
            }
            _screenInteract.stopScreenInteract();
        }

        private byte[] CreateSendMessageByte<T>(SendMessage<T> message) where T : class, new()
        {
            string jsonString = JsonHelper.SerializeObj(message.Data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(jsonString);
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            byte[] timeBytes = Encoding.UTF8.GetBytes(time);
            var actionBytes = BitConverter.GetBytes(message.Action);
            var lengthByte = BitConverter.GetBytes(dataBytes.Length + timeBytes.Length + actionBytes.Length);// StringHelper.ConvertIntToByteArray4(dataBytes.Length + 18, ref buf);

            List<byte> byteSource = new List<byte>();
            byteSource.AddRange(lengthByte);
            byteSource.AddRange(timeBytes);
            byteSource.AddRange(actionBytes);
            byteSource.AddRange(dataBytes);
            return byteSource.ToArray();
        }


        public void BeginRecordVideo(string path)
        {
            if (!_ffmpegIsRun)
            {
                _ffmpegIsRun = true;
                CreateScreenInteract();
                _screenInteract.BeginRecordVideo(path);
            }
        }

        public void EndRecordVideo()
        {
            if (_ffmpegIsRun)
            {
                _screenInteract.EndRecordVideo();
                _ffmpegIsRun = false;
            }
        }


        public void KillAllFFmpeg()
        {
            CreateScreenInteract();
            _screenInteract.KillAllFFMPEG();
        }


        #region 发送命令
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <param name="clientRole"></param>
        public void Send_UserLogin(string userName, string nickName, string password, ClientRole clientRole)
        {
            var loginInfo = new LoginInfo();
            loginInfo.username = userName;
            loginInfo.nickname = nickName;
            loginInfo.no = password;
            loginInfo.clientRole = clientRole;
            loginInfo.clientStyle = ClientStyle.PC;
            SendMessage(loginInfo, CommandType.UserLogin);
        }


        public void Send_UserLogin(LoginRequest request)
        {
            var loginInfo = new LoginInfo();
            loginInfo.username = request.userName;
            loginInfo.nickname = request.nickName;
            loginInfo.no = request.password;
            loginInfo.clientRole = request.ClientRole;
            loginInfo.clientStyle = ClientStyle.PC;
            SendMessage(loginInfo, CommandType.UserLogin);
        }


        /// <summary>
        /// 显示当前在线用户
        /// </summary>
        public void Send_OnlineList()
        {
            SendMessageNoPara(CommandType.OnlineList);
        }
        /// <summary>
        /// 屏幕广播
        /// </summary>
        public void Send_ScreenInteract()
        {
            string rtspAddress = _screenInteract.beginScreenInteract();
            var request = new ScreenInteract_Request { url = rtspAddress };
            SendMessage(request, CommandType.ScreenInteract);
        }


        public void Send_StudentShowToTeacher()
        {
            string rtspAddress = _screenInteract.beginScreenInteract();
            var request = new ScreenInteract_Request { url = rtspAddress };
            SendMessage(request, CommandType.StudentShowToTeacher);
        }

        public void Send_StudentShowVideoToTeacher()
        {
            string rtspAddress = _screenInteract.beginVideoInteract();
            var request = new ScreenInteract_Request { url = rtspAddress };
            SendMessage(request, CommandType.StudentShowToTeacher);
        }

        /// <summary>
        /// 视频直播
        /// </summary>
        public void Send_VideoInteract()
        {
            string rtspAddress = _screenInteract.beginVideoInteract();
            var request = new ScreenInteract_Request { url = rtspAddress };
            SendMessage(request, CommandType.ScreenInteract);
        }

        /// <summary>
        /// 停止屏幕广播
        /// </summary>
        public void Send_StopScreenInteract()
        {
            SendMessageNoPara(CommandType.StopScreenInteract);

        }
        /// <summary>
        /// 停止视频直播
        /// </summary>
        public void Send_StopVideoInteract()
        {
            SendMessageNoPara(CommandType.StopScreenInteract);

        }
        /// <summary>
        /// 锁屏
        /// </summary>
        /// <param name="userName"></param>
        public void Send_LockScreen(string userName)
        {
            var request = new LockScreenRequest { receivename = userName };
            SendMessage(request, CommandType.LockScreen);

        }

        /// <summary>
        /// 停止锁屏
        /// </summary>
        /// <param name="userName"></param>
        public void Send_StopLockScreen(string userName)
        {
            var request = new LockScreenRequest { receivename = userName };
            SendMessage(request, CommandType.StopLockScreen);

        }

        /// <summary>
        /// 屏幕肃静
        /// </summary>
        public void Send_Quiet()
        {
            SendMessageNoPara(CommandType.Quiet);

        }

        /// <summary>
        /// 结束屏幕肃静
        /// </summary>
        public void Send_StopQuiet()
        {
            SendMessageNoPara(CommandType.StopQuiet);
        }

        /// <summary>
        /// 私聊
        /// </summary>
        /// <param name="request"></param>
        public void Send_PrivateChat(PrivateChatRequest request)
        {
            SendMessage(request, CommandType.PrivateChat);
        }


        /// <summary>
        /// 群组聊天
        /// </summary>
        /// <param name="request"></param>
        public void Send_TeamChat(TeamChatRequest request)
        {
            SendMessage(request, CommandType.TeamChat);
        }


        /// <summary>
        /// 全体成员聊天
        /// </summary>
        /// <param name="sendName"></param>
        /// <param name="msg"></param>
        public void Send_GroupChat(GroupChatRequest request)
        {

            SendMessage(request, CommandType.GroupChat);
        }
        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="request"></param>
        public void Send_CreateTeam(TeacherTeam request)
        {
            SendMessage(request, CommandType.CreateTeam);
        }
        /// <summary>
        /// 课堂点名
        /// </summary>
        /// <param name="sendName"></param>
        /// <param name="msg"></param>
        public void Send_Call()
        {
            SendMessageNoPara(CommandType.BeginCall);

        }

        /// <summary>
        /// 结束课堂点名
        /// </summary>
        public void Send_EndCall()
        {
            SendMessageNoPara(CommandType.EndCall);

        }

        /// <summary>
        /// 客户端提交点名信息
        /// </summary>
        public void Send_StudentCall(string no, string name, string userName)
        {
            var request = new StuCallRequest { name = name, no = no, username = userName };
            SendMessage(request, CommandType.StudentCall);

        }

        /// <summary>
        /// 客户端登录完成已进入主页面
        /// </summary>
        public void Send_StudentInMainForm()
        {
            SendMessageNoPara(CommandType.StudentInMainForm);
        }

        /// <summary>
        /// 呼叫客户端演示
        /// </summary>
        /// <param name="userName"></param>
        public void Send_CallStudentShow(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.CallStudentShow);
        }

        /// <summary>
        /// 呼叫客户端演示(只给主机端)
        /// </summary>
        /// <param name="userName"></param>
        public void Send_CallStudentShowForMySelf(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.CallStudentShowForMySelf);
        }

        /// <summary>
        /// 呼叫客户端演示视频(只给主机端)
        /// </summary>
        /// <param name="userName"></param>
        public void Send_CallStudentShowVideoForMySelf(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.CallStudentShowVideoToTeacher);
        }

        /// <summary>
        /// 关闭客户端演示
        /// </summary>
        /// <param name="userName"></param>
        public void Send_StopStudentShow(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.StopStudentShow);
        }

        /// <summary>
        /// 禁止客户端私聊
        /// </summary>
        /// <param name="userName"></param>
        public void Send_ForbidPrivateChat(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.ForbidPrivateChat);
        }
        /// <summary>
        /// 允许客户端私聊
        /// </summary>
        /// <param name="userName"></param>
        public void Send_AllowPrivateChat(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.AllowPrivateChat);
        }
        /// <summary>
        /// 禁止群聊
        /// </summary>
        /// <param name="userName"></param>
        public void Send_ForbidTeamChat(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.ForbidTeamChat);
        }

        /// <summary>
        /// 允许群聊
        /// </summary>
        /// <param name="userName"></param>
        public void Send_AllowTeamChat(string userName)
        {
            var request = new OnlyUserNameRequest { receivename = userName };
            SendMessage(request, CommandType.AllowTeamChat);
        }

        /// <summary>
        /// 客户端端登出
        /// </summary>
        public void Send_StudentLoginOut()
        {
            SendMessageNoPara(CommandType.UserLoginOut);
        }

        #region 通用方法
        private void SendMessage<T>(T t, CommandType cmdType) where T : class, new()
        {
            SendMessage<T> message = new SendMessage<T>();
            message.Action = (int)cmdType;
            message.Data = t;
            SendMessage(message);
        }

        private void SendMessageNoPara(CommandType cmdType)
        {
            SendMessage<EmptyRequest> message = new SendMessage<EmptyRequest>();
            message.Action = (int)cmdType;
            message.Data = new EmptyRequest();
            SendMessage(message);
        }

        #endregion

        #endregion
    }



    class MyReceiveFilter : FixedHeaderReceiveFilter<ReceieveMessage>
    {
        public MyReceiveFilter()
        : base(4) // two vertical bars as package terminator
        {
        }

        public override ReceieveMessage ResolvePackage(IBufferStream bs)
        {
            ReceieveMessage message = new ReceieveMessage();
            var lenBytes = new byte[4];
            bs.Read(lenBytes, 0, 4);
            message.Length = BitConverter.ToInt32(lenBytes, 0);
            var timeBytes = new byte[14];
            bs.Read(timeBytes, 0, 14);
            message.TimeStamp = Encoding.UTF8.GetString(timeBytes);
            var actionBytes = new byte[4];
            bs.Read(actionBytes, 0, 4);
            message.Action = BitConverter.ToInt32(actionBytes, 0);
            var dataLength = message.Length - 18;
            var dataBytes = new byte[dataLength];
            bs.Read(dataBytes, 0, dataLength);
            message.DataStr = Encoding.UTF8.GetString(dataBytes);
            return message;
        }


        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            var lenBytes = new byte[length];
            bufferStream.Read(lenBytes, 0, length);
            return BitConverter.ToInt32(lenBytes, 0);
        }
    }


    public class ReceieveMessage : IPackageInfo
    {
        public int Length { get; set; }
        public string TimeStamp { get; set; }

        public string DataStr { get; set; }

        public int Action { get; set; }
    }

}
