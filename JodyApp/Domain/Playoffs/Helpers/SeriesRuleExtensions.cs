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
        public static List<SeriesRule> GetByReference(JodyAppContext db, Playoff p)
        {
            return db.SeriesRules.Where(sr => sr.Playoff.Id == p.Id).ToList();            
        }
    }
}
