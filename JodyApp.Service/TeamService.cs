using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Service.DTO;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class TeamService
    {
        JodyAppContext db = new JodyAppContext();        
        DivisionService divisionService = new DivisionService();

        public List<Team> GetAllTeams()
        {

            return db.Teams.ToList<Team>();
        }

        public Team GetTeamByName(String name)
        {
            var query = from t in db.Teams where t.Name.Equals(name) select t;

            return query.ToList<Team>().First();
                        

        }



    }
}
