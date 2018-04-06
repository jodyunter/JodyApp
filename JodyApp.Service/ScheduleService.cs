using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class ScheduleService : BaseService
    {
        DivisionService divisionService;
        TeamService teamService;

        public ScheduleService(JodyAppContext db) : base(db)
        {
            divisionService = new DivisionService(db);
            teamService = new TeamService(db);
        }
    }
}
