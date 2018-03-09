using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Service.DTO
{
    public class TeamDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int? Skill { get; set; }

        public static Team ToDomain(TeamDTO teamDTO, DivisionService service)
        {
            Team team = new Team
            {
                Id = teamDTO.Id,
                Name = teamDTO.Name,
                Division = teamDTO.DivisionId == null ? null : service.GetById(teamDTO.DivisionId),
                Skill = teamDTO.Skill == null ? 0 : (int)teamDTO.Skill
            };

            return team;

        }

        public static TeamDTO ToDTO(Team team)
        {
            TeamDTO teamDTO = new TeamDTO
            {
                Id = team.Id,
                Name = team.Name,
                Skill = team.Skill,
                DivisionId = team.Division == null ? null : team.Division.Id,
                DivisionName = team.Division == null ? null : team.Division.Name
            };

            return teamDTO;

        }
    }
}
