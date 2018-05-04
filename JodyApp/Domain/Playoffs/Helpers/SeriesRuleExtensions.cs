using JodyApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class SeriesRule
    {
        public static List<SeriesRule> GetByLeague(JodyAppContext db, League league, Playoff p)
        {
            if (p == null) return db.SeriesRules.Where(sr => sr.League.Id == league.Id && sr.Playoff == null).ToList();
            else return db.SeriesRules.Where(sr => sr.League.Id == league.Id && sr.Playoff.Id == p.Id).ToList();
        }
    }
}
