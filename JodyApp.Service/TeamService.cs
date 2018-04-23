using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class TeamService:BaseService
    {        
        DivisionService divisionService = null;

        public TeamService(JodyAppContext context):base(context)
        {            
            this.divisionService = new DivisionService(context);
        }
        public List<Team> GetBaseTeams()
        {

            return Team.GetTeams(db, null);
        }

        public Team GetTeamByName(String name)
        {
            return Team.GetByName(db, name, null);
                        

        }

        public List<Team> GetTeamsBySeason(Season season)
        {
            return Team.GetTeams(db, season);
        }




    }
}
