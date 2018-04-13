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

        public Dictionary<string, RecordTableTeam> Standings { get; set; }

        public void Add(String TeamName, RecordTableTeam team)
        {
            this.Standings.Add(TeamName, team);
        }

        public RecordTable()
        {
            Standings = new Dictionary<string, RecordTableTeam>();
        }
  
        public List<RecordTableTeam> GetSortedListByConference()
        {
            return null;
        }

        public List<RecordTableTeam> GetSortedListByDivision()
        {
            return null;
        }

        public List<RecordTableTeam> GetSortListByDivisionLevel(int level)
        {
            return null;
        }
        public List<RecordTableTeam> GetSortedListByLeague()
        {
            List<RecordTableTeam> teams = Standings.Values.ToList<RecordTableTeam>();


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

        public Dictionary<RecordTableDivision,List<RecordTableTeam>> SortIntoDivisions()
        {
            var result = new Dictionary<RecordTableDivision, List<RecordTableTeam>>();

            foreach(KeyValuePair<string, RecordTableTeam> entry in Standings)
            {
                RecordTableTeam team = entry.Value;

                RecordTableDivision division = (RecordTableDivision)team.Division;

                while (division != null)
                {
                    if (!result.ContainsKey(division)) result.Add(division, new List<RecordTableTeam>());
                    result[division].Add(team);
                    division = (RecordTableDivision)division.Parent;
                }
            }

            return result;
        }

    }
}
