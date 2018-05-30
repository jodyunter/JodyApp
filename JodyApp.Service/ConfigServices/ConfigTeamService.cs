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

        public ConfigTeamService(Database.JodyAppContext db) : base(db) { }
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
            return CreateListViewModelFromList(db.ConfigTeams.ToList<DomainObject>(), DomainToDTO);
            
        }
        

        public override BaseViewModel GetModelById(int id)
        {
            return DomainToDTO(db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault());
        }

        public override BaseViewModel Save(BaseViewModel model)
        {

            var m = (ConfigTeamViewModel)model;
            var team = (ConfigTeam)GetById(m.Id);
            var leagueService = new LeagueService(db);
            var configDivisionService = new ConfigDivisionService(db);

            var league = m.League != null ? (League)leagueService.GetById(m.League.Id) : null;
            var division = m.Division != null ? (ConfigDivision)configDivisionService.GetById(m.Division.Id) : null;

            if (team == null)
            {
                //new entity
                team = new ConfigTeam(m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);
                db.ConfigTeams.Add(team);
            }
            else
            {
                
                var newTeam = new ConfigTeam(m.Id, m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);
                db.Entry(team).CurrentValues.SetValues(newTeam);

                team.Division = newTeam.Division;
                team.League = newTeam.League;
            }

            
            db.SaveChanges();

            return DomainToDTO(team);
        }

        public override DomainObject GetById(int? id)
        {
            if (id == null) return null;

            return db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault();
        }

        public ListViewModel GetByLeague(int leagueId)
        {
            return CreateListViewModelFromList(db.ConfigTeams.Where(t => t.League.Id == leagueId).ToList<DomainObject>(), DomainToDTO);
        }

        public ListViewModel GetByDivisionId(int divisionId)
        {
            return CreateListViewModelFromList(db.ConfigTeams.Where(t => t.Division.Id == divisionId).ToList<DomainObject>(), DomainToDTO);

        }

    }
}
