using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Table
{
    public class RecordTable
    {
        public String TableName = "";

        public Dictionary<string, Team> Standings { get; set; }

        public void Add(Team team)
        {
            this.Standings.Add(team.Name, team);
        }

        public RecordTable()
        {
            Standings = new Dictionary<string, Team>();
        }
  
        public List<Team> GetSortedListByConference()
        {
            return null;
        }

        public List<Team> GetSortedListByDivision()
        {
            return null;
        }

        public List<Team> GetSortListByDivisionLevel(int level)
        {
            return null;
        }
        public List<Team> GetSortedListByLeague()
        {
            List<Team> teams = Standings.Values.ToList<Team>();


            return teams;
        }

        //todo replace RecordTable with Season, and move functionality into RecordTable
        public void ProcessGame(Game game)
        {
            ProcessGame(game,
                this.Standings[game.HomeTeam.Name].Stats,
                this.Standings[game.AwayTeam.Name].Stats);
        }
        public void ProcessGame(Game game, TeamStatistics HomeStats, TeamStatistics AwayStats)
        {
            int homeScore = game.HomeScore;
            int awayScore = game.AwayScore;

            ProcessTeamStats(HomeStats, homeScore, awayScore);
            ProcessTeamStats(AwayStats, awayScore, homeScore);
        }

        public void ProcessTeamStats(TeamStatistics TeamStats, int GoalsFor, int GoalsAgainst)
        {
            if (GoalsFor > GoalsAgainst)
            {
                TeamStats.Wins++;
            }
            else if (GoalsFor < GoalsAgainst)
            {
                TeamStats.Loses++;
            }
            else
            {
                TeamStats.Ties++;
            }

            TeamStats.GoalsFor += GoalsFor;
            TeamStats.GoalsAgainst += GoalsAgainst;
        }

        public Dictionary<Division,List<Team>> SortIntoDivisions()
        {
            var result = new Dictionary<Division, List<Team>>();

            foreach(KeyValuePair<string, Team> entry in Standings)
            {
                Team team = entry.Value;

                Division division = (Division)team.Division;

                while (division != null)
                {
                    if (!result.ContainsKey(division)) result.Add(division, new List<Team>());
                    result[division].Add(team);
                    division = (Division)division.Parent;
                }
            }

            return result;
        }

    }
}
