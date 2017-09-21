using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class TeacherTeam
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public List<TeamInfo> TeamInfos { get; set; }


    }

    public class TeamInfo
    {
        public string groupname { get; set; }
        public string groupid { get; set; }
        public List<TeamMember> groupuserList { get; set; }
    }
}
