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
        public const int FROM_DIVISION_BOTTOM = 3;        
        

        virtual public Group Group { get; set; }        

        public int RuleType { get; set; }
        //example of different sorty and from
        //we sort by league
        //but we take DivA, 1,2,3 and DivB, 4,5 and ConferenceA, 1
        //no rounds because we might want DivA 1 to go directly to the final where as the winners of other places will be put in other groups        
        virtual public Division FromDivision { get; set; }        
        public String SeriesName { get; set; } //this means that all series need to be created at playoff creation time
        public int FromStartValue { get; set; } //ranking or WINNER/LOSER, or start = 1 or start = 2, or start at bottom and use end value to go backwards
        public int FromEndValue { get; set; } //division rankings 1, 10
        virtual public Team FromTeam { get; set; }
        public bool IsHomeTeam { get; set; } //use this if we have teams and no sort division        
        public string Name { get; set; }
        public GroupRule() { }
        public GroupRule(GroupRule rule, Division fromDivision, Team team, Group g) : this(g, rule.SeriesName, rule.RuleType, fromDivision,
                                                                                                        rule.SeriesName, rule.FromStartValue, rule.FromEndValue, team,
                                                                                                        rule.IsHomeTeam)
        { }

        public GroupRule(Group group, string name, int ruleType, Division fromDivision, 
                            String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, 
                            bool isHomeTeam)
        {
            Group = group;
            RuleType = ruleType;            
            FromDivision = fromDivision;
            SeriesName = seriesName;
            FromStartValue = fromStartValue;
            FromEndValue = fromEndValue;
            FromTeam = fromTeam;
            IsHomeTeam = isHomeTeam;            
            Name = name;
        }


        public static GroupRule CreateFromDivision(Group g, string name,Division fromDivision, int highestRank, int lowestRank)
        {            
            var rule =  new GroupRule(g, name, GroupRule.FROM_DIVISION, fromDivision, null, highestRank, lowestRank, null, true);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromDivisionBottom(Group g, string name, Division fromDivision, int highestRank, int lowestRank)
        {           
            var rule = new GroupRule(g, name, GroupRule.FROM_DIVISION_BOTTOM, fromDivision, null, highestRank, lowestRank, null, true);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromSeriesWinner(Group g, string name, string seriesName)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_WINNER, GroupRule.SERIES_WINNER, null, true);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromSeriesLoser(Group g, string name, string seriesName)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_LOSER, GroupRule.SERIES_LOSER, null, true);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromTeam(Group g, string name, Team team, bool isHomeTeam)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_TEAM, null, null, 1, 1, team, isHomeTeam);
            g.GroupRules.Add(rule);
            return rule;
        }
    }
}
