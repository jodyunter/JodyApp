using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class GroupRule:DomainObject
    {
        public const int SERIES_WINNER = 0;
        public const int SERIES_LOSER = 1;

        public const int FROM_DIVISION = 0;
        public const int FROM_TEAM = 1;
        public const int FROM_SERIES = 2;

        public League League { get; set; }
        public Playoff Playoff { get; set; }                    

        public int RuleType { get; set; }
        //example of different sorty and from
        //we sort by league
        //but we take DivA, 1,2,3 and DivB, 4,5 and ConferenceA, 1
        //no rounds because we might want DivA 1 to go directly to the final where as the winners of other places will be put in other groups
        public Division SortByDivision { get; set; } //this is the division rankings that ranks the teams
        public Division FromDivision { get; set; }        
        public Series FromSeries { get; set; } //this means that all series need to be created at playoff creation time
        public int FromStartValue { get; set; } //ranking or WINNER/LOSER, or start = 1 or start = 2
        public int FromEndValue { get; set; } //division rankings 1, 10
        public Team FromTeam { get; set; }
        bool IsHomeTeam { get; set; } //use this if we have teams and no sort division
        public String GroupIdentifier { get; set; }  //this is how we know which rules go with which group

        public GroupRule(League league, Playoff playoff, int ruleType, Division sortByDivision, Division fromDivision, Series fromSeries, int fromStartValue, int fromEndValue, Team fromTeam, bool isHomeTeam, string groupIdentifier)
        {
            League = league;
            Playoff = playoff;
            RuleType = ruleType;
            SortByDivision = sortByDivision;
            FromDivision = fromDivision;
            FromSeries = fromSeries;
            FromStartValue = fromStartValue;
            FromEndValue = fromEndValue;
            FromTeam = fromTeam;
            IsHomeTeam = isHomeTeam;
            GroupIdentifier = groupIdentifier;
        }


        public static GroupRule CreateFromDivision(League league, string groupIdentifier, Division sortByDivision, Division fromDivision, int highestRank, int lowestRank)
        {
            return new GroupRule(league, null, GroupRule.FROM_DIVISION, sortByDivision, fromDivision, null, highestRank, lowestRank, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromSeriesWinner(League league, string groupIdentifier, Series series, Division sortByDivision)
        {
            return new GroupRule(league, null, GroupRule.SERIES_WINNER, sortByDivision, null, series, GroupRule.SERIES_WINNER, GroupRule.SERIES_WINNER, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromSeriesLoser(League league, string groupIdentifier, Series series, Division sortByDivision)
        {
            return new GroupRule(league, null, GroupRule.SERIES_WINNER, sortByDivision, null, series, GroupRule.SERIES_LOSER, GroupRule.SERIES_LOSER, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromTeam(League league, string groupIdentifier, Team team, bool isHomeTeam)
        {
            return new GroupRule(league, null, GroupRule.FROM_TEAM, null, null, null, 1, 1, team, isHomeTeam, groupIdentifier);
        }
    }
}
