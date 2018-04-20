﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Domain.Season
{
    public partial class SeasonDivision
    {
        public override List<Division> GetDivisionsByParent(JodyAppContext db)
        {            
            var divs = db.SeasonDivisions.Where(div => div.Parent.Id == this.Id && this.Season.Id == div.Season.Id);

            return divs.ToList<Division>();

        }

        public override Division GetByName(JodyAppContext db)
        {
            var division = db.SeasonDivisions.Include("Season").Where(d => d.Name.Equals(Name) && d.Season.Id == Season.Id);

            return division.First();
        }


        public override List<Team> GetAllTeamsInDivision(JodyAppContext db)
        {
            List<Team> teams = new List<Team>();
            if (this.Teams != null) teams.AddRange(this.Teams);

            this.GetDivisionsByParent(db).ForEach(div =>
            {
                teams.AddRange(((SeasonDivision)div).GetAllTeamsInDivision(db));
            });

            return teams;
        }

    }
}