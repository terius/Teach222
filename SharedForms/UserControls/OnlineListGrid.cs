using Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms.UserControls
{
    public partial class OnlineListGrid : DataGridView
    {
        public OnlineListGrid()
        {
            InitializeComponent();
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.RowHeadersVisible = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ReadOnly = true;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.AllowUserToResizeRows = false;
            this.MultiSelect = false;
            this.BackgroundColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void UpdateOnlineUser(IList<User> list)
        {
            this.Rows.Clear();
            foreach (var item in list)
            {
                string[] obs = new string[] { item.DisplayName, (item.IsDianMing ? "是" : "否"), "", "", item.UserName };
                this.Rows.Add(obs);
            }

        }

        public void RemoveOnlineUser(string userName)
        {
            foreach (DataGridViewRow item in this.Rows)
            {
                if (item.Cells["col_userName"].Value.ToString().Equals(userName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Rows.Remove(item);
                    break;
                }
            }
        }

        public void AddLoginUser(User onlineUser)
        {
            RemoveOnlineUser(onlineUser.UserName);
            string[] obs = new string[] { onlineUser.DisplayName, (onlineUser.IsDianMing ? "是" : "否"), "", "", onlineUser.UserName };
            this.Rows.Add(obs);
        }

        public void UpdateDianMing(string userName)
        {
            foreach (DataGridViewRow item in this.Rows)
            {
                if (item.Cells["col_userName"].Value.ToString().Equals(userName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Cells["col_isval"].Value = "是";
                    break;
                }
            }
        }

        public void SetPrivateChatPermission(string userName, bool isAllow)
        {
            foreach (DataGridViewRow item in this.Rows)
            {
                if (item.Cells["col_userName"].Value.ToString().Equals(userName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Cells["col_disablePrivateChat"].Value = isAllow ? "" : "是";
                    break;
                }
            }
        }

        public void SetTeamChatPermission(string userName, bool isAllow)
        {
            foreach (DataGridViewRow item in this.Rows)
            {
                if (item.Cells["col_userName"].Value.ToString().Equals(userName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Cells["col_disableTeamChat"].Value = isAllow ? "" : "是";
                    break;
                }
            }
        }
    }
}
