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
            return db.Divisions.Where(div => div.Parent.Id == this.Id && div.League.Id == this.League.Id).ToList<Division>();            
        }

        virtual public Division GetByName(JodyAppContext db)
        {

            return db.Divisions.Where(d => (d.Name == this.Name) && (d.League.Id == this.League.Id)).First();
            
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
        
        virtual public List<Division> GetByLeague(JodyAppContext db)
        {
            return db.Divisions.Where(d => d.League.Id == this.League.Id).ToList<Division>();
        }
    }

}

