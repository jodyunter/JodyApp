using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Service.DataFolder;
using JodyApp.Service.DTO;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class TeamService
    {
        JodyAppContext db = new JodyAppContext();
        DataService dataService = DataService.Instance;
        DivisionService divisionService = new DivisionService();

        public List<TeamDTO> GetAllTeams()
        {
            var teamList = from t in db.Teams orderby t.Name select t;
            
            List<TeamDTO> teamDTOList = new List<TeamDTO>();
            foreach (var team in teamList) 
            {
                teamDTOList.Add(TeamDTO.ToDTO(team));
            }

            return teamDTOList;
        }

        public RecordTable GetStandings()
        {
            return dataService.GetStandings();
        }

        public TeamDTO GetTeamByName(String name)
        {
            return null;
        }

        public void Save(TeamDTO teamDTO)
        {
            Team team = TeamDTO.ToDomain(teamDTO, divisionService);

            dataService.Save(team);

        }

    }
}
