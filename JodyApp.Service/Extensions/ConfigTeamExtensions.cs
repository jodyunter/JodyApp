using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class ConfigService
    {
        //probably need to somehow seperate these into config vs non config
        public ConfigTeamListViewModel GetAllTeams()
        {
            var viewModel = new ConfigTeamListViewModel(new List<ConfigTeamViewModel>());

            db.ConfigTeams.ToList().ForEach(t =>
            {
                viewModel.Items.Add(DomainToDTO(t));
            });

            return viewModel;
        }

        public ConfigTeamViewModel GetTeamModelById(int id)
        {
            return DomainToDTO(GetTeamById(id));
        }

        public ConfigTeamViewModel DomainToDTO(ConfigTeam team)
        {
            if (team == null) return null;
            return new ConfigTeamViewModel(team.Id, team.Name, team.Skill,
                team.League != null ? team.League.Name : "None",
                team.Division != null ? team.Division.Name : "None");
        }

        public ConfigTeamListViewModel GetTeamsByDivisionName(string name)
        {

            var viewModel = new ConfigTeamListViewModel(new List<ConfigTeamViewModel>());

            db.ConfigTeams.Where(t => t.Division.Name == name).ToList().ForEach(t =>
            {
                viewModel.Items.Add(DomainToDTO(t));
            });

            return viewModel;
        }
    }
}
