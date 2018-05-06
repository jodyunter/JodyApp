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
        public static List<GroupRule> GetByReference(JodyAppContext db, Playoff p)
        {
            return db.GroupRules.Where(sr => sr.Playoff.Id == p.Id).ToList();            
        }
    }
}
