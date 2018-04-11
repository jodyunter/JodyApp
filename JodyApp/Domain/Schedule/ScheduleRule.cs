using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Schedule
{
    public class ScheduleRule:DomainObject
    {
        //rules with opponents implied
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

        //when creating a new season, we need to translate these into the season rules.
        //since this would be done only at the beginning, we can use it to find the parent teams for the current season

        public ScheduleRule() { }
        public ScheduleRule(int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway)
        {
            HomeType = homeType;
            HomeTeam = homeTeam;
            HomeDivision = homeDivision;
            AwayType = awayType;
            AwayTeam = awayTeam;
            AwayDivision = awayDivision;
            PlayHomeAway = playHomeAway;
        }
    }
}
