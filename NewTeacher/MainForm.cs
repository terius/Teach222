using Common;
using EduService;
using Helpers;
using Model;
using NewTeacher.Controls;
using NewTeacher.Properties;
using SharedForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NewTeacher
{
    public partial class MainForm : MyForm
    {
        #region 自定义字段
        /// <summary>
        /// 在线用户类
        /// </summary>
        OnlineInfo onlineInfo;

        /// <summary>
        /// 聊天窗体
        /// </summary>
        ChatForm chatForm;

        /// <summary>
        /// 是否正在推送视频流
        /// </summary>
        bool isPush = false;

        /// <summary>
        ///  当前操作的学生用户名
        /// </summary>
        string actionStuUserName;

        /// <summary>
        ///  当前操作的学生姓名
        /// </summary>
        string actionStuTrueName;

        /// <summary>
        /// 视频播放窗体
        /// </summary>
        VideoShow videoForm;

        /// <summary>
        /// UDP控制类
        /// </summary>
        EduUDPClient udpClient;


        /// <summary>
        /// 是否正在学生演示
        /// </summary>
        bool isStudentShowing;

        /// <summary>
        /// 学生端标题
        /// </summary>
        string _clientTitle = "学生端";

        /// <summary>
        /// 聊天窗体是否已打开
        /// </summary>
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
        #endregion
        public MainForm()
        {
            InitializeComponent();
            //flowLayoutPanel1.MouseWheel += FlowLayoutPanel1_MouseWheel;
            //for (int i = 0; i < 30; i++)
            //{
            //    ScreenCaptureInfo sc = new ScreenCaptureInfo();
            //    sc.UserName = "username" + i;
            //    sc.DisplayName = "disname" + i;
            //    AddStudentScreenToPanel(sc);
            //}
        

            CreateFilePath();
            this.onlineListGrid1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            _clientTitle = GlobalVariable.ClientTitle;
            this.Text = GlobalVariable.SystemTitle;
            menuStudentShow.Text = GlobalVariable.ClientTitle + "演示";
            myGroupBox7.Text = "在线" + GlobalVariable.ClientTitle + "列表";
            myGroupBox8.Text = GlobalVariable.ClientTitle + "屏幕";
            if (GlobalVariable.IsHuiShenXiTong)
            {


                menuClassNamed.Hide();
                menuExportSign.Hide();
                menuFileShare.Hide();
                menuRomoteControl.Hide();
                tableLayoutPanel1.ColumnCount = 1;
                tableLayoutPanel1.RowCount = 1;
                myGroupBox1.Text = "群聊";
                menuGroupChat.Text = "群组聊天";
                onlineListGrid1.Columns["col_isval"].Visible = false;
                //  tableLayoutPanel3.ColumnStyles[4].Width = 0f;
            }

            GlobalVariable.client.OnClentIsConnecting += Client_OnClentIsConnecting;
            InitOnlineInfo();
            GlobalVariable.LoadTeamFromXML();

            #region 接收消息事件
            GlobalVariable.client.OnTeacherReceiveMessage = (message) =>
            {
                switch ((CommandType)message.Action)
                {
                    case CommandType.OnlineList:
                        var userList2 = JsonHelper.DeserializeObj<List<OnlineUserResponse>>(message.DataStr);

                        onlineInfo.OnOnlineChange(userList2);
                        break;
                    case CommandType.StudentShowToTeacher:
                    case CommandType.ScreenInteract:
                        ScreenInteract_Response resp = JsonHelper.DeserializeObj<ScreenInteract_Response>(message.DataStr);
                        this.InvokeOnUiThreadIfRequired(() =>
                        {
                            PlayRtspVideo(resp.url);
                        });
                        break;
                    case CommandType.StopScreenInteract:
                        this.InvokeOnUiThreadIfRequired(() =>
                        {
                            StopPlay();
                        });
                        break;

                    case CommandType.PrivateChat:
                    case CommandType.TeamChat:
                    case CommandType.GroupChat:
                        DoReceiveChatMessage(message);
                        break;
                    case CommandType.OneUserLogIn:
                        var newUser = JsonHelper.DeserializeObj<List<OnlineUserResponse>>(message.DataStr);
                        onlineInfo.OnNewUserLoginIn(newUser);
                        break;
                    case CommandType.UserLoginOut:
                        var loginoutInfo = JsonHelper.DeserializeObj<UserLogoutResponse>(message.DataStr);
                        onlineInfo.OnUserLoginOut(loginoutInfo);
                        DeleteScreen(loginoutInfo);
                        break;
                    case CommandType.StudentCall:
                        var callInfo = JsonHelper.DeserializeObj<StuCallRequest>(message.DataStr);
                        UpdateOnLineStatus(callInfo);
                        break;
                    case CommandType.CreateTeam://收到创建群组信息
                        var teamInfo = JsonHelper.DeserializeObj<TeacherTeam>(message.DataStr);
                        GlobalVariable.LoadTeamList(teamInfo);
                        this.InvokeOnUiThreadIfRequired(() =>
                        {
                            if (GlobalVariable.CheckChatFormIsOpened())
                            {
                                GlobalVariable.ShowNotifyMessage("群组信息已经更改");
                                chatForm.ReflashTeamChat();
                            }
                        });
                        break;
                    default:
                        break;
                }
            };
            #endregion

            GlobalVariable.client.DueLostMessage();
            GlobalVariable.client.Send_OnlineList();
        }

        int lastRightPanelVerticalScrollValue = -1;//为鼠标滚动事件提供一个静态变量，用来存储上次滚动后的VerticalScroll.Value
        private void FlowLayoutPanel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!(flowLayoutPanel1.VerticalScroll.Visible == false
                || (flowLayoutPanel1.VerticalScroll.Value == 0 && e.Delta > 0) 
                || (flowLayoutPanel1.VerticalScroll.Value == lastRightPanelVerticalScrollValue && e.Delta < 0)))
            {
                flowLayoutPanel1.VerticalScroll.Value += 10;
                lastRightPanelVerticalScrollValue = flowLayoutPanel1.VerticalScroll.Value;
                flowLayoutPanel1.Refresh();
                flowLayoutPanel1.Invalidate();
                flowLayoutPanel1.Update();
            }
        }


        /// <summary>
        /// 处理收到的聊天信息
        /// </summary>
        /// <param name="message"></param>
        private void DoReceiveChatMessage(ReceieveMessage message)
        {
            var chatMessage = GlobalVariable.CreateChatMessage(message);
            this.InvokeOnUiThreadIfRequired(() =>
            {
                OpenOrCreateChatForm(chatMessage, true);
            });
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
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
                videoForm.FormClosing += VideoForm_FormClosing;
            }
            videoForm.BringToFront();
            videoForm.Show();
            Thread.Sleep(4000);
            videoForm.PlayVideo(rtsp);

        }

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopPlay()
        {
            
            if (videoForm != null)
            {
                videoForm.Close();
                videoForm = null;
            }

        }

        //private void ReceieveTeamMessage(TeamChatRequest message)
        //{
        //    var request = message.ToChatMessage();
        //    GlobalVariable.AddNewChat(request);
        //    OpenOrCreateChatForm(request, true);
        //}

        //private void ReceieveGroupMessage(GroupChatRequest message)
        //{
        //    var request = message.ToChatMessage();
        //    GlobalVariable.AddNewChat(request);
        //    OpenOrCreateChatForm(request, true);
        //}


        //private void ReceievePrivateMessage(PrivateChatRequest message)
        //{
        //    var request = message.ToChatMessage();
        //    GlobalVariable.AddNewChat(request);
        //    OpenOrCreateChatForm(request, true);
        //}

        private void UpdateOnLineStatus(StuCallRequest callInfo)
        {
            foreach (var item in onlineInfo.StudentOnlineList)
            {
                if (item.UserName == callInfo.username)
                {
                    item.IsDianMing = true;
                    break;
                }
            }
            this.InvokeOnUiThreadIfRequired(() =>
            {
                onlineListGrid1.UpdateDianMing(callInfo.username);
                //foreach (ListViewItem item in lvOnline.Items)
                //{
                //    // string nickname = item.Text;
                //    //   string no = item.SubItems[3].Text;
                //    string userName = item.SubItems[2].Text;
                //    if (userName == callInfo.username)
                //    {
                //        item.SubItems[1].Text = "是";
                //        break;
                //    }
                //}

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
        private void OnlineInfo_DelOnLine(object sender, string delUserName)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                onlineListGrid1.RemoveOnlineUser(delUserName);
            });
        }

        private void OnlineInfo_AddOnLine(object sender, IList<User> e)
        {
            this.InvokeOnUiThreadIfRequired(() => AddOnlineUser(e));
        }

        private void AddOnlineUser(IList<User> list)
        {

            onlineListGrid1.AddLoginUser(list[0]);
        }

        private bool IsMySelf(string userName)
        {
            return userName == GlobalVariable.LoginUserInfo.UserName;
        }

        private void OnlineInfo_OnLineChange(object sender, IList<User> e)
        {
            this.InvokeOnUiThreadIfRequired(() => userListShow(e));
        }

        /// <summary>
        /// 显示在线用户列表
        /// </summary>
        /// <param name="onLineList"></param>
        private void userListShow(IList<User> list)
        {
            this.onlineListGrid1.UpdateOnlineUser(list);
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



        private void GetSelectStudentUserName()
        {
            actionStuUserName = null;
            if (onlineListGrid1.Rows.Count <= 0)
            {
                GlobalVariable.ShowWarnning("当前在线" + _clientTitle + "为空");
                return;
            }
            if (onlineListGrid1.SelectedRows.Count <= 0)
            {
                GlobalVariable.ShowWarnning("请先选择" + _clientTitle);
                return;
            }

            actionStuUserName = onlineListGrid1.SelectedRows[0].Cells["col_userName"].Value.ToString();
            actionStuTrueName = onlineListGrid1.SelectedRows[0].Cells["col_name"].Value.ToString();


            //old
            //actionStuUserName = null;
            //if (lvOnline.Items.Count <= 0)
            //{
            //    GlobalVariable.ShowWarnning("当前在线" + _clientTitle + "为空");
            //    return "";
            //}
            //if (lvOnline.SelectedItems.Count <= 0)
            //{
            //    GlobalVariable.ShowWarnning("请先选择" + _clientTitle);
            //    return "";
            //}
            //string username = lvOnline.SelectedItems[0].SubItems[2].Text;
            //actionStuUserName = username;
            //return username;
        }

        private void ChatToALL()
        {
            var request = new ChatMessage();
            request.SendDisplayName = "所有人";
            request.ChatType = ChatType.GroupChat;
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
            chatForm.CreateChatItems(request, fromReceMsg);
            if (fromReceMsg && !ChatFormIsVisible)
            {
                GlobalVariable.ShowChatMessageNotify(request, chatForm);
            }
            else
            {
                chatForm.Show();
            }

        }

        /// <summary>
        /// 导出签到
        /// </summary>
        private void ExportSign()
        {
            //var onlineList = onlineInfo.GetStudentOnlineList();
            if (onlineInfo == null || onlineInfo.StudentOnlineList.Count <= 0)
            {
                GlobalVariable.ShowWarnning("当前登陆" + _clientTitle + "为空");
                return;
            }
            var table = new System.Data.DataTable();
            table.Columns.Add(_clientTitle + "姓名", typeof(string));
            table.Columns.Add("是否签到", typeof(string));
            foreach (var item in onlineInfo.StudentOnlineList)
            {
                if (item.UserType == ClientRole.Student)
                {
                    System.Data.DataRow dr = table.NewRow();
                    dr[0] = item.DisplayName;
                    dr[1] = item.IsDianMing ? "是" : "否";
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


        #region  主菜单按钮事件
        private void menuClassNamed_Click(object sender, System.EventArgs e)
        {
            //VideoShow vs = new VideoShow(ProgramType.Student, "stu213123");
            //vs.Show();
            //vs.PlayVideo(@"D:\qh\0811.mp4");

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

        /// <summary>
        /// 屏幕主菜单
        /// </summary>
        /// <param name="type"></param>
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
                    //if (lvOnline.SelectedItems.Count <= 0)
                    //{
                    //    GlobalVariable.ShowWarnning("请先选择要控制的" + _clientTitle);
                    //    return;
                    //}
                    //string username = lvOnline.SelectedItems[0].SubItems[2].Text;
                    GetSelectStudentUserName();
                    if (!string.IsNullOrWhiteSpace(actionStuUserName))
                    {
                        if (menuRomoteControl.Text == "禁用键鼠")
                        {
                            GlobalVariable.client.Send_LockScreen(actionStuUserName);
                            menuRomoteControl.Text = "解锁";
                        }
                        else
                        {
                            GlobalVariable.client.Send_StopLockScreen(actionStuUserName);
                            menuRomoteControl.Text = "禁用键鼠";
                        }
                    }
                    break;
                case TeacherAction.menuScreenShare_Click:
                    string text = menuScreenShare.Text;
                    if (text == "屏幕广播")
                    {
                        if (!isPush)
                        {
                        
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
                    if (!isStudentShowing)
                    {

                        GetSelectStudentUserName();
                        if (!string.IsNullOrWhiteSpace(actionStuUserName))
                        {

                            GlobalVariable.client.Send_CallStudentShow(actionStuUserName);
                            menuStudentShow.Text = "关闭演示";
                            isStudentShowing = true;
                        }

                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(actionStuUserName))
                        {
                            GlobalVariable.client.Send_StopStudentShow(actionStuUserName);
                            actionStuUserName = null;
                            menuStudentShow.Text = _clientTitle + "演示";
                            isStudentShowing = false;
                        }

                    }
                    break;
                case TeacherAction.menuVideoLive_Click:
                    string menuVideoLiveText = menuVideoLive.Text;
                    if (menuVideoLiveText == "视频直播")
                    {
                        if (!isPush)
                        {
                        
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
                        GlobalVariable.BeginRecordVideo();
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

        #endregion



        #region 用户列表右键操作事件
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            GlobalVariable.client.Send_OnlineList();
        }


        /// <summary>
        /// 用户列表右键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    string displayName = onlineListGrid1.SelectedRows[0].Cells["col_name"].Value.ToString();
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
                        HideScreenShowPic(actionStuUserName);
                        GlobalVariable.client.Send_StopStudentShow(actionStuUserName);
                    }
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// 用户列表禁止私聊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_P_forbidPrivateChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_ForbidPrivateChat(actionStuUserName);
                onlineListGrid1.SetPrivateChatPermission(actionStuUserName, false);
            }
        }

        /// <summary>
        /// 用户列表禁止群聊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_P_forbidGroupChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_ForbidTeamChat(actionStuUserName);
                onlineListGrid1.SetTeamChatPermission(actionStuUserName, false);
            }
        }

        /// <summary>
        /// 用户列表允许私聊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_P_allowPrivateChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_AllowPrivateChat(actionStuUserName);
                onlineListGrid1.SetPrivateChatPermission(actionStuUserName, true);
            }
        }

        /// <summary>
        /// 用户列表允许群聊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_P_allowGroupChat_Click(object sender, System.EventArgs e)
        {
            GetSelectStudentUserName();
            if (!string.IsNullOrWhiteSpace(actionStuUserName))
            {
                GlobalVariable.client.Send_AllowTeamChat(actionStuUserName);
                onlineListGrid1.SetTeamChatPermission(actionStuUserName, true);
            }
        }

        #endregion



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.KillAllFFmpeg();
            if (udpClient != null)
            {
                udpClient.CloseTeacherUDP();
            }
        }

        private void onlineListGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = onlineListGrid1.HitTest(e.X, e.Y);
                if (hti.RowIndex >= 0)
                {
                    onlineListGrid1.ClearSelection();
                    onlineListGrid1.Rows[hti.RowIndex].Selected = true;
                    onlineListGrid1.ContextMenuStrip = UserListMenu;
                }
                else
                {
                    onlineListGrid1.ContextMenuStrip = null;
                }
            }
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel1.Focus();
        }
    }
}
