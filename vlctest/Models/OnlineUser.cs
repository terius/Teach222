using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlctest.Models
{
    public class OnlineUser
    {
        private IList<User> userList = new List<User>();

        public void AddUser(User user)
        {
            if (!userList.Any(d => d.UserName == user.UserName))
            {
                userList.Add(user);
            }
        }


        public IList<User> UserList { get { return userList; } }

    }

    public class ChatStore
    {
        IList<ChatMessage> messageList = new List<ChatMessage>();
        public string UserName { get; set; }
        public void AddMessage(ChatMessage message)
        {
            messageList.Add(message);
        }
        
        public IList<ChatMessage> ChatMessageList { get { return messageList; } }
    }

    public class User
    {
        IList<ChatMessage> messageList = new List<ChatMessage>();
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public ClientRole UserType { get; set; }

        public bool IsOnline { get; set; }

        public bool IsDianMing { get; set; }

        public void AddMessage(ChatMessage message)
        {
            messageList.Add(message);
        }

       

        public IList<ChatMessage> ChatMessageList { get { return messageList; } }
    }

    public class Team
    {
        private IList<User> userList = new List<User>();
        public string TeamId { get; set; }
        public string TeamName { get; set; }

        public IList<User> TeamMember { get { return userList; } }

        public void AddMember(User user)
        {
            if (!userList.Any(d => d.UserName == user.UserName))
            {
                userList.Add(user);
            }
        }
        IList<ChatMessage> messageList = new List<ChatMessage>();
        public IList<ChatMessage> ChatMessageList { get { return messageList; } }
        public void AddMessage(ChatMessage message)
        {
            messageList.Add(message);
        }

    }

    public class ChatMessage
    {
        /// <summary>
        /// 聊天类型
        /// </summary>
        public ChatType ChatType { get; set; }
        /// <summary>
        /// 发送者用户名
        /// </summary>
        public string SendUserName { get; set; }
        /// <summary>
        /// 发送者姓名
        /// </summary>
        public string SendDisplayName { get; set; }
        /// <summary>
        /// 接收者用户名
        /// </summary>
        public string ReceieveUserName { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 发送者身份
        /// </summary>
        public ClientRole UserType { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get { return SendDisplayName + " (" + SendTime.ToString("yyyy-MM-dd HH:mm:ss") + ")"; } }
    }


}
