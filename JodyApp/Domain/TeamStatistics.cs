using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{    
    public class TeamStatistics:DomainObject
    {
        public enum Stats { Rank, Name, Wins, Loses, Ties, GoalsFor, GoalsAgainst, Points, GoalDifference, GamesPlayed }

        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GamesPlayed { get { return Wins + Loses + Ties; } }
        public int GoalDifference {  get { return GoalsFor - GoalsAgainst; } }
        public int Points {  get { return 2 * Wins + Ties; } }

        public TeamStatistics() { }
        public TeamStatistics(int rank, int wins, int loses, int ties, int goalsFor, int goalsAgainst)
        {
            Rank = rank;
            Wins = wins;
            Loses = loses;
            Ties = ties;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
        }
    }
}
