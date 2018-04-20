using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Domain.Season
{
    public partial class SeasonDivision
    {
        public List<SeasonTeam> Teams { get; set; }

        public List<SeasonDivision> GetDivisionsByParent(JodyAppContext db)
        {            
            var divs = db.SeasonDivisions.Where(div => div.Parent.Id == this.Id && this.Season.Id == div.Season.Id && this.League.Id == div.League.Id);

            return divs.ToList<SeasonDivision>();

        }

        public SeasonDivision GetByName(JodyAppContext db)
        {
            return db.SeasonDivisions.Include("Season").Where(d => d.Name.Equals(Name) && d.Season.Id == Season.Id && this.League.Id == d.League.Id).First();

            
        }

        public List<SeasonTeam> GetAllTeamsInDivision(JodyAppContext db)
        {

            List<SeasonTeam> teams = new List<SeasonTeam>();
            if (this.Teams != null) teams.AddRange(this.Teams);

            this.GetDivisionsByParent(db).ForEach(div =>
            {
                teams.AddRange(((SeasonDivision)div).GetAllTeamsInDivision(db));
            });

            return teams;


        }


    public List<SeasonDivision> GetByLeague(JodyAppContext db)
        {
            return db.SeasonDivisions.Where(d => d.League.Id == this.League.Id && d.Season.Id == this.Season.Id).ToList<SeasonDivision>();
        }

    }
}
