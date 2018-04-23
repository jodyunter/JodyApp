using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Domain.Schedule
{
    public partial class ScheduleRule
    {
        public static List<ScheduleRule> GetRules(JodyAppContext db, League league)
        {
            return db.ScheduleRules.Where(rule => rule.League.Id == league.Id).ToList<ScheduleRule>();
        }
    }
}
