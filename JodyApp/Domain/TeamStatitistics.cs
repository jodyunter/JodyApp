using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{    
    public class TeamStatitistics
    {
        public enum Stats { Name, Wins, Loses, Ties, GoalsFor, GoalsAgainst, Points, GoalDifference, GamesPlayed }

        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GamesPlayed { get { return Wins + Loses + Ties; } }
        public int GoalDifference {  get { return GoalsFor - GoalsAgainst; } }
        public int Points {  get { return 2 * Wins + Ties; } }

    }
}
