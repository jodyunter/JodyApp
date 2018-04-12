using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class TeamService:BaseService
    {        
        DivisionService divisionService = null;

        public TeamService(JodyAppContext context):base(context)
        {            
            this.divisionService = new DivisionService(context);
        }
        public List<Team> GetAllTeams()
        {

            return db.Teams.ToList<Team>();
        }

        public Team GetTeamByName(String name)
        {
            var query = from t in db.Teams where t.Name.Equals(name) select t;

            return query.ToList<Team>().First();
                        

        }

        public List<Team> GetBaseTeams()
        {
            var query = from t in db.Teams select t;

            return query.ToList<Team>();
        }



    }
}
