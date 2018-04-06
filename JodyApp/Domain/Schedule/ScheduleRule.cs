using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class ScheduleRule
    {
        //rules with opponents implied
        public static int BY_DIVISION = 0; //get teams in specific division 
        public static int BY_LEVEL = 1; //get teams in specific level
        public static int BY_TEAM = 2; //get specific team
        public static int NONE = -1; //use this to ignore the away team
        
        public int HomeType { get; set; } //By Division, By Team
        public int HomeName { get; set; } //Division Number -- when loading data will need to use names        
        public int AwayType { get; set; }
        public int AwayName { get; set; }
        public Boolean PlayHomeAway { get; set; } //if home and away teams are the same we need special rules

        //when creating a new season, we need to translate these into the season rules.
        //since this would be done only at the beginning, we can use it to find the parent teams for the current season
        
    }
}
