using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.App
{
    public class AppConstants
    {
        public const string SERVICE_LEAGUE = "League";
        public const string SERVICE_CONFIGTEAM = "ConfigTeam";
        public const string SERVICE_CONFIGDIVISION = "ConfigDivision";
        public const string SERVICE_CONFIGCOMPETITION = "ConfigCompetition";
        public const string SERVICE_CONFIGSCHEDULERULE = "ConfigScheduleRule";
        public const string SERVICE_CONFIGGROUPRULE = "ConfigGroupRule";
        public const string SERVICE_CONFIGGROUP = "ConfigGroup";
        public const string SERVICE_CONFIGSERIESRULE = "ConfigSeriesRule";
        public const string SERVICE_CONFIGSORTINGRULE = "ConfigSortingRule";
        public const string SERVICE_SEASON = "Season";
        public const string SERVICE_STANDINGS = "Standings";
        public const string SERVICE_COMPETITION = "Competition";
        public const string SERVICE_PLAYOFF = "Playoff";
        public const string SERVICE_SERIES = "Series";
        public const string SERVICE_DIVISION = "Division";
        public const string SERVICE_SCHEDULE = "Schedule";

        #region INPUTS
        //general        
        public const string INPUT_ID = "Id";
        public const string INPUT_LEAGUE = "League";
        public const string INPUT_NAME = "Name";
        public const string INPUT_ORDER = "Order";
        public const string INPUT_FIRSTYEAR = "First Year";
        public const string INPUT_LASTYEAR = "Last Year";

        //division        
        public const string INPUT_SHORTNAME = "Short Name";
        public const string INPUT_LEVEL = "Level";     
        public const string INPUT_TEAMNAME = "TEAMNAME_";
        public const string INPUT_TEAMID = "TEAMID_";
        public const string INPUT_LEAGUEID = "LEAGUEID";
        public const string INPUT_LEAGUENAME = "LEAGUENAME";
        public const string INPUT_PARENTID = "PARENTID";
        public const string INPUT_PARENTNAME = "PARENTNAME";
        public const string INPUT_SEASONID = "SEASONID";
        public const string INPUT_SEASONNAME = "SEASONNAME";

        //Schedule Rule
        public const string INPUT_HOMETYPE = "Home Type";
        public const string INPUT_AWAYTYPE = "Away Type";
        public const string INPUT_HOMETEAM = "Home Team";
        public const string INPUT_AWAYTEAM = "Away Team";
        public const string INPUT_HOMEDIVISION = "Home Div";
        public const string INPUT_AWAYDIVISION = "Away Div";
        public const string INPUT_HOMEANDAWAY = "Home&Away";
        public const string INPUT_ROUNDS = "Rounds";
        public const string INPUT_DIVISIONLEVEL = "Div Level";
        public const string INPUT_COMPETITION = "Competition";
        public const string INPUT_REVERSE = "Reverse";

        #endregion
        #region Labels
        public const string LABEL_NONE = "None";
        public const string LABEL_NAME = INPUT_NAME;

        #endregion 

    }
}
