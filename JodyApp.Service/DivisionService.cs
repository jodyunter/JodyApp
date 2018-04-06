using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Service.DTO;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {            
        }

        //this could cause trouble with seasons
        public List<Division> GetDivisionsByParent(Division parent)
        {
            var divs = db.Divisions.Where(div => div.Parent.Name == parent.Name);
            
            return divs.ToList<Division>();
        }

        public List<Team> GetAllTeamsInDivision(Division division)
        {

            List<Team> teams = new List<Team>();
            teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));                                
            });

            return teams;
        }
        public Division GetByName(String Name)
        {
            var query = from d in db.Divisions where d.Name.Equals(Name) select d;
            
            Division division = null;
            foreach (var d in query)
            {
                division = d;
            }

            return division;
        }

        public Division GetById(int id)
        {
            return null;
        }

        public Division GetById(int? divisionId)
        {
            throw new NotImplementedException();
        }
        
   
    }
}
