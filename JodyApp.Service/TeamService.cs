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
            return GetStandings(GetAllTeams());
        }

        public RecordTable GetStandings(List<Team> teams)
        {
            RecordTable table = new RecordTable();

            teams.ForEach(team =>
            {
                table.Standings.Add(team.Name, new RecordTableTeam(team));
            });

            return table;
        }

    }
}
