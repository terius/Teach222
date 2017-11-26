using Common;
using Model;
using SharedForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewTeacher
{
    public partial class TeamDiscuss : MyForm
    {

        OnlineInfo _onLineInfo;
        Team selectTeam;
        bool teamIsChange;
        public TeamDiscuss(OnlineInfo onLineInfo)
        {
            InitializeComponent();
            myGroupBox2.Text = "在线" + GlobalVariable.ClientTitle;
            btnAddStudent.Text = "选择" + GlobalVariable.ClientTitle;
            _onLineInfo = onLineInfo;
            _onLineInfo.AddOnLine += _onLineInfo_AddOnLine;
            _onLineInfo.DelOnLine += _onLineInfo_DelOnLine;

        }

        private void _onLineInfo_DelOnLine(object sender, string delUserName)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                foreach (ListViewItem item in this.onLineListView.Items)
                {
                    if (item.SubItems[1].Text == delUserName)
                    {
                        item.Remove();
                        break;
                    }
                }
            });

        }

        private void _onLineInfo_AddOnLine(object sender, IList<User> e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                AddUserList(e);
            });
        }

        private void AddUserList(IList<User> onLineList)
        {
            foreach (var item in onLineList)
            {
                if (!item.UserName.IsMySelf())
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.DisplayName;
                    listItem.ImageIndex = item.UserType == ClientRole.Student ? 0 : 1;
                    listItem.SubItems.Add(item.UserName);
                    this.onLineListView.Items.Add(listItem);
                }
            }

        }

        private void TeamDiscuss_Load(object sender, System.EventArgs e)
        {

            BindOnlineUser();
            BindTeam();
        }

        private void BindOnlineUser()
        {
            foreach (var item in _onLineInfo.StudentOnlineList)
            {
                if (!item.UserName.IsMySelf())
                {
                    ListViewItem listItem = new ListViewItem();
                    //listItem.Name = item.clientStyle == ClientStyle.PC ? "计算机" : "移动端";
                    listItem.Text = item.DisplayName;
                    listItem.ImageIndex = item.UserType == ClientRole.Student ? 0 : 1;
                    listItem.SubItems.Add(item.UserName);

                    this.onLineListView.Items.Add(listItem);
                }
            }
        }

        private void btnCreate_Click(object sender, System.EventArgs e)
        {
            if (GlobalVariable.CreateTeam(this.txtCreate.Text.Trim()))
            {
                teamIsChange = true;
                UpdateTeamCBox();
            }
        }

        private void UpdateTeamCBox()
        {
            var newItem = GlobalVariable.GetNewestTeamChat();
            cboxTeam.Items.Add(newItem);
            cboxTeam2.Items.Add(newItem);
            if (cboxTeam.SelectedIndex <= 0)
            {
                cboxTeam.SelectedIndex = 0;
            }

            if (cboxTeam2.SelectedIndex <= 0)
            {
                cboxTeam2.SelectedIndex = 0;
            }
        }

        private void BindTeam()
        {
            cboxTeam.Items.Clear();
            cboxTeam2.Items.Clear();
            teamMemList.Clear();
            //   var list = GlobalVariable.GetTeamChatList();
            foreach (var team in GlobalVariable.TeamList)
            {
                cboxTeam.Items.Add(team);
                cboxTeam2.Items.Add(team);
            }
            cboxTeam.DisplayMember = cboxTeam2.DisplayMember = "TeamName";
            cboxTeam.ValueMember = cboxTeam2.ValueMember = "TeamId";
            if (cboxTeam.Items.Count > 0)
            {
                cboxTeam.SelectedIndex = 0;
            }
            if (cboxTeam2.Items.Count > 0)
            {
                cboxTeam2.SelectedIndex = 0;
            }
        }

        private void cboxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cboxTeam.SelectedIndex >= 0)
            {
                selectTeam = (Team)cboxTeam.SelectedItem;
                fmEditTeamName editForm = new fmEditTeamName(selectTeam.TeamId, selectTeam.TeamName);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    teamIsChange = true;
                    BindTeam();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboxTeam.SelectedIndex >= 0)
            {
                selectTeam = (Team)cboxTeam.SelectedItem;

                Action<string, IList<User>> sendDelCommand = (teamId, member) =>
                 {
                     foreach (var item in member)
                     {
                         SendDelMemberCommand(teamId, item.UserName, true);
                         //  SendDelMemberCommand(item.)
                     }
                 };
                var rs = GlobalVariable.RemoveTeam(selectTeam.TeamId, sendDelCommand);
                if (rs)
                {

                    teamIsChange = true;
                    GlobalVariable.ShowSuccess("分组删除成功");
                    BindTeam();
                }
                else
                {
                    GlobalVariable.ShowError("分组删除失败");
                }
            }
        }



        private void BindTeamMember()
        {
            if (cboxTeam2.SelectedIndex >= 0)
            {
                selectTeam = (Team)cboxTeam2.SelectedItem;
                myGroupBox3.Text = "分组：" + selectTeam.TeamName + "的成员";
                teamMemList.Clear();
                foreach (var mem in selectTeam.TeamMembers)
                {
                    ListViewItem listItem = new ListViewItem();
                    //listItem.Name = item.clientStyle == ClientStyle.PC ? "计算机" : "移动端";
                    listItem.Text = mem.DisplayName;
                    listItem.ImageIndex = 0;
                    listItem.SubItems.Add(mem.UserName);

                    this.teamMemList.Items.Add(listItem);
                }


                //TeacherTeam request = new TeacherTeam();
                //request.groupguid = selectTeam.ChatUserName;
                //request.groupname = selectTeam.ChatDisplayName;
                //request.groupuserList = selectTeam.TeamMembers.Select(d => d.UserName).ToArray();
                //request.nickname = GlobalVariable.LoginUserInfo.DisplayName;
                //request.username = GlobalVariable.LoginUserInfo.UserName;
                //GlobalVariable.client.Send_CreateTeam(request);


            }
        }

        private void memDel_Click(object sender, EventArgs e)
        {
            if (cboxTeam.SelectedIndex >= 0)
            {
                selectTeam = (Team)cboxTeam.SelectedItem;

                string userName = this.teamMemList.SelectedItems[0].SubItems[1].Text;
                bool rs = GlobalVariable.RemoveTeamMember(selectTeam.TeamId, userName);
                if (rs)
                {
                    SendDelMemberCommand(selectTeam.TeamId, userName);
                    teamIsChange = true;
                }
                BindTeamMember();
            }
        }

        private void SendDelMemberCommand(string teamId, string userName, bool isDeleteTeam = false)
        {
            DeleteTeamMemberRequest request = new DeleteTeamMemberRequest();
            request.TeamId = teamId;
            request.UserName = userName;
            request.IsDeleteTeam = isDeleteTeam;
            GlobalVariable.SendCommand(request, CommandType.DeleteUserInGroup);
        }

        private void teamMemList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem lvi = teamMemList.GetItemAt(e.X, e.Y);
                if (lvi != null)
                {
                    teamMemList.ContextMenuStrip = memberMenu;
                }
                else
                {
                    teamMemList.ContextMenuStrip = null;
                }
                return;
            }
        }

        private void cboxTeam2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTeamMember();
        }


        private void btnAddStudent_Click_1(object sender, EventArgs e)
        {
            if (onLineListView.CheckedItems.Count <= 0)
            {
                GlobalVariable.ShowWarnning("请先选择要添加的客户端");
                return;
            }

            if (cboxTeam2.SelectedIndex < 0)
            {
                GlobalVariable.ShowWarnning("请先选择要添加的分组");
                return;
            }
            selectTeam = (Team)cboxTeam2.SelectedItem;
            GlobalVariable.AddTeamMembers(ConvertToUserList(onLineListView), selectTeam.TeamId);
            teamIsChange = true;
            BindTeamMember();
        }

        private IList<User> ConvertToUserList(ListView listView)
        {
            IList<User> users = new List<User>();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                users.Add(new User { DisplayName = item.Text, UserName = item.SubItems[1].Text, IsOnline = true, UserType = ClientRole.Student });
            }
            return users;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariable.SendCommand_CreateOrUpdateTeam();
            teamIsChange = false;
            MessageBox.Show("群组信息保存成功");
        }

        private void TeamDiscuss_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (teamIsChange)
            {
                if (MessageBox.Show("是否要保存群组信息？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    GlobalVariable.SendCommand_CreateOrUpdateTeam();
                }
            }
        }
    }
}
