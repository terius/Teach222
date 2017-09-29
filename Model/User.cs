using Common;
using System.Collections.Generic;

namespace Model
{
    public class User
    {
      //  IList<ChatMessage> messageList = new List<ChatMessage>();
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public ClientRole UserType { get; set; }

        public bool IsOnline { get; set; }

        public bool IsDianMing { get; set; }

        //public void AddMessage(ChatMessage message)
        //{
        //    messageList.Add(message);
        //}



        //public IList<ChatMessage> ChatMessageList { get { return messageList; } }
    }
}
