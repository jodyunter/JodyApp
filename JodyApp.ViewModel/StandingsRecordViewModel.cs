using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class StandingsRecordViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int Games { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        public StandingsRecordViewModel(string name, int rank, int wins, int loses, int ties, int games, int points, int goalsFor, int goalsAgainst, int goalDifference)
        {
            Name = name;
            Rank = rank;
            Wins = wins;
            Loses = loses;
            Ties = ties;
            Games = games;
            Points = points;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            GoalDifference = goalDifference;
        }


    }
}
