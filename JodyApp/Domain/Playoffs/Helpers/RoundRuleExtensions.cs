using JodyApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class RoundRule
    {
        public List<RoundRule> GetByLeague(JodyAppContext db, League league, Playoff playoff)
        {
            if (playoff == null) return db.RoundRules.Where(rule => rule.League.Id == league.Id && rule.Playoff == null).ToList<RoundRule>();
            else return db.RoundRules.Where(rule => rule.League.Id == league.Id && rule.Playoff.Id == Playoff.Id).ToList<RoundRule>();

        }
    }
}
