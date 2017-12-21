using Common;
using EduService;
using Helpers;
using Model;
using SharedForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StudentUser
{
    public partial class UserMainForm : Form
    {
        //  BlackScreen bsForm = null;
        DisableMouseAndKeyboardForm bsForm = null;
        bool isPushScream = false;
        CommandType currPushCreamType;

        ChatForm chatForm;
        //   ViewRtsp videoPlayer2;
        CallForm callForm;
        volatile bool isRunScreen = false;
        Thread theadScreen;
        ScreenCapture sc;
        object obLock = new object();
        VideoShow videoForm;
        EduUDPClient udpClient;
        string tempScreenFile = "";

        //最小化窗体
        private bool windowCreate = true;
        protected override void OnActivated(EventArgs e)
        {
            if (windowCreate)
            {
                base.Visible = false;
                windowCreate = false;
            }

            base.OnActivated(e);
        }
        //   string GlobalVariable.MasterTitle;

        public UserMainForm()
        {

            InitializeComponent();
            CreateFilePath();
            //  GlobalVariable.MasterTitle = "教师端";
            if (GlobalVariable.IsHuiShenXiTong)
            {
                // GlobalVariable.MasterTitle = "指挥室";
                mSignIn.Visible = false;
                mHandUp.Visible = false;
                mPrivateSMS.Visible = false;
                mFileShare.Visible = false;
            }

            chatForm = new ChatForm();
            Text = GlobalVariable.LoginUserInfo.DisplayName;
            tuopan.Text = Text;
            #region 处理收到的消息


            //学生端收到消息
            GlobalVariable.client.OnStudentReceiveMessage = (message) =>
            {
                switch (message.Action)
                {
                    case (int)CommandType.UserLoginRes:
                        break;
                    case (int)CommandType.TeacherLoginIn://主机端登录

                        TeacherLoginInResponse teachRes = JsonHelper.DeserializeObj<TeacherLoginInResponse>(message.DataStr);
                        ShowNotify(GlobalVariable.MasterTitle + "登录");
                        GlobalVariable.TeacherIP = teachRes.teachIP;
                        DoAction(() =>
                        {
                            //  CreateUDPHole();

                        });
                        break;
                    case (int)CommandType.TeacherLoginOut://主机端登出
                        ShowNotify(GlobalVariable.MasterTitle + "登出");
                        if (theadScreen != null && theadScreen.ThreadState == ThreadState.Background)
                        {
                            isRunScreen = false;
                            Thread.Sleep(200);
                            theadScreen.Abort();
                        }
                        break;
                    case (int)CommandType.ScreenInteract://播放视频流
                        ShowNotify("收到视频流，开始播放");
                        ScreenInteract_Response resp = JsonHelper.DeserializeObj<ScreenInteract_Response>(message.DataStr);
                        DoAction(() =>
                        {
                            ShowRtspVideo(resp.url);

                        });
                        break;
                    case (int)CommandType.StopScreenInteract://停止视频流
                        ShowNotify("视频流已结束，停止播放");
                        DoAction(() =>
                        {
                            StopPlay();

                        });
                        break;
                    case (int)CommandType.LockScreen://锁屏
                        ShowNotify("当前屏幕被锁定");
                        LockScreen(false);
                        break;
                    case (int)CommandType.StopLockScreen://终止锁屏
                        ShowNotify("屏幕锁定已解除");
                        StopLockScreen();
                        break;
                    case (int)CommandType.Quiet://屏幕肃静
                        ShowNotify("当前屏幕被锁定并黑屏");
                        LockScreen(true);
                        break;
                    case (int)CommandType.StopQuiet://终止屏幕肃静
                        ShowNotify("屏幕锁定及黑屏已解除");
                        StopLockScreen();
                        break;
                    case (int)CommandType.PrivateChat://收到私聊信息
                    case (int)CommandType.TeamChat://收到组聊信息
                    case (int)CommandType.GroupChat://收到群聊信息
                        DoReceiveChatMessage(message);
                        break;
                    case (int)CommandType.BeginCall://开始点名
                        ShowNotify("开始点名");
                        DoAction(() =>
                        {
                            OpenCallForm();

                        });
                        break;
                    case (int)CommandType.EndCall://结束点名
                        ShowNotify("结束点名");
                        DoAction(() =>
                        {
                            CloseCallForm();

                        });
                        break;
                    case (int)CommandType.CreateTeam://收到创建群组信息
                        var teamInfo = JsonHelper.DeserializeObj<TeacherTeam>(message.DataStr);
                        GlobalVariable.LoadTeamList(teamInfo);
                        DoAction(() =>
                        {

                            if (GlobalVariable.CheckChatFormIsOpened())
                            {
                                GlobalVariable.ShowNotifyMessage("群组信息已经更改");
                                chatForm.ReflashTeamChat();
                            }
                            //  chatForm.BringToFront();
                            //  chatForm.Show();



                        });
                        break;
                    case (int)CommandType.CallStudentShow://收到请求客户端演示
                        ShowNotify("收到推送请求，开始广播当前屏幕");
                        DoAction(() =>
                        {
                            currPushCreamType = CommandType.CallStudentShow;
                            PushVideoCream();

                        });
                        break;
                    case (int)CommandType.CallStudentShowForMySelf://收到请求客户端演示
                        ShowNotify("收到推送请求，开始推送当前屏幕到" + GlobalVariable.MasterTitle);
                        DoAction(() =>
                        {
                            currPushCreamType = CommandType.CallStudentShowForMySelf;
                            PushVideoCream();

                        });
                        break;
                    case (int)CommandType.CallStudentShowVideoToTeacher://收到请求客户端演示视频
                        ShowNotify("收到推送请求，开始推送摄像头视频到" + GlobalVariable.MasterTitle);
                        DoAction(() =>
                        {
                            currPushCreamType = CommandType.CallStudentShowVideoToTeacher;
                            PushVideoCream();

                        });
                        break;
                    case (int)CommandType.StopStudentShow://停止演示
                        ShowNotify("停止演示");
                        DoAction(() =>
                        {
                            GlobalVariable.client.StopScreenInteract();
                            GlobalVariable.client.Send_StopScreenInteract();
                            isPushScream = false;
                        });
                        break;
                    case (int)CommandType.ForbidPrivateChat://收到禁止私聊
                        ShowNotify(GlobalVariable.MasterTitle + "已禁止私聊");
                        GlobalVariable.LoginUserInfo.AllowPrivateChat = false;
                        ChangeChatAllowOrForbit(ChatType.PrivateChat, false);
                        break;
                    case (int)CommandType.ForbidTeamChat://收到禁止群聊
                        ShowNotify(GlobalVariable.MasterTitle + "已禁止群聊");
                        GlobalVariable.LoginUserInfo.AllowTeamChat = false;
                        ChangeChatAllowOrForbit(ChatType.TeamChat, false);
                        break;
                    case (int)CommandType.AllowPrivateChat://收到允许私聊
                        ShowNotify(GlobalVariable.MasterTitle + "已允许私聊");
                        GlobalVariable.LoginUserInfo.AllowPrivateChat = true;
                        ChangeChatAllowOrForbit(ChatType.PrivateChat, true);
                        break;
                    case (int)CommandType.AllowTeamChat://收到允许群聊
                        ShowNotify(GlobalVariable.MasterTitle + "已允许群聊");
                        GlobalVariable.LoginUserInfo.AllowTeamChat = true;
                        ChangeChatAllowOrForbit(ChatType.TeamChat, true);
                        break;
                    case (int)CommandType.DeleteUserInGroup://收到删除群组成员
                        var deleteInfo = JsonHelper.DeserializeObj<DeleteTeamMemberRequest>(message.DataStr);
                        DoAction(() =>
                        {
                            DeleteTeamMember(deleteInfo);
                        });

                        break;
                    default:
                        break;
                }
            };


            #endregion
            GlobalVariable.client.DueLostMessage();
            GlobalVariable.client.SendXinTiao();
            //  GlobalVariable.client.OnReveieveData += Client_OnReveieveData;
            //   GlobalVariable.client.Send_StudentInMainForm();
        }

        private void StopPushScream()
        {
            if (isPushScream)
            {
                GlobalVariable.client.StopScreenInteract();
                GlobalVariable.client.Send_StopScreenInteract();
            }
        }

        private void PushVideoCream(string fbl = null)
        {
         //   StopPushScream();
            if (currPushCreamType == CommandType.CallStudentShow)
            {
                GlobalVariable.client.Send_ScreenInteract(fbl);
            }
            else if (currPushCreamType == CommandType.CallStudentShowForMySelf)
            {
                GlobalVariable.client.Send_StudentShowToTeacher(fbl);
            }
            else if (currPushCreamType == CommandType.CallStudentShowVideoToTeacher)
            {
                GlobalVariable.client.Send_StudentShowVideoToTeacher(fbl);
            }
            isPushScream = true;
        }


        private void DoReceiveChatMessage(ReceieveMessage message)
        {
            var chatMessage = GlobalVariable.CreateChatMessage(message);
            DoAction(() =>
            {

                OpenChatForm(chatMessage);
            });
        }

        public bool ChatFormIsVisible
        {
            get
            {
                if (chatForm == null)
                {
                    return false;
                }
                return chatForm.Visible;
            }
        }

        private void OpenChatForm(ChatMessage message)
        {
            chatForm.BringToFront();
            chatForm.CreateChatItems(message, true);
            if (!ChatFormIsVisible)
            {
                GlobalVariable.ShowChatMessageNotify(message, chatForm);
            }
            else
            {
                chatForm.Show();
            }
        }


        private void ShowNotify(string message)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                GlobalVariable.ShowNotifyMessage(message);
            });
        }

        private void DeleteTeamMember(DeleteTeamMemberRequest deleteInfo)
        {
            GlobalVariable.DelTeamMember(deleteInfo);
            if (GlobalVariable.CheckChatFormIsOpened())
            {
                GlobalVariable.ShowNotifyMessage("群组信息已经更改");
                chatForm.ReflashTeamChat();
            }
        }

        private void CreateFilePath()
        {
            if (!Directory.Exists(GlobalVariable.BaseFilePath))
            {
                Directory.CreateDirectory(GlobalVariable.BaseFilePath);
            }
            if (!Directory.Exists(GlobalVariable.DownloadPath))
            {
                Directory.CreateDirectory(GlobalVariable.DownloadPath);
            }

            if (!Directory.Exists(GlobalVariable.AudioRecordPath))
            {
                Directory.CreateDirectory(GlobalVariable.AudioRecordPath);
            }

            if (!Directory.Exists(GlobalVariable.TempPath))
            {
                Directory.CreateDirectory(GlobalVariable.TempPath);
            }

            if (!Directory.Exists(GlobalVariable.VideoRecordPath))
            {
                Directory.CreateDirectory(GlobalVariable.VideoRecordPath);
            }
        }

        private void UserMainForm_Load(object sender, System.EventArgs e)
        {
            string tempPath = FileHelper.CreatePath("tempScreen");
            tempScreenFile = Path.Combine(tempPath, "temp.jpg");
            udpClient = new EduUDPClient(ProgramType.Student);
            CreateUDPHole();
            CheckNetworkStatus();
        }
        private void CheckNetworkStatus()
        {
           
            ThreadPool.QueueUserWorkItem((ob) =>
            {
                bool netStatus = false;
                string errMsg;
                string serverUrl = System.Configuration.ConfigurationManager.AppSettings["serverIP"];
                while (true)
                {
                   
                    netStatus = PingNetwork.GetServerStatus(serverUrl, out errMsg);
                    if (!netStatus)
                    {
                        GlobalVariable.client.IsNetworkOK = false;
                        this.InvokeOnUiThreadIfRequired(() =>
                        {
                            GlobalVariable.ShowNotifyMessage(errMsg, 5, "red");
                        });
                    }
                    else
                    {
                        GlobalVariable.client.IsNetworkOK = true;
                    }
                    Thread.Sleep(20000);
                }

            });
        }



        private void CreateUDPHole()
        {

            if (theadScreen == null || theadScreen.ThreadState != ThreadState.Running)
            {
                theadScreen = new Thread(() =>
                    {
                        //  udpClient.CreateUDPStudentHole();
                        GetScreenCapture();

                    });
                theadScreen.IsBackground = true;
                theadScreen.Start();
            }
        }


        //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //private void CreateUDPServer()
        //{

        //    var receieveUdpClient = new UdpClient(10887);


        //    Byte[] receiveBytes = receieveUdpClient.Receive(ref RemoteIpEndPoint);
        //    var str = Encoding.UTF8.GetString(receiveBytes);

        //}

        private void DoAction(Action action)
        {
            this.InvokeOnUiThreadIfRequired(action);
        }




        private void ChangeChatAllowOrForbit(ChatType chatType, bool isAllow)
        {
            DoAction(() =>
            {
                if (chatForm != null && !chatForm.IsDisposed)
                {
                    chatForm.ChangeAllowChat(chatType, isAllow);
                }

            });
        }




        private void CloseCallForm()
        {
            if (callForm != null)
            {
                callForm.Close();
            }
        }

        private void OpenCallForm()
        {
            if (callForm == null || callForm.IsDisposed)
            {
                callForm = new CallForm();
            }
            callForm.BringToFront();
            callForm.ShowDialog();
        }

        private bool CheckChatFormIsOpen()
        {
            if (chatForm == null)
            {
                return false;
            }
            return !chatForm.IsHide;
            //  return Application.OpenForms.OfType<ChatForm>().Any();
        }



        //private void OpenChatForm(AddChatRequest request)
        //{
        //    GlobalVariable.AddNewChat(request);
        //    chatForm.BringToFront();
        //    chatForm.Show();
        //    chatForm.CreateChatItems(request, true);
        //}




        ///// <summary>
        ///// 窗体隐藏（重载此方法后onload事件不会执行)
        ///// </summary>
        ///// <param name="value"></param>
        //protected override void SetVisibleCore(bool value)
        //{
        //    base.SetVisibleCore(false);
        //}







        private void ShowRtspVideo(string rtsp)
        {
            if (videoForm == null || videoForm.IsDisposed)
            {
                videoForm = new VideoShow(ProgramType.Student, null);
            }
            videoForm.BringToFront();
            videoForm.Show();
            //  videoPlayer = f;
            Thread.Sleep(4000);
            videoForm.PlayVideo(rtsp);

        }




        private void StopPlay()
        {
            if (videoForm != null)
            {
                videoForm.Close();
                videoForm = null;
            }



        }

        /// <summary>
        /// 锁屏（禁止鼠标和键盘)
        /// </summary>
        private void LockScreen(bool setBlack)
        {
            // BlockInput(true);
            // actHook = new Cls.UserActivityHook();
            //actHook.OnMouseActivity += ActHook_OnMouseActivity;
            //actHook.KeyDown += ActHook_KeyDown;
            //actHook.KeyPress += ActHook_KeyPress;
            //actHook.KeyUp += ActHook_KeyUp;
            //     actHook.Start();
            DoAction(() =>
            {
                if (bsForm == null || bsForm.IsDisposed)
                {
                    bsForm = new DisableMouseAndKeyboardForm();
                    //BlackScreen frm = new BlackScreen(isSlient);
                    //frm.Show();
                    //bsForm = frm;
                }
                bsForm.Show();
                bsForm.SetDisable(setBlack);

            });


        }

        /// <summary>
        /// 撤销锁屏
        /// </summary>
        private void StopLockScreen()
        {
            //   BlockInput(false);
            //if (actHook != null)
            //{
            //    actHook.Stop();
            //}
            DoAction(() =>
            {

                if (bsForm != null)
                {
                    bsForm.Release();
                    bsForm = null;
                }
                //FormCollection fc = Application.OpenForms;
                //foreach (Form frm in fc)
                //{
                //    if (frm.Name == "BlackScreen")
                //    {
                //        frm
                //        frm.Close();
                //        break;
                //    }
                //}

            });
        }


        private void btnLockScreen_Click(object sender, EventArgs e)
        {

            LockScreen(true);
        }

        private void btnStopLockScreen_Click(object sender, EventArgs e)
        {
            StopLockScreen();
        }





        #region 右键菜单

        //签到
        private void mSignIn_Click(object sender, EventArgs e)
        {
            OpenCallForm();
        }
        private void mChat_Click(object sender, EventArgs e)
        {
            chatForm.ReflashTeamChat();
            chatForm.BringToFront();
            chatForm.Show();
        }

        private void mHandUp_Click(object sender, EventArgs e)
        {

        }

        private void mPrivateSMS_Click(object sender, EventArgs e)
        {

        }

        private void mFileShare_Click(object sender, EventArgs e)
        {

        }

        private void mExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }



        #endregion



        private void GetScreenCapture()
        {
            isRunScreen = true;
            while (isRunScreen)
            {
                lock (obLock)
                {
                    try
                    {
                        if (sc == null)
                        {
                            sc = new ScreenCapture();
                        }

                        byte[] imgBytes = sc.GetCaptureScreenToSmallFile(tempScreenFile, ImageFormat.Jpeg);// sc.CaptureScreen();
                        ScreenCaptureInfo info = new ScreenCaptureInfo();
                        info.DisplayName = GlobalVariable.LoginUserInfo.DisplayName;
                        info.UserName = GlobalVariable.LoginUserInfo.UserName;

                        var userJson = JsonHelper.SerializeObj(info);
                        byte[] userBytes = Encoding.UTF8.GetBytes(userJson);
                        byte[] userLengthBytes = BitConverter.GetBytes(userBytes.Length);
                        //     byte[] imgBytes = FileHelper.ImageToByteArray(screenImage);
                        byte[] imgLengthBytes = BitConverter.GetBytes(imgBytes.Length);
                        byte[] allLengtBytes = BitConverter.GetBytes(userBytes.Length + userLengthBytes.Length + imgBytes.Length + imgLengthBytes.Length);


                        List<byte> byteSource = new List<byte>();
                        byteSource.AddRange(allLengtBytes);
                        byteSource.AddRange(userLengthBytes);
                        byteSource.AddRange(userBytes);
                        byteSource.AddRange(imgLengthBytes);
                        byteSource.AddRange(imgBytes);
                        var sendBytes = byteSource.ToArray();

                        udpClient.SendDesktopPic(sendBytes);
                        //   udpClient.WaitUdp();
                        //    Loger.LogMessage("GetScreenCapture:");
                        Thread.Sleep(5000);
                    }
                    catch (Exception ex)
                    {
                        Loger.LogMessage(ex);
                    }

                }
            }


        }




        private void UserMainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.tuopan.Visible = false;
            GlobalVariable.client.Send_StudentLoginOut();
            StopUdp();
            GlobalVariable.KillAllFFmpeg();
            Environment.Exit(Environment.ExitCode);
        }

        private void StopUdp()
        {

            isRunScreen = false;
            Thread.Sleep(500);
            if (udpClient != null)
            {
                udpClient.CloseStudentUDP();
            }
            if (theadScreen != null)
            {
                theadScreen.Abort();
            }

        }

        private void 高清ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isPushScream)
            {
                PushVideoCream("1280*720");
            }
        }

        private void 标清ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isPushScream)
            {
                PushVideoCream("640*480");
            }
        }

        private void 流畅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isPushScream)
            {
                PushVideoCream("320*240");
            }
        }
    }




}
