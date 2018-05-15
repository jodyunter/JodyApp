using JodyApp.Domain.Playoffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigGroupRule:DomainObject, BaseConfigItem
    {

        virtual public ConfigGroup Group { get; set; }
        public string Name { get; set; }
        public int RuleType { get; set; }
        //example of different sorty and from
        //we sort by league
        //but we take DivA, 1,2,3 and DivB, 4,5 and ConferenceA, 1
        //no rounds because we might want DivA 1 to go directly to the final where as the winners of other places will be put in other groups        
        virtual public ConfigDivision FromDivision { get; set; }
        public String FromSeries { get; set; } //this means that all series need to be created at playoff creation time
        public int FromStartValue { get; set; } //ranking or WINNER/LOSER, or start = 1 or start = 2, or start at bottom and use end value to go backwards
        public int FromEndValue { get; set; } //division rankings 1, 10        
        virtual public ConfigTeam FromTeam { get; set; }

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigGroupRule() { }
        public ConfigGroupRule(ConfigGroup group, string name, int ruleType, ConfigDivision fromDivision, string fromSeries, int fromStartValue, int fromEndValue, ConfigTeam fromTeam, int? firstYear, int? lastYear)
        {
            Group = group;
            Name = name;
            RuleType = ruleType;
            FromDivision = fromDivision;
            FromSeries = fromSeries;
            FromStartValue = fromStartValue;
            FromEndValue = fromEndValue;
            FromTeam = fromTeam;
        }



        #region Static Creates
        public static ConfigGroupRule CreateFromDivision(ConfigGroup g, string name, ConfigDivision fromDivision, int highestRank, int lowestRank, int? firstYear, int? lastYear)
        {
            var rule = new ConfigGroupRule(g, name, GroupRule.FROM_DIVISION, fromDivision, null, highestRank, lowestRank, null, firstYear, lastYear);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static ConfigGroupRule CreateFromDivisionBottom(ConfigGroup g, string name, ConfigDivision fromDivision, int highestRank, int lowestRank, int? firstYear, int? lastYear)
        {
            var rule = new ConfigGroupRule(g, name, GroupRule.FROM_DIVISION_BOTTOM, fromDivision, null, highestRank, lowestRank, null, firstYear, lastYear);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static ConfigGroupRule CreateFromSeriesWinner(ConfigGroup g, string name, string seriesName, int? firstYear, int? lastYear)
        {
            var rule = new ConfigGroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_WINNER, GroupRule.SERIES_WINNER, null, firstYear, lastYear);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static ConfigGroupRule CreateFromSeriesLoser(ConfigGroup g, string name, string seriesName, int? firstYear, int? lastYear)
        {
            var rule = new ConfigGroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_LOSER, GroupRule.SERIES_LOSER, null, firstYear, lastYear);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static ConfigGroupRule CreateFromTeam(ConfigGroup g, string name, ConfigTeam team, int? firstYear, int? lastYear)
        {
            var rule = new ConfigGroupRule(g, name, GroupRule.FROM_TEAM, null, null, 1, 1, team, firstYear, lastYear);
            g.GroupRules.Add(rule);
            return rule;
        }
        #endregion
    }
}
