using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Schedule
{
    //schedule rules should also incorporate when each set of game are played.
    //setup groups of rules so we know what happens.
    //group 1: Play a round of division games
    //group 2: Play a round of home and home vs all teams
    //group 3: Play a play off series
    //group 4: Setup new division based on series winners and play more games
    //group 5: Setup final round of playoff series and games
    public partial class ScheduleRule:DomainObject
    {
        //rules with opponents implied
        public const int BY_DIVISION_LEVEL = 2;  //use this to first sort all teams by division level, then they all play games against each other
        public const int BY_DIVISION = 0; //get teams in specific division         
        public const int BY_TEAM = 1; //get specific team
        public const int NONE = -1; //use this to ignore the away team
        
        public int HomeType { get; set; } //By Division, By Team              
        virtual public Team HomeTeam { get; set; }        
        virtual public Division HomeDivision { get; set; }

        virtual public League League { get; set; }
        public int AwayType { get; set; }                     
        virtual public Team AwayTeam { get; set; }        
        virtual public Division AwayDivision { get; set; }

        public Boolean PlayHomeAway { get; set; } //if home and away teams are the same we need special rules

        public int Rounds { get; set; }

        //used in conjunction with BY_DIVISION_LEVEL
        public int DivisionLevel { get; set; }

        public int Order { get; set; }

        public String Name { get; set; }
        //when creating a new season, we need to translate these into the season rules.
        //since this would be done only at the beginning, we can use it to find the parent teams for the current season

        public Season Season { get; set; }
        public ScheduleRule() { }

        public ScheduleRule(ScheduleRule rule) : this(rule.League, rule.Season, rule.Name, rule.HomeType, rule.HomeTeam, rule.HomeDivision,
     rule.AwayType, rule.AwayTeam, rule.AwayDivision,
     rule.PlayHomeAway, rule.Rounds, rule.DivisionLevel, rule.Order)
        {

        }

        public ScheduleRule(Season season, ScheduleRule rule) : this(rule)
        {
            Season = season;
        }
        public ScheduleRule(League league, Season season, String name, int homeType, Team homeTeam, Division homeDivision,
                int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds, int divisionLevel, int order)
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
        }

        public static ScheduleRule CreateByDivisionVsSelf(League league, Season season, String name, Division division, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = division.League,
                Order = order,
                Season = season

            };
        }
        public static ScheduleRule CreateByDivisionVsDivision(League league, Season season, String name, Division homeDivision, Division awayDivision, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = homeDivision,
                AwayType = ScheduleRule.BY_DIVISION,
                AwayDivision = awayDivision,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = homeDivision.League,
                Season = season

            };
        }
        public static ScheduleRule CreateByTeamVsTeam(League league, Season season, String name, Team homeTeam, Team awayTeam, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule(league,
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
                order);

        }
        public static ScheduleRule CreateByTeamVsDivision(League league, Season season, string name, Team team, Division division, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule(
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
                order
                );
        }
        public static ScheduleRule CreateByDivisionVsTeam(League league, Season season, string name, Division division, Team team, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule(
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
                order);
        }
        public static ScheduleRule CreateByDivisionLevel(League league, Season season, string name, int divisionLevel, bool playHomeAway, int rounds, int order)
        {
            return new ScheduleRule(
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
                order);
        }


    }
}
