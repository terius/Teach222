using Common;
using DevExpress.XtraEditors;
using Helpers;
using Model;
using SharedForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StudentUser
{
    public partial class UserMainForm : XtraForm
    {
        BlackScreen bsForm = null;
        //  VLCPlayer videoPlayer;
        ChatForm chatForm = new ChatForm();
        //   ViewRtsp videoPlayer2;
        CallForm callForm;
        volatile bool isRunScreen = false;
        Thread theadScreen;
        ScreenCapture sc;
        object obLock = new object();
        VideoShow videoForm;

        //最小化窗体
        //private bool windowCreate = true;
        //protected override void OnActivated(EventArgs e)
        //{
        //    if (windowCreate)
        //    {
        //        base.Visible = false;
        //        windowCreate = false;
        //    }

        //    base.OnActivated(e);
        //}
        public UserMainForm()
        {

            InitializeComponent();
            Text = GlobalVariable.LoginUserInfo.DisplayName;
            tuopan.Text = Text;
            #region 处理收到的消息
            //教师端登入
            GlobalVariable.client.OnTeacherLoginIn = (message) =>
              {
                  TeacherLoginInResponse teachRes = JsonHelper.DeserializeObj<TeacherLoginInResponse>(message.DataStr);
                  GlobalVariable.TeacherIP = teachRes.teachIP;
                  DoAction(() =>
                  {
                      CreateUDPHole();

                  });
              };
            //教师端登出
            GlobalVariable.client.OnTeacherLoginOut = (message) =>
            {
                if (theadScreen != null && theadScreen.ThreadState == ThreadState.Background)
                {
                    isRunScreen = false;
                    Thread.Sleep(200);
                    theadScreen.Abort();
                }
            };



            //收到视频流
            GlobalVariable.client.OnScreenInteract = (message) =>
            {

                ScreenInteract_Response resp = JsonHelper.DeserializeObj<ScreenInteract_Response>(message.DataStr);
                DoAction(() =>
                {
                    ShowRtspVideo(resp.url);

                });

            };
            //收到视频流停止
            GlobalVariable.client.OnStopScreenInteract = (message) =>
            {

                DoAction(() =>
                {
                    StopPlay();

                });

            };
            //锁屏
            GlobalVariable.client.OnLockScreen = (message) =>
            {
                LockScreen(false);

            };
            //终止锁屏
            GlobalVariable.client.OnStopLockScreen = (message) =>
            {
                StopLockScreen();

            };
            //屏幕肃静
            GlobalVariable.client.OnQuiet = (message) =>
            {
                LockScreen(true);

            };
            //终止屏幕肃静
            GlobalVariable.client.OnStopQuiet = (message) =>
            {
                StopLockScreen();

            };

            //收到私聊信息
            GlobalVariable.client.OnPrivateChat = (message) =>
            {
                var chatResponse = JsonHelper.DeserializeObj<PrivateChatRequest>(message.DataStr);
                DoAction(() =>
                {
                    var chatMessage = chatResponse.ToChatMessage();
                    OpenChatForm(chatMessage);
                });
            };
            //收到群聊信息
            GlobalVariable.client.OnTeamChat = (message) =>
            {
                var teamChatResponse = JsonHelper.DeserializeObj<TeamChatRequest>(message.DataStr);

                DoAction(() =>
                {
                    var request = teamChatResponse.ToChatMessage();
                    OpenChatForm(request);
                });

            };
            //收到群聊信息
            GlobalVariable.client.OnGroupChat = (message) =>
            {
                var groupChatResponse = JsonHelper.DeserializeObj<GroupChatRequest>(message.DataStr);

                DoAction(() =>
                {
                    var request = groupChatResponse.ToChatMessage();
                    OpenChatForm(request);
                });

            };

            //点名
            GlobalVariable.client.OnBeginCall = (message) =>
            {
                DoAction(() =>
                {
                    OpenCallForm();

                });
            };
            //结束点名
            GlobalVariable.client.OnEndCall = (message) =>
            {
                DoAction(() =>
                {
                    CloseCallForm();

                });

            };
            //收到创建群组信息
            GlobalVariable.client.OnCreateTeam = (message) =>
            {
                var teamInfo = JsonHelper.DeserializeObj<TeamChatCreateOrUpdateRequest>(message.DataStr);
                GlobalVariable.RefleshTeamList(teamInfo);
                DoAction(() =>
                {
                    chatForm.BringToFront();
                    chatForm.Show();
                    chatForm.ReflashTeamChat();


                });

            };
            //收到请求学生演示
            GlobalVariable.client.OnCallStudentShow = (message) =>
            {
                DoAction(() =>
                {
                    GlobalVariable.client.CreateScreenInteract();
                    GlobalVariable.client.Send_ScreenInteract();

                });

            };
            //收到请求学生演示,只给教师
            GlobalVariable.client.OnCallStudentShowForTeacher = (message) =>
            {
                DoAction(() =>
                {
                    GlobalVariable.client.CreateScreenInteract();
                    GlobalVariable.client.Send_StudentShowToTeacher();

                });

            };
            //收到请求学生演示视频,只给教师
            GlobalVariable.client.OnCallStudentShowVideoForTeacher = (message) =>
            {
                DoAction(() =>
                {
                    GlobalVariable.client.CreateScreenInteract();
                    GlobalVariable.client.Send_StudentShowVideoToTeacher();

                });

            };
            //停止演示
            GlobalVariable.client.OnStopStudentShow = (message) =>
        {
            DoAction(() =>
            {
                GlobalVariable.client.StopScreenInteract();
                GlobalVariable.client.Send_StopScreenInteract();

            });

        };
            //收到禁止私聊
            GlobalVariable.client.OnForbidPrivateChat = (message) =>
            {
                GlobalVariable.LoginUserInfo.AllowPrivateChat = false;
                ChangeChatAllowOrForbit(ChatType.PrivateChat, false);
            };
            //收到禁止群聊
            GlobalVariable.client.OnForbidTeamChat = (message) =>
            {
                GlobalVariable.LoginUserInfo.AllowTeamChat = false;
                ChangeChatAllowOrForbit(ChatType.TeamChat, false);
            };
            //收到允许私聊
            GlobalVariable.client.OnAllowPrivateChat = (message) =>
            {
                GlobalVariable.LoginUserInfo.AllowPrivateChat = true;
                ChangeChatAllowOrForbit(ChatType.PrivateChat, true);
            };
            //收到允许群聊
            GlobalVariable.client.OnAllowTeamChat = (message) =>
            {
                GlobalVariable.LoginUserInfo.AllowTeamChat = true;
                ChangeChatAllowOrForbit(ChatType.TeamChat, true);
            };


            #endregion
            GlobalVariable.client.DueLostMessage();

            //  GlobalVariable.client.OnReveieveData += Client_OnReveieveData;
            //   GlobalVariable.client.Send_StudentInMainForm();
        }

        private void UserMainForm_Load(object sender, System.EventArgs e)
        {
            CreateUDPHole();
        }




        private void CreateUDPHole()
        {
            if (theadScreen == null || theadScreen.ThreadState != ThreadState.Running)
            {
                    theadScreen = new Thread(() =>
                {

                    GlobalVariable.client.CreateUDPStudentHole();
                    GetScreenCapture();

                });
                    theadScreen.IsBackground = true;
                    theadScreen.Start();
            }
        }


        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private void CreateUDPServer()
        {

            var receieveUdpClient = new UdpClient(10887);


            Byte[] receiveBytes = receieveUdpClient.Receive(ref RemoteIpEndPoint);
            var str = Encoding.UTF8.GetString(receiveBytes);

        }

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

        private void OpenChatForm(ChatMessage message)
        {
            GlobalVariable.AddNewChat(message);
            chatForm.BringToFront();
            chatForm.Show();
            chatForm.CreateChatItems(message, true);
        }


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
                videoForm = new VideoShow(ProgramType.Student);
            }
            videoForm.BringToFront();
            videoForm.Show();
            //  videoPlayer = f;
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
        private void LockScreen(bool isSlient)
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
                if (bsForm == null)
                {
                    BlackScreen frm = new BlackScreen(isSlient);
                    frm.Show();
                    bsForm = frm;
                }
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
                    bsForm.EnableMouseAndKeyboard();
                    bsForm.Close();
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



        private void UserMainForm_Shown(object sender, EventArgs e)
        {
            // this.Hide();
        }




        #region 右键菜单

        //签到
        private void mSignIn_Click(object sender, EventArgs e)
        {
            OpenCallForm();
        }
        private void mChat_Click(object sender, EventArgs e)
        {
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




        private void button1_Click(object sender, EventArgs e)
        {
            //var url = "rtsp://184.72.239.149/vod/mp4://BigBuckBunny_175k.mov";
            //videoPlayer = new VLCPlayer();
            //videoPlayer.Show();
            //videoPlayer.StartPlayStream(@"D:\dy\hl.mkv");

            var url = @"e:\terius\hkdg.mkv";
            url = "rtsp://184.72.239.149/vod/mp4://BigBuckBunny_175k.mov";
            ShowRtspVideo(url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopPlay();
        }

        private void btnScreenCapture_Click(object sender, EventArgs e)
        {


        }

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
                        Image screenImage = sc.CaptureScreen();
                        ScreenCaptureInfo info = new ScreenCaptureInfo();
                        info.DisplayName = GlobalVariable.LoginUserInfo.DisplayName;
                        info.UserName = GlobalVariable.LoginUserInfo.UserName;

                        var userJson = JsonHelper.SerializeObj(info);
                        byte[] userBytes = Encoding.UTF8.GetBytes(userJson);
                        byte[] userLengthBytes = BitConverter.GetBytes(userBytes.Length);
                        byte[] imgBytes = FileHelper.ImageToByteArray(screenImage);
                        byte[] imgLengthBytes = BitConverter.GetBytes(imgBytes.Length);
                        byte[] allLengtBytes = BitConverter.GetBytes(userBytes.Length + userLengthBytes.Length + imgBytes.Length + imgLengthBytes.Length);


                        List<byte> byteSource = new List<byte>();
                        byteSource.AddRange(allLengtBytes);
                        byteSource.AddRange(userLengthBytes);
                        byteSource.AddRange(userBytes);
                        byteSource.AddRange(imgLengthBytes);
                        byteSource.AddRange(imgBytes);
                        var sendBytes = byteSource.ToArray();

                        GlobalVariable.client.SendDesktopPic(sendBytes);
                        //   GlobalVariable.client.StopUdp();
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
            GlobalVariable.client.Send_StudentLoginOut();
            StopUdp();
        }

        private void StopUdp()
        {
            isRunScreen = false;
            Thread.Sleep(500);
            if (theadScreen != null)
            {
                theadScreen.Abort();
            }

        }

      
    }




}
