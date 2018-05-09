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

        public const int POSITIONS_FROM_BOTTOM = -5000;
        

        public Playoff Playoff { get; set; }                    

        public int RuleType { get; set; }
        //example of different sorty and from
        //we sort by league
        //but we take DivA, 1,2,3 and DivB, 4,5 and ConferenceA, 1
        //no rounds because we might want DivA 1 to go directly to the final where as the winners of other places will be put in other groups
        public Division SortByDivision { get; set; } //this is the division rankings that ranks the teams
        public Division FromDivision { get; set; }        
        public String SeriesName { get; set; } //this means that all series need to be created at playoff creation time
        public int FromStartValue { get; set; } //ranking or WINNER/LOSER, or start = 1 or start = 2
        public int FromEndValue { get; set; } //division rankings 1, 10
        public Team FromTeam { get; set; }
        public bool IsHomeTeam { get; set; } //use this if we have teams and no sort division
        public String GroupIdentifier { get; set; }  //this is how we know which rules go with which group
        public string Name { get; set; }
        public GroupRule() { }
        public GroupRule(GroupRule rule, Division sortByDivision, Division fromDivision, Team team, Playoff p) : this(p, rule.SeriesName, rule.RuleType, sortByDivision, fromDivision,
                                                                                                        rule.SeriesName, rule.FromStartValue, rule.FromEndValue, team,
                                                                                                        rule.IsHomeTeam, rule.GroupIdentifier)
        { }

        public GroupRule(Playoff playoff, string name, int ruleType, Division sortByDivision, Division fromDivision, 
                            String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, 
                            bool isHomeTeam, string groupIdentifier)
        {            
            Playoff = playoff;
            RuleType = ruleType;
            SortByDivision = sortByDivision;
            FromDivision = fromDivision;
            SeriesName = seriesName;
            FromStartValue = fromStartValue;
            FromEndValue = fromEndValue;
            FromTeam = fromTeam;
            IsHomeTeam = isHomeTeam;
            GroupIdentifier = groupIdentifier;
            Name = name;
        }


        public static GroupRule CreateFromDivision(Playoff p, string name, string groupIdentifier, Division sortByDivision, Division fromDivision, int highestRank, int lowestRank)
        {
            if (sortByDivision == null) throw new ApplicationException("Cannot create new Gorup rule from division if SortByDivision is null");

            return new GroupRule(p, name, GroupRule.FROM_DIVISION, sortByDivision, fromDivision, null, highestRank, lowestRank, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromSeriesWinner(Playoff p, string name, string groupIdentifier, string seriesName, Division sortByDivision)
        {
            return new GroupRule(p, name, GroupRule.FROM_SERIES, sortByDivision, null, seriesName, GroupRule.SERIES_WINNER, GroupRule.SERIES_WINNER, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromSeriesLoser(Playoff p, string name, string groupIdentifier, string seriesName, Division sortByDivision)
        {
            return new GroupRule(p, name, GroupRule.FROM_SERIES, sortByDivision, null, seriesName, GroupRule.SERIES_LOSER, GroupRule.SERIES_LOSER, null, true, groupIdentifier);
        }

        public static GroupRule CreateFromTeam(Playoff p, string name, string groupIdentifier, Team team, bool isHomeTeam)
        {
            return new GroupRule(p, name, GroupRule.FROM_TEAM, null, null, null, 1, 1, team, isHomeTeam, groupIdentifier);
        }
    }
}
