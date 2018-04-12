using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain.Config
{
    [Table("ScheduleRules")]
    public class BaseScheduleRule:ScheduleRule
    {
        public BaseScheduleRule() { }
        public BaseScheduleRule(String name, int homeType, Team homeTeam, Division homeDivision, int awayType, Team awayTeam, Division awayDivision, bool playHomeAway, int rounds)
            : base(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, playHomeAway, rounds) { }
    }
}
