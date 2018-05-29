using System;
using System.Collections.Generic;
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

        public ConfigTeam GetById(int? id)
        {
            if (id == null) return null;

            return db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault();
        }
        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            if (obj == null) return null;

            var team = (ConfigTeam)obj;

            var model = new ConfigTeamViewModel(team.Id, team.Name, team.Skill,
                team.League != null ? team.League.Name : "None",
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
            var league = db.Leagues.Where(l => l.Name == m.League).FirstOrDefault();
            var division = db.ConfigDivisions.Where(d => d.League.Id == league.Id && d.Name == m.Name);

            if (team == null)
            {
                //new entity
                team = new ConfigTeam(m.Name, m.Skill, null, null, m.FirstYear, m.LastYear);
            }
            else
            {
                league.Name = m.Name;
                league.CurrentYear = m.CurrentYear;
                //what about reference comps? Different scren!
            }

            return DomainToDTO(league);
        }
        
    }
}
