using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    [Serializable]
    public class Team
    {
        private IList<User> _teamMembers = new List<User>();
        public string TeamId { get; set; }
        public string TeamName { get; set; }

        public IList<User> TeamMembers { get { return _teamMembers; } }

        public void AddMember(User user)
        {
            if (!_teamMembers.Any(d => d.UserName == user.UserName))
            {
                _teamMembers.Add(user);
            }
        }

        public void UpdateTeamMembers(List<TeamMember> mems)
        {
            _teamMembers = mems.ConvertToUserList();
        }

        public void AddMembers(IList<User> users)
        {
            foreach (var user in users)
            {
                AddMember(user);
            }
        }

        public bool CheckUserIsInTeam(string userName)
        {
            return _teamMembers.Any(d => d.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public void AddMember(string userName, string displayName)
        {
            var user = new User { UserName = userName, DisplayName = displayName, IsOnline = true, UserType = Common.ClientRole.Student };
            AddMember(user);
        }

        public void UpdateTeamName(string newName)
        {
            this.TeamName = newName;
        }


        public bool RemoveMember(string userName)
        {
            var user = _teamMembers.FirstOrDefault(d => d.UserName == userName);
            if (user != null)
            {
                return _teamMembers.Remove(user);
            }
            return false;
        }

        public void UpdateOnline(string userName, bool isOnLine)
        {
            foreach (var item in _teamMembers)
            {
                if (item.UserName.Equals(userName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    item.IsOnline = isOnLine;
                    break;
                }
            }
        }
        //IList<ChatMessage> messageList = new List<ChatMessage>();
        //public IList<ChatMessage> ChatMessageList { get { return messageList; } }
        //public void AddMessage(ChatMessage message)
        //{
        //    messageList.Add(message);
        //}

    }
}
