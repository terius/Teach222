using System.Collections.Generic;

namespace Model
{
    public static class Mapping
    {
        public static User ConvertToUser(this OnlineUserResponse source)
        {
            var user = new User();
            user.DisplayName = source.nickname;
            user.IsOnline = true;
            user.UserName = source.username;
            user.UserType = source.clientRole;
            return user;
        }

        public static IList<User> ConvertToUserList(this IList<OnlineUserResponse> source)
        {
            IList<User> dest = new List<User>();
            foreach (var item in source)
            {
                dest.Add(item.ConvertToUser());
            }
            return dest;

        }

        public static IList<User> ConvertToUserList(this List<TeamMember> source)
        {
            IList<User> dest = new List<User>();
            User user = null;
            foreach (var item in source)
            {
                user = new User { DisplayName = item.DisplayName, UserName = item.UserName };
                dest.Add(user);
            }
            return dest;
        }

        public static List<TeamMember> ConvertToTeamMember(this IList<User> source)
        {
            List<TeamMember> dest = new List<TeamMember>();
            TeamMember member = null;
            foreach (var item in source)
            {
                member = new TeamMember { DisplayName = item.DisplayName, UserName = item.UserName, IsOnline = item.IsOnline };
                dest.Add(member);
            }
            return dest;
        }

        public static Team ConvertToTeam(this TeamInfo source)
        {
            var team = new Team();
            team.TeamId = source.groupid;
            team.TeamName = source.groupname;
            team.UpdateTeamMembers(source.groupuserList);
            return team;
        }

        public static TeacherTeam ConvertToTeacherTeam(this IList<Team> source,string userName,string displayName)
        {
            var team = new TeacherTeam();
            team.DisplayName = displayName;
            team.UserName = userName;
            team.TeamInfos = new List<TeamInfo>();
            //  var list = GetTeamChatList();
            TeamInfo info;
            foreach (var item in source)
            {
                info = new TeamInfo();
                info.groupname = item.TeamName;
                info.groupid = item.TeamId;
                info.groupuserList = item.TeamMembers.ConvertToTeamMember();
                team.TeamInfos.Add(info);
            }
            return team;
        }
    }
}
