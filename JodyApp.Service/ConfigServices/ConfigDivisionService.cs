using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigDivisionService : BaseService<ConfigDivision>
    {
        public override DbSet<ConfigDivision> Entities => db.ConfigDivisions;

        public ConfigDivisionService(JodyAppContext db) : base(db) {  }

        //public ConfigDivisionService():base()
        //{
        //}

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var division = (ConfigDivision)obj;

            var model = new ConfigDivisionViewModel(division.Id, 
                division.League != null ? division.League.Id : null,
                division.League != null ? division.League.Name : "None",
                division.Competition != null ? division.Competition.Id : null,
                division.Competition != null ? division.Competition.Name : "None",
                division.Name, division.ShortName,
                division.Parent != null ? division.Parent.Id : null,
                division.Parent != null ? division.Parent.Name : "None",
                division.Level, division.Order, new List<ReferenceObject>(), division.FirstYear, division.LastYear);

            var configTeamService = new ConfigTeamService(db);

            division.Teams.ForEach(team =>
            {
                model.Teams.Add(new ReferenceObject(team.Id, team.Name));
            });

            return model;

        }

        //todo: Add teams to save and competitions
        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
        //public override BaseViewModel Save(BaseViewModel model)
        //{
        //    var m  = (ConfigDivisionViewModel)model;

        //    var division = (ConfigDivision)GetById(m.Id);
        //    var leagueService = new LeagueService(db);
        //    var configDivisionService = new ConfigDivisionService(db);
        //    var configTeamService = new ConfigTeamService(db);

        //    var league = m.League != null ? (League)leagueService.GetById(m.League.Id) : null;
        //    var parent = m.Parent != null ? (ConfigDivision)configDivisionService.GetById(m.Parent.Id) : null;

        //    var teamList = configTeamService.GetByDivision((int)division.Id);
        //    teamList.ForEach(team => team.Division = null);

        //    if (division == null)
        //    {
        //        //new entity
        //        //division = new ConfigDivision(league, 
        //        db.ConfigTeams.Add(team);
        //    }
        //    else
        //    {

        //        var newTeam = new ConfigTeam(m.Id, m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);
        //        db.Entry(team).CurrentValues.SetValues(newTeam);

        //        team.Division = newTeam.Division;
        //        team.League = newTeam.League;
        //    }

        //    db.SaveChanges();

        //    return DomainToDTO(team);
        //}

        public ListViewModel GetByLeagueId(int leagueId)
        {
            var items = new List<BaseViewModel>();

            db.ConfigDivisions.Where(t => t.League.Id == leagueId).ToList().ForEach(d =>
           {
               items.Add(DomainToDTO(d));
           });

            return new ListViewModel(items);
        }

        public List<ConfigDivision> GetDivisions(ConfigCompetition season)
        {
            return db.ConfigDivisions.Where(division => division.Competition.Id == season.Id).ToList();
        }
        
        public ConfigDivision CreateDivision(League league, ConfigCompetition competition, string name,
                                            string shortName, int level, int order, ConfigDivision parent,
                                            int? firstYear, int? lastYear)
        {
            var division = new ConfigDivision(league, competition, name, shortName, level, order, parent, firstYear, lastYear);
            db.ConfigDivisions.Add(division);

            return division;
        }

        public ConfigDivision GetDivisionByName(League league, string name)
        {
            return db.ConfigDivisions.Where(d => d.League.Id == league.Id && d.Name == name).FirstOrDefault();
        }

        public ConfigDivision GetDivisionById(int id)
        {
            return db.ConfigDivisions.Where(d => d.Id == id).FirstOrDefault();
        }
        

    }
}
