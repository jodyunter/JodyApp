using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain.Season
{
    public class SeasonScheduleRule : ScheduleRule
    {
        virtual public Season Season { get; set; }

        public SeasonScheduleRule() {}

        public SeasonScheduleRule(Season season)
        {
            Season = season;
        }

        public SeasonScheduleRule(Season season, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway) : base(homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, playHomeAway)
        {
            Season = season;
        }
    }
}
