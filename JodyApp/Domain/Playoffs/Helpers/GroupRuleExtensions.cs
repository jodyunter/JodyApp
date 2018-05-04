using JodyApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class GroupRule
    {
        public static List<GroupRule> GetByLeague(JodyAppContext db, League league, Playoff p)
        {
            if (p == null) return db.GroupRules.Where(sr => sr.League.Id == league.Id && sr.Playoff == null).ToList();
            else return db.GroupRules.Where(sr => sr.League.Id == league.Id && sr.Playoff.Id == p.Id).ToList();
        }
    }
}
