using Common;
using System.Linq;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatItem : UserControl
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public ChatType ChatType { get; set; }
        public ChatItem(string userName, string displayName, ChatType chatType, ClientRole userType)
        {
            InitializeComponent();
            Name = "item_" + userName;
            labName.Text = displayName;
            switch (chatType)
            {
                case ChatType.PrivateChat:
                    switch (userType)
                    {
                        case ClientRole.Teacher:
                        case ClientRole.Assistant:
                            this.pictureBox1.Image = Resource1.老师24;
                            break;
                        case ClientRole.Student:
                            this.pictureBox1.Image = Resource1.学生24;
                            break;
                        default:
                            break;
                    }
                    //source.pan. Controls.Add(this);
                    //  this.paren = source.Groups[2];

                    break;
                case ChatType.GroupChat:
                    pictureBox1.Image = Resource1.所有人24;
                    //  source.Controls.Add(this);
                    //   this.Group = source.Groups[0];
                    break;
                case ChatType.TeamChat:
                    pictureBox1.Image = Resource1.群组24;
                    //    source.Controls.Add(this);
                    //  this.Group = source.Groups[1];
                    var childList = GlobalVariable.GetTeamMemberDisplayNames(userName);
                    // Caption = displayName + " 【" + childList.Count + "】";
                    //   Hint = string.Join("\r\n", childList);
                    // this.ToolTipText = string.Join("\r\n", childList);

                    break;
                default:
                    break;
            }
         
            this.UserName = userName;
            this.DisplayName = displayName;
            this.ChatType = chatType;
           // source.AddChatItem(this);
            //  this.AppearanceHotTracked.BorderColor = System.Drawing.Color.Black;
            //    this.AppearanceHotTracked.Options.UseBorderColor = true;
            // source.Items.Add(this);
        }


        public void SetNewMessagePic()
        {
            this.picNewMessage.Image = Resource1.新消息24;
        }

        public ChatStore GetChatStore()
        {
            return GlobalVariable.ChatList.FirstOrDefault(d => d.ChatUserName == UserName);
        }

        public string GetTeamMemUserNames()
        {
            var chat = GetChatStore();
            return string.Join(",", chat.TeamMembers.Select(d => d.UserName));
        }
    }
}
