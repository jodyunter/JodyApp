using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class TeamService : BaseService
    {
        public override void Initialize(JodyAppContext db)
        {
            
        }

        public Team CreateTeam(ConfigTeam team, Division division)
        {
            var newTeam = new Team(team, division);
            db.Teams.Add(newTeam);

            return newTeam;
        }
    }
}
