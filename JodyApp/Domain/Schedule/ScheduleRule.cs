using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;

namespace JodyApp.Domain.Schedule
{
    public abstract partial class ScheduleRule:DomainObject
    {
        //rules with opponents implied
        public const int BY_DIVISION_LEVEL = 2;  //use this to first sort all teams by division level, then they all play games against each other
        public const int BY_DIVISION = 0; //get teams in specific division         
        public const int BY_TEAM = 1; //get specific team
        public const int NONE = -1; //use this to ignore the away team
        
        public int HomeType { get; set; } //By Division, By Team              
        virtual public Team HomeTeam { get; set; }        
        virtual public Division HomeDivision { get; set; }

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

        public ScheduleRule() { }

        public ScheduleRule(ScheduleRule rule) : this(rule.Name, rule.HomeType, rule.HomeTeam, rule.HomeDivision,
     rule.AwayType, rule.AwayTeam, rule.AwayDivision,
     rule.PlayHomeAway, rule.Rounds, rule.DivisionLevel)
        {

        }
        public ScheduleRule(String name, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds, int divisionLevel)
        {
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

    }
}
