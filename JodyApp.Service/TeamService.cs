using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;

namespace JodyApp.Service
{
    public class TeamService
    {
        List<Team> GetAllTeams()
        {
            var teams = new List<Team>();

            for (int i = 0; i < 21; i++)
            {
                teams.Add(new Team { Name = "Team " + i, Skill = 5 });
            }

            return teams;
        }

        public RecordTable GetStandings()
        {
            return GetStandings(GetAllTeams());
        }

        public RecordTable GetStandings(List<Team> teams)
        {
            RecordTable table = new RecordTable();

            teams.ForEach(team =>
            {
                table.Standings.Add(team.Name, new RecordTableTeam(team));
            });

            return table;
        }

    }
}
