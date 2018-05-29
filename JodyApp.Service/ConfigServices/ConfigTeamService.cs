using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigTeamService : BaseService
    {        

        public ConfigTeamService() : base() { }


        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            if (obj == null) return null;

            var team = (ConfigTeam)obj;

            var model = new ConfigTeamViewModel(team.Id, team.Name, team.Skill,
                team.League != null ? team.League.Id : null,
                team.League != null ? team.League.Name : "None",
                team.Division != null ? team.Division.Id : null,
            team.Division != null ? team.Division.Name : "None",
            team.FirstYear, team.LastYear);

            return model;

        }

        public override ListViewModel GetAll()
        {
            var items = new List<BaseViewModel>();

            db.ConfigTeams.ToList().ForEach(team =>
            {
                items.Add(DomainToDTO(team));
            });

            var teamList = new ListViewModel(items);

            return teamList;
        }

        public override BaseViewModel GetModelById(int id)
        {
            return DomainToDTO(db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault());
        }

        public override BaseViewModel Save(BaseViewModel model)
        {

            var m = (ConfigTeamViewModel)model;
            var team = GetById(m.Id);
            var leagueService = new LeagueService(db);
            var configDivisionService = new ConfigDivisionService(db);

            var league = m.League != null ? (League)leagueService.GetById(m.League.Id) : null;
            var division = m.Division != null ? (ConfigDivision)configDivisionService.GetById(m.Division.Id) : null;

            if (team == null)
            {
                //new entity
                team = new ConfigTeam(m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);
            }
            else
            {
                team = new ConfigTeam(m.Id, m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);
                //what about reference comps? Different scren!
            }

            return DomainToDTO(league);
        }

        public override DomainObject GetById(int? id)
        {
            if (id == null) return null;

            return db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault();
        }
    }
}
