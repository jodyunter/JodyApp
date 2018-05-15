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
        DivisionService divisionService = new DivisionService();
        ConfigService configService = new ConfigService();

        public override void Initialize(JodyAppContext db)
        {
            divisionService.db = db; divisionService.Initialize(db);
            configService.db = db; configService.Initialize(db);
        }

        public TeamService(JodyAppContext context) : base(context) { Initialize(context); }

        public Team GetTeamByName(String name)
        {
            return Team.GetByName(db, name, null);
                        

        }

        public List<Team> GetTeamsBySeason(Season season)
        {
            return Team.GetTeams(db, season);
        }


        public Team GetByName(string name)
        {
            return db.Teams.Where(t => t.Parent == null && t.Name == name).FirstOrDefault();
        }

        //this will allow us to create a team from any 
        public Team CreateTeam(ConfigTeam configTeam, Division div)
        {
            var team = new Team(configTeam, div);
            db.Teams.Add(team);
            return team;
        }
    }
}
