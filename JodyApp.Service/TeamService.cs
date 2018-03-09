using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Service.DataFolder;

namespace JodyApp.Service
{
    public class TeamService
    {
        DataService dataService = DataService.Instance;

        public List<Team> GetAllTeams()
        {
            return dataService.GetAllTeams();
        }

        public RecordTable GetStandings()
        {
            return dataService.GetStandings();
        }

    }
}
