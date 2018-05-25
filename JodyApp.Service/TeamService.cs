using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public class TeamService : BaseService
    {        

        public TeamService(JodyAppContext instance):base(instance)
        {
            
        }

        public override void Initialize(JodyAppContext db)
        {
            
        }

        public Team CreateTeam(ConfigTeam team, Division division)
        {
            var newTeam = new Team(team, division);
            db.Teams.Add(newTeam);

            return newTeam;
        }

        //probably need to somehow seperate these into config vs non config
        public TeamListViewModel GetAll()
        {
            var viewModel = new TeamListViewModel(new List<TeamViewModel>());

           db.ConfigTeams.ToList().ForEach(t =>
           {
               viewModel.Items.Add(DomainToDTO(t));
           });

            return viewModel;
        }

        public TeamViewModel GetById(int id)
        {
            return DomainToDTO(db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault());
        }
        public TeamViewModel DomainToDTO(ConfigTeam team )
        {
            if (team == null) return null;
            return new TeamViewModel(team.Id, team.Name, team.Skill, 
                team.League != null ? team.League.Name : "None",
                team.Division != null ? team.Division.Name : "None");
        }

        public TeamListViewModel GetByDivisionName(string name)
        {
            
            var viewModel = new TeamListViewModel(new List<TeamViewModel>());            
            
            db.ConfigTeams.Where(t => t.Division.Name == name).ToList().ForEach(t =>
            {
                viewModel.Items.Add(DomainToDTO(t));
            });

            return viewModel;
        }
    }
}
