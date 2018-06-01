using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;

using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public class SeasonService:BaseService
    {
        public ConfigService ConfigService { get; set; }
        public DivisionService DivisionService { get; set; }
        
        public ScheduleService ScheduleService { get; set; }

        public SeasonService(JodyAppContext db, ConfigService configService, DivisionService divisionService, ScheduleService scheduleService):base(db)
        {
            ConfigService = configService;
            DivisionService = divisionService;
            ScheduleService = scheduleService;
        }

        public SeasonService(JodyAppContext db, LeagueService leagueService, ConfigService configService) : base(db)
        {
            ConfigService = configService;
            DivisionService = new DivisionService(db);
            ScheduleService = new ScheduleService(db, DivisionService);
        }

        public SeasonService(JodyAppContext db) : base(db)
        {            
            ConfigService = new ConfigService(db);
            DivisionService = new DivisionService(db);
            ScheduleService = new ScheduleService(db, DivisionService);
        }


        public SeasonService() : base() { }

        public bool IsSeasonStarted(Season season)
        {
            return season.Started;
        }

        public Season CreateNewSeason(ConfigCompetition referenceSeason, int year)
        {
            
            Season season = new Season();

            season.League = referenceSeason.League;
            season.Name = referenceSeason.Name;
            season.Year = year;

            Dictionary<string, Division> seasonDivisions = new Dictionary<string, Division>();
            Dictionary<string, Team> seasonTeams = new Dictionary<string, Team>();
            Dictionary<string, ConfigScheduleRule> seasonScheduleRules = new Dictionary<string, ConfigScheduleRule>();

            var activeConfigDivisions = ConfigService.GetDivisions(referenceSeason).Where(cd => cd.IsActive(year)).ToList();
            //loop once to create teams and new season divisions, order means we will not add a parent we haven't created yet
            activeConfigDivisions.OrderBy(d => d.Level).ToList().ForEach(configDivision =>            
            {
                Division seasonDiv = season.CreateDivisionForSeason(configDivision);
                if (configDivision.Parent != null) seasonDiv.Parent = seasonDivisions[configDivision.Parent.Name];
                seasonDivisions.Add(seasonDiv.Name, seasonDiv);

            });

            activeConfigDivisions.ForEach(configDivision =>
                configDivision.Teams.Where(t => t.IsActive(year)).ToList().ForEach(dt =>
                {
                    Team seasonTeam = new Team(dt, seasonDivisions[configDivision.Name]);
                    seasonDivisions[configDivision.Name].Teams.Add(seasonTeam);
                    db.Teams.Add(seasonTeam);
                    seasonTeams.Add(seasonTeam.Name, seasonTeam);
                })
            );
            


            //need to change season rules too
            season.TeamData = seasonTeams.Values.ToList();

            db.Seasons.Add(season);
            db.Divisions.AddRange(seasonDivisions.Values);            
            db.SaveChanges();

            seasonDivisions.Values.ToList().ForEach(seasonDiv =>
            {
                DivisionService.GetAllTeamsInDivision(seasonDiv).ForEach(team => { seasonDiv.SetRank(0, team); });

                db.DivisionRanks.AddRange(seasonDiv.Rankings);
            });

            var configRules = ConfigService.GetScheduleRulesByCompetition(referenceSeason).Where(rule => rule.IsActive(year)).ToList();

            season.Games = new List<Game>();
            ScheduleService.CreateGamesFromRules(configRules, seasonTeams, seasonDivisions, season.Games, 0);
            db.Games.AddRange(season.Games);

            season.SetupStandings();
            db.SaveChanges();

            return season;
            
        }

        public void SortAllDivisions(Season season)
        {
            
            List<Division> divisions = DivisionService.GetDivisionsBySeason(season);

            divisions.ForEach(div => { DivisionService.SortByDivision(div); });

            db.SaveChanges();

        }

        public List<Team> GetTeamsInDivisionByRank(Division division)
        {

            var teams = DivisionService.GetAllTeamsInDivision(division).ToDictionary(t => t.Name, t => t);

            division.Rankings.Sort();
            int rank = 1;
            division.Rankings.ForEach(r => { teams[r.Team.Name].Stats.Rank = rank; rank++; });

            return teams.Values.ToList();
        }

        public Season GetSeason(League league, string name, int year)
        {
            return db.Seasons.Where(s => s.Name == name && s.Year == year && s.League.Id == league.Id).FirstOrDefault();
        }

        public ListViewModel GetAllByLeagueId(int leagueId)
        {

            return CreateListViewModelFromList(db.Seasons.Where(s => s.League.Id == leagueId).ToList<DomainObject>(), DomainToDTO);
        }

        public SeasonViewModel DomainToDTO(Season season)
        {
            if (season == null) return null;
            return new SeasonViewModel(season.Id, season.League.Id, season.League.Name, season.Name, season.Year, "Season", season.Complete, season.Started, season.StartingDay);
        }


        public override BaseViewModel GetModelById(int id)
        {
            return DomainToDTO(db.Seasons.Where(s => s.Id == id).FirstOrDefault());
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var season = (Season)obj;
            return new SeasonViewModel(season.Id, season.League.Id, season.League.Name, season.Name, season.Year, "Season", season.Started, season.Complete, season.StartingDay);
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }
        public override ListViewModel GetAll()
        {

            return CreateListViewModelFromList(db.Seasons.ToList<DomainObject>(), DomainToDTO);
        }

        public override DomainObject GetById(int? id)
        {
            return db.Seasons.Where(s => s.Id == id).FirstOrDefault();
        }
    }
}
