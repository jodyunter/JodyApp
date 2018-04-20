using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;
using JodyApp.Database;

namespace JodyApp.Domain.Season
{
    public partial class SeasonScheduleRule
    {

        public override List<Division> GetDivisionsByLevel(JodyAppContext db)
        {            

            var query = db.SeasonDivisions.Include("Season").Where(d => d.Level == this.DivisionLevel && d.Season.Id == this.Season.Id);

            return query.ToList<Division>();
        }

    }
}
