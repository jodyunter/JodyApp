using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;

namespace JodyApp.Domain
{

    public abstract partial class Division
    {
        virtual public List<Division> GetDivisionsByParent(JodyAppContext db)
        {
            var divs = db.Divisions.Where(div => div.Parent.Id == this.Id);

            return divs.ToList<Division>();
        }

        virtual public Division GetByName(JodyAppContext db)
        {
            var query = from d in db.Divisions where d.Name.Equals(Name) select d;

            return query.First();
        }

        virtual public List<Team> GetAllTeamsInDivision(JodyAppContext db)
        {

            List<Team> teams = new List<Team>();
            if (this.Teams != null) teams.AddRange(this.Teams);

            this.GetDivisionsByParent(db).ForEach(div =>
            {
                teams.AddRange(div.GetAllTeamsInDivision(db));
            });

            return teams;
        }
    }

}

