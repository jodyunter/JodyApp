using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigTeamService : BaseService<ConfigTeam>
    {
        public override DbSet<ConfigTeam> Entities => db.ConfigTeams;

        public ConfigTeamService(Database.JodyAppContext db) : base(db) { }
        //public ConfigTeamService() : base() { }


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
                team = CreateTeam(m.Name, m.Skill, division, league, m.FirstYear, m.LastYear);                
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
        public ListViewModel GetModelByLeague(int leagueId)
        {
            return CreateListViewModelFromList(db.ConfigTeams.Where(t => t.League.Id == leagueId).ToList<DomainObject>(), DomainToDTO);
        }

        public ListViewModel GetModelByDivision(int divisionId)
        {
            return CreateListViewModelFromList(GetByDivision(divisionId).ToList<DomainObject>(), DomainToDTO);

        }
        public List<ConfigTeam> GetByDivision(int divisionId)
        {
            return db.ConfigTeams.Where(t => t.Division.Id == divisionId).ToList();
        }
         
        public void SetNewSkills(League league, Random random)
        {
            int[] chanceToIncrease = new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0 };

            GetByLeagueAndYear(league, league.CurrentYear).ForEach(team =>
            {
                int max = 200;
                int chgNum = 100;
                int num = random.Next(0, max);
                if (num <= chgNum)
                {
                    if (num <= chanceToIncrease[team.Skill]) team.Skill++;
                    else team.Skill--;
                }

                if (team.Skill > 10) team.Skill = 10;
                if (team.Skill < 0) team.Skill = 0;
            });
        }

        public List<ConfigTeam> GetByLeagueAndYear(League league, int currentYear)
        {
            return db.ConfigTeams.Where(team =>
            team.League.Id == league.Id &&
            team.FirstYear != null &&
            team.FirstYear <= currentYear &&
            (team.LastYear == null || team.LastYear >= currentYear)
            ).ToList();
        }

        public ConfigTeam CreateTeam(string name, int skill, ConfigDivision division, League league, int? firstYear, int? lastYear)
        {
            var newTeam = new ConfigTeam(name, skill, division, league, firstYear, lastYear);
            db.ConfigTeams.Add(newTeam);
            return newTeam;
        }

        public ConfigTeam GetByName(string name)
        {
            return db.ConfigTeams.Where(t => t.Name == name).FirstOrDefault();
        }        
    }
}
