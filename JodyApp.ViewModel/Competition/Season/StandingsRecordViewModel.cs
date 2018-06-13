using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class StandingsRecordViewModel : BaseViewModel
    {
        public int Rank { get; set; }
        public string LeagueName { get; set; }
        public string DivisionName { get; set; }
        public string TeamName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int Points { get; set; }
        public int GamesPlayed { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        public StandingsRecordViewModel(int rank, string leagueName, string divisionName, string teamName, int wins, int loses, int ties, int points, int gamesPlayed, int goalsFor, int goalsAgainst, int goalDifference)
        {
            Rank = rank;
            LeagueName = leagueName;
            DivisionName = divisionName;
            TeamName = teamName;
            Wins = wins;
            Loses = loses;
            Ties = ties;
            Points = points;
            GamesPlayed = gamesPlayed;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            GoalDifference = goalDifference;
        }
    }
}
