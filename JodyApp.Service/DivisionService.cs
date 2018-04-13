using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Season;
using System.Data.Entity;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {            
        }

        //this could cause trouble with seasons
        virtual public List<Division> GetDivisionsByParent(Division parent)
        {
            var divs = db.Divisions.Where(div => div.Parent.Name == parent.Name);
            
            return divs.ToList<Division>();
        }

        virtual public List<Team> GetAllTeamsInDivision(Division division)
        {

            List<Team> teams = new List<Team>();
            if (division.Teams != null) teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));                                
            });

            return teams;
        }
        virtual public Division GetByName(String Name)
        {            
            var query = from d in db.Divisions where d.Name.Equals(Name) select d;
            
            Division division = null;
            foreach (var d in query)
            {
                division = d;
            }

            return division;
        }   
        
        virtual public List<Division> GetDivisionsByLevel(int level)
        {
            var query = db.Divisions.Where(d => d.Level == level);

            return query.ToList<Division>();

        }

        public List<Division> GetDivisionsByLevel(Division d)
        {
            return GetDivisionsByLevel(d.Level);
        }

        public List<Division> GetDivisionsByParent(SeasonDivision parent)
        {            
            var divs = db.SeasonDivisions.Where(div => div.Parent.Name == parent.Name);

            return divs.ToList<Division>();
        }

        public List<Team> GetAllTeamsInDivision(SeasonDivision division)
        {            
            List<Team> teams = new List<Team>();
            if (division.Teams != null) teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));
            });

            return teams;
        }
        public Division GetByName(String name, Season season)
        {
            var division = db.SeasonDivisions.Include("Season").Where(d => d.Name.Equals(name) && d.Season.Id == season.Id);

            return division.First();
        }

        public List<Division> GetDivisionsByLevel(int level, Season season)
        {
            var query = db.SeasonDivisions.Include("Season").Where(d => d.Level == level && d.Season.Id == season.Id);

            return query.ToList<Division>();
        }

        public List<Division> GetDivisionsByLevel(SeasonDivision division)
        {
            return GetDivisionsByLevel(division.Level, division.Season);
        }

    }
}

