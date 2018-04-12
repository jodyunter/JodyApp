using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain.Season
{
    [Table("ScheduleRules")]
    public class SeasonScheduleRule : ScheduleRule
    {
        virtual public Season Season { get; set; }

        public SeasonScheduleRule() {}

        public SeasonScheduleRule(Season season, String name, int homeType, SeasonTeam homeTeam, SeasonDivision homeDivision, int awayType, SeasonTeam awayTeam, SeasonDivision awayDivision, bool playHomeAway, int rounds) : base(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, playHomeAway, rounds)
        {
            Season = season;
        }
    }
}
