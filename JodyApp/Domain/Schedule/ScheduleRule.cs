using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Schedule
{
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

        public String Name { get; set; }
        //when creating a new season, we need to translate these into the season rules.
        //since this would be done only at the beginning, we can use it to find the parent teams for the current season

        public Season Season { get; set; }
        public ScheduleRule() { }

        public ScheduleRule(ScheduleRule rule) : this(rule.League, rule.Name, rule.HomeType, rule.HomeTeam, rule.HomeDivision,
     rule.AwayType, rule.AwayTeam, rule.AwayDivision,
     rule.PlayHomeAway, rule.Rounds, rule.DivisionLevel)
        {

        }
        public ScheduleRule(League league, String name, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds, int divisionLevel)
        {
            League = league;
            Name = name;
            HomeType = homeType;
            HomeTeam = homeTeam;
            HomeDivision = homeDivision;
            AwayType = awayType;
            AwayTeam = awayTeam;
            AwayDivision = awayDivision;
            PlayHomeAway = playHomeAway;
            Rounds = rounds;
            DivisionLevel = divisionLevel;
        }

        public ScheduleRule(Season season, ScheduleRule rule) : this(rule)
        {
            Season = season;
        }
        public ScheduleRule(League league, Season season, String name, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds, int divisionLevel) : this(league, name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, playHomeAway, rounds, divisionLevel)
        {
            Season = season;
        }

        public static ScheduleRule CreateByDivisionVsSelf(League league, String name, Division division, bool playHomeAway, int rounds)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = division.League

            };
        }
        public static ScheduleRule CreateByDivisionVsDivision(League league, String name, Division homeDivision, Division awayDivision, bool playHomeAway, int rounds)
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
                League = homeDivision.League

            };
        }
        public static ScheduleRule CreateByTeamVsTeam(League league, String name, Team homeTeam, Team awayTeam, bool playHomeAway, int rounds)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_TEAM,
                HomeTeam = homeTeam,
                AwayType = ScheduleRule.BY_TEAM,
                AwayTeam = awayTeam,
                PlayHomeAway = playHomeAway,
                Rounds = rounds, 
                League = league
            };
        }
        public static ScheduleRule CreateByTeamVsDivision(League league, string name, Team team, Division division, bool playHomeAway, int rounds)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_TEAM,
                HomeTeam = team,
                AwayType = ScheduleRule.BY_DIVISION,
                AwayDivision = division,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = league
            };
        }
        public static ScheduleRule CreateByDivisionVsTeam(League league, string name, Division division, Team team, bool playHomeAway, int rounds)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ScheduleRule.BY_TEAM,
                AwayTeam = team,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = league
            };
        }
        public static ScheduleRule CreateByDivisionLevel(League league, string name, int divisionLevel, bool playHomeAway, int rounds)
        {
            return new ScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION_LEVEL,
                DivisionLevel = divisionLevel,
                AwayType = ScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds,
                League = league
            };
        }


    }
}
