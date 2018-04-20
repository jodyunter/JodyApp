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

        public virtual List<Division> GetDivisionsByLevel(JodyAppContext db)
        {
            return GetDivisionsByLevel(this.DivisionLevel, db);
        }

        virtual public List<Division> GetDivisionsByLevel(int level, JodyAppContext db)
        {
            var query = db.Divisions.Where(d => d.Level == level);

            return query.ToList<Division>();

        }

    }
}
