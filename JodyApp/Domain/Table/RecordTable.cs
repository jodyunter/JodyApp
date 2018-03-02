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

        public RecordTable()
        {
            Standings = new Dictionary<string, RecordTableTeam>();
        }

        public void ProcessGame(Game game)
        {
            RecordTableTeam home = Standings[game.HomeTeam.Name];
            RecordTableTeam away = Standings[game.AwayTeam.Name];

            RecordTable.ProcessGame(game, home.Stats, away.Stats);
        }

        public static void ProcessGame(Game game, TeamStatitistics HomeStats, TeamStatitistics AwayStats)
        {            
            int homeScore = game.HomeScore;
            int awayScore = game.AwayScore;

            ProcessTeamStats(HomeStats, homeScore, awayScore);
            ProcessTeamStats(AwayStats, awayScore, homeScore);
        }

        public static void ProcessTeamStats(TeamStatitistics TeamStats, int GoalsFor, int GoalsAgainst)
        {
            if (GoalsFor > GoalsAgainst)
            {
                TeamStats.Wins++;
            } else if (GoalsFor < GoalsAgainst)
            {
                TeamStats.Loses++;
            } else
            {
                TeamStats.Ties++;
            }

            TeamStats.GoalsFor += GoalsFor;
            TeamStats.GoalsAgainst += GoalsAgainst;
        }

        public List<RecordTableTeam> GetSortedList()
        {
            List<RecordTableTeam> teams = Standings.Values.ToList<RecordTableTeam>();
            teams.Sort();
            teams.Reverse();

            int rank = 1;

            teams.ForEach(team =>
            {
                team.Stats.Rank = rank;
                rank++;
            });

            return teams;
        }

    }
}
