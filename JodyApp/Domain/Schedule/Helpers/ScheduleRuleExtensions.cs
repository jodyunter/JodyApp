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
        public static List<ScheduleRule> GetRules(JodyAppContext db, League league, Season season)
        {
            if (season == null) return db.ScheduleRules.Where(rule => rule.League.Id == league.Id && rule.Season == null).ToList<ScheduleRule>();
            else return db.ScheduleRules.Where(rule => rule.League.Id == league.Id && rule.Season.Id == season.Id).ToList<ScheduleRule>();
        }
    }
}
