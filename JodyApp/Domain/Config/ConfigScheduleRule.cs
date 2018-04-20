using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain.Config
{    
    public class ConfigScheduleRule : ScheduleRule
    {        
        public ConfigScheduleRule() { }
        public ConfigScheduleRule(ScheduleRule rule):base(rule)
        {

        }
        public ConfigScheduleRule(string name, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds, int divisionLevel) : base(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, playHomeAway, rounds, divisionLevel)
        {
        }

        public static ScheduleRule CreateByDivisionVsSelf(String name, Division division, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds

            };
        }
        public static ScheduleRule CreateByDivisionVsDivision(String name, Division homeDivision, Division awayDivision, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = homeDivision,
                AwayType = ScheduleRule.BY_DIVISION,
                AwayDivision = awayDivision,
                PlayHomeAway = playHomeAway,
                Rounds = rounds

            };
        }
        public static ScheduleRule CreateByTeamVsTeam(String name, Team homeTeam, Team awayTeam, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_TEAM,
                HomeTeam = homeTeam,
                AwayType = ScheduleRule.BY_TEAM,
                AwayTeam = awayTeam,
                PlayHomeAway = playHomeAway,
                Rounds = rounds
            };
        }
        public static ScheduleRule CreateByTeamVsDivision(string name, Team team, Division division, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_TEAM,
                HomeTeam = team,
                AwayType = ScheduleRule.BY_DIVISION,
                AwayDivision = division,
                PlayHomeAway = playHomeAway,
                Rounds = rounds
            };
        }
        public static ScheduleRule CreateByDivisionVsTeam(string name, Division division, Team team, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION,
                HomeDivision = division,
                AwayType = ScheduleRule.BY_TEAM,
                AwayTeam = team,
                PlayHomeAway = playHomeAway,
                Rounds = rounds
            };
        }
        public static ScheduleRule CreateByDivisionLevel(string name, int divisionLevel, bool playHomeAway, int rounds)
        {
            return new ConfigScheduleRule()
            {
                Name = name,
                HomeType = ScheduleRule.BY_DIVISION_LEVEL,
                DivisionLevel = divisionLevel,
                AwayType = ScheduleRule.NONE,
                PlayHomeAway = playHomeAway,
                Rounds = rounds
            };
        }

    }
}
