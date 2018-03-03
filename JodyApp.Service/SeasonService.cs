using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;

namespace JodyApp.Service
{
    public class SeasonService
    {
        //todo replace RecordTable with Season
        public void ProcessGame(Game game, RecordTable table)
        {
            ProcessGame(game,
                table.Standings[game.HomeTeam.Name].Stats,
                table.Standings[game.AwayTeam.Name].Stats);
        }
        public void ProcessGame(Game game, TeamStatitistics HomeStats, TeamStatitistics AwayStats)
        {
            int homeScore = game.HomeScore;
            int awayScore = game.AwayScore;

            ProcessTeamStats(HomeStats, homeScore, awayScore);
            ProcessTeamStats(AwayStats, awayScore, homeScore);
        }

        public void ProcessTeamStats(TeamStatitistics TeamStats, int GoalsFor, int GoalsAgainst)
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
    }
}
