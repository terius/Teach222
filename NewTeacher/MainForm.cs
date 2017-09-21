using Common;
using Helpers;
using Model;
using NewTeacher.Controls;
using NewTeacher.Properties;
using SharedForms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using EduService;
using System.IO;

namespace NewTeacher
{
    public partial class MainForm : MyForm
    {
        #region 自定义字段
        OnlineInfo onlineInfo;
        ChatForm chatForm;
        //  string soundSource;
        bool isPush = false;//是否正在推送视频流
        string actionStuUserName;
        VideoShow videoForm;
        //   ViewRtsp videoPlayer;
        string videoFileName = "";
        EduUDPClient udpClient;
        #endregion
        public MainForm()
        {
            InitializeComponent();
            CreateFilePath();
            if (GlobalVariable.IsHuiShenXiTong)
            {
                this.Text = "会审系统";
                menuClassNamed.Hide();
                menuExportSign.Hide();
                menuFileShare.Hide();
                menuRomoteControl.Hide();
                tableLayoutPanel1.ColumnCount = 1;
                tableLayoutPanel1.RowCount = 1;
                myGroupBox1.Text = "群聊";
                menuStudentShow.Text = "审讯室演示";
                myGroupBox7.Text = "在线审讯室列表";
                myGroupBox8.Text = "审讯室屏幕";
                menuGroupChat.Text = "群组聊天";
                //  tableLayoutPanel3.ColumnStyles[4].Width = 0f;
            }

            GlobalVariable.client.OnClentIsConnecting += Client_OnClentIsConnecting;
            InitOnlineInfo();
            GlobalVariable.LoadTeamFromXML();

            #region 接收消息事件
            GlobalVariable.client.OnOnlineList = (message) =>
            {
                var userList2 = JsonHelper.DeserializeObj<List<OnlineListResult>>(message.DataStr);
                onlineInfo.OnOnlineChange(userList2);
            };

            GlobalVariable.client.OnPrivateChat = (message) =>
            {
                var PrivateChatMessage = JsonHelper.DeserializeObj<PrivateChatRequest>(message.DataStr);
                this.InvokeOnUiThreadIfRequired(() => { ReceievePrivateMessage(PrivateChatMessage); });
            };

            GlobalVariable.client.OnTeamChat = (message) =>   //收到群聊信息
            {

                var TeamChatRequest = JsonHelper.DeserializeObj<TeamChatRequest>(message.DataStr);
                this.InvokeOnUiThreadIfRequired(() => { ReceieveTeamMessage(TeamChatRequest); });
            };
            GlobalVariable.client.OnGroupChat = (message) =>
            {

                var groupChatRequest = JsonHelper.DeserializeObj<GroupChatRequest>(message.DataStr);
                this.InvokeOnUiThreadIfRequired(() => { ReceieveGroupMessage(groupChatRequest); });
            };
            GlobalVariable.client.OnOneUserLogIn = (message) =>//某个客户端登录
            {

                var newUser = JsonHelper.DeserializeObj<List<OnlineListResult>>(message.DataStr);
                onlineInfo.OnNewUserLoginIn(newUser);
            };
            GlobalVariable.client.OnStudentCall = (message) =>//课堂点名
            {
                var callInfo = JsonHelper.DeserializeObj<StuCallRequest>(message.DataStr);
                UpdateOnLineStatus(callInfo);
            };
            GlobalVariable.client.OnUserLoginOut = (message) =>//用户登出
            {
                var loginoutInfo = JsonHelper.DeserializeObj<UserLogoutResponse>(message.DataStr);
                onlineInfo.OnUserLoginOut(loginoutInfo);
                DeleteScreen(loginoutInfo);
            };
            GlobalVariable.client.OnScreenInteract = (message) =>//收到视频流
            {

                ScreenInteract_Response resp = JsonHelper.DeserializeObj<ScreenInteract_Response>(message.DataStr);
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    if (message.Action == (int)CommandType.StudentShowToTeacher)
                    {

                    }
                    PlayRtspVideo(resp.url);
                });
            };
            GlobalVariable.client.OnStopScreenInteract = (message) =>//收到停止接收视频流
            {
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    StopPlay();
                });
            };

            #endregion

            GlobalVariable.client.DueLostMessage();
            GlobalVariable.client.Send_OnlineList();
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
        }

        private void Client_OnClentIsConnecting(object sender, EventArgs e)
        {
            MessageBox.Show("正在连接中，请稍后再试");
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

            CreateUDPConnect();
        }

        #region  接收消息事件


        private void HideScreenShowPic(string userName)
        {
            foreach (StudentScreen item in flowLayoutPanel1.Controls)
            {
                if (item.UserName == userName)
                {
                    item.HideCallShowLabel();
                    break;
                }
            }
        }

        private void DeleteScreen(UserLogoutResponse loginoutInfo)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                foreach (StudentScreen item in flowLayoutPanel1.Controls)
                {
                    if (item.UserName == loginoutInfo.username)
                    {
                        flowLayoutPanel1.Controls.Remove(item);
                        break;
                    }
                }
            });
        }

        private void PlayRtspVideo(string rtsp)
        {

            if (videoForm == null || videoForm.IsDisposed)
            {
                videoForm = new VideoShow(ProgramType.Teacher, actionStuUserName);
            }
            videoForm.BringToFront();
            videoForm.Show();
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

        private void ReceieveTeamMessage(TeamChatRequest message)
        {
            var request = message.ToChatMessage();
            GlobalVariable.AddNewChat(request);
            OpenOrCreateChatForm(request, true);
        }

        private void ReceieveGroupMessage(GroupChatRequest message)
        {
            var request = message.ToChatMessage();
            GlobalVariable.AddNewChat(request);
            OpenOrCreateChatForm(request, true);
        }


        private void ReceievePrivateMessage(PrivateChatRequest message)
        {
            var request = message.ToChatMessage();
            GlobalVariable.AddNewChat(request);
            OpenOrCreateChatForm(request, true);
        }

        private void UpdateOnLineStatus(StuCallRequest callInfo)
        {
            foreach (OnlineListResult item in onlineInfo.OnLineList)
            {
                if (item.username == callInfo.username)
                {
                    item.isCalled = true;
                    break;
                }
            }
            this.InvokeOnUiThreadIfRequired(() =>
            {
                foreach (ListViewItem item in lvOnline.Items)
                {
                    // string nickname = item.Text;
                    //   string no = item.SubItems[3].Text;
                    string userName = item.SubItems[2].Text;
                    if (userName == callInfo.username)
                    {
                        item.SubItems[1].Text = "是";
                        break;
                    }
                }

            });

        }


        #endregion

        #region 在线列表
        private void InitOnlineInfo()
        {
            onlineInfo = new OnlineInfo();
            onlineInfo.OnLineChange += OnlineInfo_OnLineChange;
            onlineInfo.AddOnLine += OnlineInfo_AddOnLine;
            onlineInfo.DelOnLine += OnlineInfo_DelOnLine;
        }
        private void OnlineInfo_DelOnLine(UserLogoutResponse delInfo)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                foreach (ListViewItem item in this.lvOnline.Items)
                {
                    if (item.SubItems[2].Text == delInfo.username)
                    {
                        item.Remove();
                        break;
                    }
                }
            });
        }

        private void OnlineInfo_AddOnLine(object sender, OnlineEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => AddOnlineUser(e.OnLines));
        }

        private void AddOnlineUser(IList<OnlineListResult> list)
        {
            foreach (OnlineListResult item in list)
            {
                if (!IsMySelf(item.username))
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.nickname;
                    listItem.ImageIndex = item.clientRole == ClientRole.Student ? 0 : 39;
                    listItem.SubItems.Add(item.isCalled ? "是" : "");
                    listItem.SubItems.Add(item.username);
                    listItem.SubItems.Add(item.no);
                    this.lvOnline.Items.Add(listItem);

                }
            }
        }

        private bool IsMySelf(string userName)
        {
            return userName == GlobalVariable.LoginUserInfo.UserName;
        }

        private void OnlineInfo_OnLineChange(object sender, OnlineEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => userListShow(e.OnLines));
        }

        /// <summary>
        /// 显示在线用户列表
        /// </summary>
        /// <param name="onLineList"></param>
        private void userListShow(IList<OnlineListResult> list)
        {
            this.lvOnline.Items.Clear();
            AddOnlineUser(list);
        }

        #endregion




        #region 方法

        private void CreateUDPConnect()
        {
            Thread t = new Thread(() =>
            {
                udpClient = new EduUDPClient(ProgramType.Teacher);
                udpClient.OnTeacherReceiveUDP = (sinfo) =>
                 {
                     this.InvokeOnUiThreadIfRequired(() =>
                     {
                         AddStudentScreenToPanel(sinfo);
                     });

                 };
                udpClient.CreateUDPTeacherHole();


            });
            t.IsBackground = true;
            t.Start();
        }

        private void AddStudentScreenToPanel(ScreenCaptureInfo sinfo)
        {
            //var saveAudioFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screen");
            //if (!Directory.Exists(saveAudioFilePath))
            //{
            //    Directory.CreateDirectory(saveAudioFilePath);
            //}
            //sinfo.Image.Save(Path.Combine(saveAudioFilePath, DateTime.Now.Ticks + ".png"));
            bool isExist = false;
            foreach (StudentScreen item in flowLayoutPanel1.Controls)
            {
                if (item.UserName == sinfo.UserName)
                {
                    isExist = true;
                    item.UpdateScreen(sinfo.Image);
                    break;
                }
            }

            if (!isExist)
            {
                StudentScreen newItem = new StudentScreen(sinfo);
                flowLayoutPanel1.Controls.Add(newItem);
                newItem.Width = 300;
                newItem.Height = 220;
            }
        }
        private void SendAction(TeacherAction type)
        {
            switch (type)
            {
                case TeacherAction.menuClassNamed_Click:
                    CallForm frm = new CallForm();
                    frm.ShowDialog(this);
                    break;
                case TeacherAction.menuExportSign_Click:
                    ExportSign();
                    break;
                case TeacherAction.menuGroupChat_Click:
                    ChatToALL();
                    break;
                case TeacherAction.menuTeamCreate_Click:
                    TeamDiscuss formTeam = new TeamDiscuss(onlineInfo);
                    formTeam.ShowDialog();
                    break;
                case TeacherAction.menuViewTeam_Click:
                    TeamView teamView = new TeamView();
                    teamView.ShowDialog();
                    break;
                case TeacherAction.menuSilence_Click:
                    if (menuSilence.Text == "屏幕肃静")
                    {
                        GlobalVariable.client.Send_Quiet();
                        menuSilence.Text = "解除屏幕肃静";
                    }
                    else
                    {
                        GlobalVariable.client.Send_StopQuiet();
                        menuSilence.Text = "屏幕肃静";
                    }
                    break;
                case TeacherAction.menuRomoteControl_Click:
                    if (lvOnline.SelectedItems.Count <= 0)
                    {
                        GlobalVariable.ShowWarnning("请先选择要控制的客户端");
                        return;
                    }
                    string username = lvOnline.SelectedItems[0].SubItems[2].Text;
                    if (menuRomoteControl.Text == "禁用键鼠")
                    {
                        GlobalVariable.client.Send_LockScreen(username);
                        menuRomoteControl.Text = "解锁";
                    }
                    else
                    {
                        GlobalVariable.client.Send_StopLockScreen(username);
                        menuRomoteControl.Text = "禁用键鼠";
                    }
                    break;
                case TeacherAction.menuScreenShare_Click:
                    string text = menuScreenShare.Text;
                    if (text == "屏幕广播")
                    {
                        if (!isPush)
                        {
                            GlobalVariable.client.CreateScreenInteract();
                            GlobalVariable.client.Send_ScreenInteract();
                            menuScreenShare.Text = "关闭广播";
                            isPush = true;
                        }
                        else
                        {
                            //  showTip();
                            return;
                        }
                    }
                    else
                    {
                        GlobalVariable.client.StopScreenInteract();
                        GlobalVariable.client.Send_StopScreenInteract();
                        menuScreenShare.Text = "屏幕广播";
                        isPush = false;
                    }
                    break;
                case TeacherAction.menuStudentShow_Click:
                    string menuStudentText = menuStudentShow.Text;
                    if (menuStudentText == "客户端演示")
                    {
                        GetSelectStudentUserName();
                        if (!string.IsNullOrWhiteSpace(actionStuUserName))
                        {

                            GlobalVariable.client.Send_CallStudentShow(actionStuUserName);
                            menuStudentShow.Text = "关闭演示";

                        }

                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(actionStuUserName))
                        {
                            GlobalVariable.client.Send_StopStudentShow(actionStuUserName);
                            actionStuUserName = null;
                            menuStudentShow.Text = "客户端演示";
                        }
                    }
                    break;
                case TeacherAction.menuVideoLive_Click:
                    string menuVideoLiveText = menuVideoLive.Text;
                    if (menuVideoLiveText == "视频直播")
                    {
                        if (!isPush)
                        {
                            GlobalVariable.client.CreateScreenInteract();
                            GlobalVariable.client.Send_VideoInteract();
                            menuVideoLive.Text = "关闭直播";
                            isPush = true;
                        }
                        else
                        {
                            //  showTip();
                            return;
                        }
                    }
                    else
                    {
                        GlobalVariable.client.StopScreenInteract();
                        GlobalVariable.client.Send_StopScreenInteract();
                        menuVideoLive.Text = "视频直播";
                        isPush = false;
                    }
                    break;
                case TeacherAction.menuFileShare_Click:
                    ChatToALL();
                    chatForm.UploadFileToALL(); // 暂时屏蔽
                    break;
                case TeacherAction.menuFileShare2_Click:

                    break;
                case TeacherAction.menuAccount_Click:
                    break;
                case TeacherAction.menuVideoRecord_Click:

                    if (this.menuVideoRecord.Text == "屏幕录制")
                    {
                        videoFileName = GlobalVariable.BeginRecordVideo();
                        this.menuVideoRecord.Image = Resources.录制;
                        GlobalVariable.ShowNotifyMessage("正在屏幕录制中...", -1);
                        this.menuVideoRecord.Text = "停止录制";
                    }
                    else
                    {
                        GlobalVariable.EndRecordVideo();
                        GlobalVariable.ShowNotifyMessage("录制完成,视频文件在程序目录的VideoRecord中", -1);
                        this.menuVideoRecord.Image = Resources.未录制;
                        this.menuVideoRecord.Text = "屏幕录制";
                    }
                    break;
                default:
                    break;
            }
        }

        private string GetSelectStudentUserName()
        {
            actionStuUserName = null;
            if (lvOnline.Items.Count <= 0)
            {
                GlobalVariable.ShowWarnning("当前在线客户端为空");
                return "";
            }
            if (lvOnline.SelectedItems.Count <= 0)
            {
                GlobalVariable.ShowWarnning("请先选择客户端");
                return "";
            }
            string username = lvOnline.SelectedItems[0].SubItems[2].Text;
            actionStuUserName = username;
            return username;
        }

        private void ChatToALL()
        {
            var request = new ChatMessage();
            request.SendDisplayName = "所有人";
            request.ChatType = ChatType.PrivateChat;
            request.SendUserName = "allpeople";
            request.UserType = ClientRole.Student;
            GlobalVariable.AddNewChat(request);
            OpenOrCreateChatForm(request, false);
        }

        public void OpenOrCreateChatForm(ChatMessage request, bool fromReceMsg)
        {
            if (chatForm == null || chatForm.IsDisposed)
            {
                chatForm = new ChatForm();
            }

            chatForm.BringToFront();
            chatForm.Show();
            chatForm.CreateChatItems(request, fromReceMsg);

        }

        private void ExportSign()
        {
            //var onlineList = onlineInfo.GetStudentOnlineList();
            if (onlineInfo == null || onlineInfo.LoginedStuList.Count <= 0)
            {
                GlobalVariable.ShowWarnning("当前登陆客户端为空");
                return;
            }
            var table = new System.Data.DataTable();
            table.Columns.Add("客户端姓名", typeof(string));
            table.Columns.Add("是否签到", typeof(string));
            foreach (var item in onlineInfo.LoginedStuList)
            {
                if (item.clientRole == ClientRole.Student)
                {
                    System.Data.DataRow dr = table.NewRow();
                    dr[0] = item.nickname;
                    dr[1] = item.isCalled ? "是" : "否";
                    table.Rows.Add(dr);
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "导出签到.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelHelper.Export(table, saveFileDialog1.FileName);
                GlobalVariable.ShowSuccess("导出成功");
            }

        }
        #endregion


        #region  事件
        private void menuClassNamed_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuClassNamed_Click);
        }

        private void menuExportSign_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuExportSign_Click);
        }

        private void menuGroupChat_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuGroupChat_Click);
        }

        private void menuTeamCreate_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuTeamCreate_Click);
        }

        private void menuViewTeam_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuViewTeam_Click);
        }

        private void menuSilence_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuSilence_Click);
        }

        private void menuRomoteControl_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuRomoteControl_Click);
        }

        private void menuScreenShare_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuScreenShare_Click);
        }

        private void menuStudentShow_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuStudentShow_Click);
        }

        private void menuVideoLive_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuVideoLive_Click);
        }

        private void menuFileShare_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuFileShare_Click);
        }

        private void menuFileShare2_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuFileShare2_Click);
        }

        private void menuAccount_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuAccount_Click);
        }

        private void menuVideoRecord_Click(object sender, System.EventArgs e)
        {
            SendAction(TeacherAction.menuVideoRecord_Click);
        }

        private enum TeacherAction
        {
            menuClassNamed_Click,
            menuExportSign_Click,
            menuGroupChat_Click,
            menuTeamCreate_Click,
            menuViewTeam_Click,
            menuSilence_Click,
            menuRomoteControl_Click,
            menuScreenShare_Click,
            menuStudentShow_Click,
            menuVideoLive_Click,
            menuFileShare_Click,
            menuFileShare2_Click,
            menuAccount_Click,
            menuVideoRecord_Click
        }


        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            GlobalVariable.client.Send_OnlineList();
        }

        private void UserListMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Name;
            GetSelectStudentUserName();
            if (string.IsNullOrWhiteSpace(actionStuUserName))
            {
                return;
            }
            //  GlobalVariable.ShowSuccess(actionStuUserName);
            switch (name)
            {
                case "userList_privateChat":
                    // string userName = lvOnline.SelectedItems[0].SubItems[2].Text;
                    string displayName = lvOnline.SelectedItems[0].Text;
                    var request = new ChatMessage();
                    request.SendDisplayName = displayName;
                    request.ChatType = ChatType.PrivateChat;
                    request.SendUserName = actionStuUserName;
                    request.UserType = ClientRole.Student;
                    GlobalVariable.AddNewChat(request);
                    OpenOrCreateChatForm(request, false);

                    break;
                case "userList_lockScreen":

                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        GlobalVariable.client.Send_LockScreen(actionStuUserName);
                    }
                    break;
                case "userList_stopLockScreen":

                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        GlobalVariable.client.Send_StopLockScreen(actionStuUserName);
                    }
                    break;
                case "userList_studentShow":

                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        GlobalVariable.client.Send_CallStudentShow(actionStuUserName);
                    }
                    break;
                case "userList_studentVideoShow":

                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        GlobalVariable.client.Send_CallStudentShowVideoForMySelf(actionStuUserName);
                    }
                    break;
                case "userList_stopStudentShow":
                    StopPlay();

                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        GlobalVariable.client.Send_StopStudentShow(actionStuUserName);
                    }
                    break;

                default:
                    break;
            }

        }

        private void lvOnline_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem lvi = lvOnline.GetItemAt(e.X, e.Y);
                if (lvi != null)
                {
                    lvOnline.ContextMenuStrip = UserListMenu;
                }
                else
                {
                    lvOnline.ContextMenuStrip = null;
                }
                return;
            }
        }

        private void userList_P_forbidPrivateChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_ForbidPrivateChat(actionStuUserName);
            }
        }

        private void userList_P_forbidGroupChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_ForbidTeamChat(actionStuUserName);
            }
        }

        private void userList_P_allowPrivateChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_AllowPrivateChat(actionStuUserName);
            }
        }

        private void userList_P_allowGroupChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_AllowTeamChat(actionStuUserName);
            }
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.client.KillAllFFmpeg();
            udpClient.CloseTeacherUDP();
        }
    }
}
