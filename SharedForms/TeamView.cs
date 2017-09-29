using Model;
using System;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class TeamView : MyForm
    {
    
        public TeamView()
        {
            InitializeComponent();
        }

       

        private void TeamView_Load(object sender, EventArgs e)
        {
           // toolTip1.SetToolTip(this.treeView1, "信息提示");
            this.treeView1.Nodes.Clear();
          //  var list = GlobalVariable.GetTeamChatList();
            TreeNode node;
            TreeNode childNode;
            foreach (var team in GlobalVariable.TeamList)
            {
                node = new TreeNode(team.TeamName);
                foreach (var member in team.TeamMembers)
                {
                    childNode = new TreeNode(member.DisplayName);
                    childNode.ImageIndex = childNode.SelectedImageIndex = member.IsOnline ? 2 : 3;
                    childNode.ToolTipText = member.IsOnline ? "在线" : "离线";
                    node.Nodes.Add(childNode);
                }
                this.treeView1.Nodes.Add(node);
            }
            treeView1.ExpandAll();
        }
    }
}
