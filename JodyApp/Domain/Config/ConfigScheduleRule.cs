using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Config
{
    //schedule rules should also incorporate when each set of game are played.
    //setup groups of rules so we know what happens.
    //group 1: Play a round of division games
    //group 2: Play a round of home and home vs all teams
    //group 3: Play a play off series
    //group 4: Setup new division based on series winners and play more games
    //group 5: Setup final round of playoff series and games
    public class ConfigScheduleRule:DomainObject, BaseConfigItem
    {
        //rules with opponents implied
        public const int BY_DIVISION_LEVEL = 2;  //use this to first sort all teams by division level, then they all play games against each other
        public const int BY_DIVISION = 0; //get teams in specific division         
        public const int BY_TEAM = 1; //get specific team
        public const int NONE = -1; //use this to ignore the away team
        
        public int HomeType { get; set; } //By Division, By Team              
        virtual public ConfigTeam HomeTeam { get; set; }
        virtual public ConfigDivision HomeDivision { get; set; }

        //should probably remove this
        virtual public League League { get; set; }
        virtual public int AwayType { get; set; }
        virtual public ConfigTeam AwayTeam { get; set; }
        virtual public ConfigDivision AwayDivision { get; set; }

        public Boolean PlayHomeAway { get; set; } //if home and away teams are the same we need special rules

        public int Rounds { get; set; }

        //used in conjunction with BY_DIVISION_LEVEL
        public int DivisionLevel { get; set; }

        public int Order { get; set; }

        public String Name { get; set; }
        //when creating a new season, we need to translate these into the season rules.
        //since this would be done only at the beginning, we can use it to find the parent teams for the current season

        public ConfigCompetition Season { get; set; }

        public bool Reverse { get; set; } //reverse default order

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }
        public ConfigScheduleRule() { }

        public ConfigScheduleRule(ConfigScheduleRule rule) : this(rule.League, rule.Season, rule.Name, rule.HomeType, rule.HomeTeam, rule.HomeDivision,
     rule.AwayType, rule.AwayTeam, rule.AwayDivision,
     rule.PlayHomeAway, rule.Rounds, rule.DivisionLevel, rule.Order, rule.Reverse)
        {

        }

        public ConfigScheduleRule(ConfigCompetition season, ConfigScheduleRule rule) : this(rule)
        {
            Season = season;
        }
        public ConfigScheduleRule(League league, ConfigCompetition season, String name, int homeType, ConfigTeam homeTeam, ConfigDivision homeDivision,
                int awayType, ConfigTeam awayTeam, ConfigDivision awayDivision, bool playHomeAway, int rounds, int divisionLevel, int order, bool reverse)
        {
            this.League = league;
            this.Season = season;
            this.Name = name;
            this.HomeType = homeType;
            this.HomeTeam = homeTeam;
            this.HomeDivision = homeDivision;
            this.AwayType = awayType;
            this.AwayTeam = awayTeam;
            this.AwayDivision = awayDivision;
            this.PlayHomeAway = playHomeAway;
            this.Rounds = rounds;
            this.DivisionLevel = divisionLevel;
            this.Order = order;
            this.Reverse = reverse;
        }

        public static ConfigScheduleRule CreateByDivisionVsSelf(League league, ConfigCompetition season, String name, ConfigDivision division, bool playHomeAway, int rounds, int order, bool reverse)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ConfigScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ConfigScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = league,
                Order = order,
                Season = season,
                Reverse = reverse

            };
        }
        public static ConfigScheduleRule CreateByDivisionVsDivision(League league, ConfigCompetition season, String name, ConfigDivision homeDivision, ConfigDivision awayDivision, bool playHomeAway, int rounds, int order, bool reverse)
        {
        return new ConfigScheduleRule()
        {
            Name = name,
            HomeType = ConfigScheduleRule.BY_DIVISION,
            HomeDivision = homeDivision,
            AwayType = ConfigScheduleRule.BY_DIVISION,
            AwayDivision = awayDivision,
            PlayHomeAway = playHomeAway,
            Rounds = rounds,
            League = league,
            Season = season,
            Reverse = reverse
            };
        }
        public static ConfigScheduleRule CreateByTeamVsTeam(League league, ConfigCompetition season, String name, ConfigTeam homeTeam, ConfigTeam awayTeam, bool playHomeAway, int rounds, int order, bool reverse)
        {
            return new ConfigScheduleRule(league,
                season,
                name,
                BY_TEAM,
                homeTeam,
                null,
                BY_TEAM,
                awayTeam,
                null,
                playHomeAway,
                rounds,
                -1,
                order,
                reverse);

        }
        public static ConfigScheduleRule CreateByTeamVsDivision(League league, ConfigCompetition season, string name, ConfigTeam team, ConfigDivision division, bool playHomeAway, int rounds, int order, bool reverse)
        {
            return new ConfigScheduleRule(
                league,
                season,
                name,
                BY_TEAM,
                team,
                null,
                BY_DIVISION,
                null,
                division,
                playHomeAway,
                rounds,
                -1,
                order,
                reverse
                );
        }
        public static ConfigScheduleRule CreateByDivisionVsTeam(League league, ConfigCompetition season, string name, ConfigDivision division, ConfigTeam team, bool playHomeAway, int rounds, int order, bool reverse)
        {
            return new ConfigScheduleRule(
                league,
                season,
                name,
                BY_DIVISION,
                null,
                division,
                BY_TEAM,
                team,
                null,
                playHomeAway,
                rounds,
                -1,
                order, 
                reverse);
        }
        public static ConfigScheduleRule CreateByDivisionLevel(League league, ConfigCompetition season, string name, int divisionLevel, bool playHomeAway, int rounds, int order, bool reverse)
        {
            return new ConfigScheduleRule(
                league,
                season, 
                name,
                BY_DIVISION_LEVEL,
                null,
                null,
                NONE,
                null,
                null,
                playHomeAway,
                rounds,
                divisionLevel,
                order,
                reverse);
        }

        public override bool Equals(object obj)
        {
            var rule = obj as ConfigScheduleRule;
            return rule != null &&
                   HomeType == rule.HomeType &&
                   EqualityComparer<ConfigTeam>.Default.Equals(HomeTeam, rule.HomeTeam) &&
                   EqualityComparer<ConfigDivision>.Default.Equals(HomeDivision, rule.HomeDivision) &&
                   EqualityComparer<League>.Default.Equals(League, rule.League) &&
                   AwayType == rule.AwayType &&
                   EqualityComparer<ConfigTeam>.Default.Equals(AwayTeam, rule.AwayTeam) &&
                   EqualityComparer<ConfigDivision>.Default.Equals(AwayDivision, rule.AwayDivision) &&
                   PlayHomeAway == rule.PlayHomeAway &&
                   Rounds == rule.Rounds &&
                   DivisionLevel == rule.DivisionLevel &&
                   Order == rule.Order &&
                   Name == rule.Name &&
                   EqualityComparer<ConfigCompetition>.Default.Equals(Season, rule.Season) &&
                   Reverse == rule.Reverse &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, rule.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, rule.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = -435751921;
            hashCode = hashCode * -1521134295 + HomeType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigTeam>.Default.GetHashCode(HomeTeam);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(HomeDivision);
            hashCode = hashCode * -1521134295 + EqualityComparer<League>.Default.GetHashCode(League);
            hashCode = hashCode * -1521134295 + AwayType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigTeam>.Default.GetHashCode(AwayTeam);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(AwayDivision);
            hashCode = hashCode * -1521134295 + PlayHomeAway.GetHashCode();
            hashCode = hashCode * -1521134295 + Rounds.GetHashCode();
            hashCode = hashCode * -1521134295 + DivisionLevel.GetHashCode();
            hashCode = hashCode * -1521134295 + Order.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigCompetition>.Default.GetHashCode(Season);
            hashCode = hashCode * -1521134295 + Reverse.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
