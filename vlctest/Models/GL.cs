using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlctest.Models
{
    public class GL
    {
        private static IList<Team> teamList = new List<Team>();
        public static IList<Team> TeamList { get { return teamList; } }

        public static void CreateTeam(string teamName)
        {
            if (teamList.Any(d=>d.TeamName == teamName))
            {
                throw new Exception("群组名称不能重复");
            }
            var team = new Team { TeamId = Guid.NewGuid().ToString(), TeamName = teamName };
            teamList.Add(team);
        }
    }
}
