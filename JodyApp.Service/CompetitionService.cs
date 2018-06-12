using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JodyApp.Service
{
    public class CompetitionService : BaseService<DomainObject>
    {        


        SeasonService SeasonService { get; set; }
        PlayoffService PlayoffService { get; set; }
        LeagueService LeagueService { get; set; }        
        ConfigDivisionService ConfigDivisionService { get; set; }
        DivisionService DivisionService { get; set; }
        ConfigScheduleRuleService ScheduleRuleService { get; set; }
        ScheduleService ScheduleService { get; set; }

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public CompetitionService(JodyAppContext db):base(db)
        {
            LeagueService = new LeagueService(db);
            SeasonService = new SeasonService(db);
            PlayoffService = new PlayoffService(db);
            ConfigDivisionService = new ConfigDivisionService(db);
            ScheduleRuleService = new ConfigScheduleRuleService(db);
            ScheduleService = new ScheduleService(db);
        }

        public Competition StartNextYear(int leagueId)
        {
            var league = (League)LeagueService.GetById(leagueId);

            if (LeagueService.IsYearDone(league))
            {
                league.CurrentYear++;
                LeagueService.Save();
                return GetNextCompetition(league);
            }
            else
            {
                return null;
            }
        }
        public Competition GetNextCompetition(League league)
        {
            if (LeagueService.IsYearDone(league))
            {
                return null;
            }
            else
            {
                //get all the possible competitions for this league
                var referenceComps = league.GetActiveConfigCompetitions();

                Competition currentComp = null;

                for (int i = 0; i < referenceComps.Count; i++)
                {
                    //does the current year version exist?                
                    //if yes, is it complete?
                    //if yes, move on, we alreayd know at least one is not complete
                    //if no return it
                    //if no return it
                    ConfigCompetition rc = referenceComps[i];

                    switch (rc.Type)
                    {
                        case ConfigCompetition.SEASON:
                            currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;
                        case ConfigCompetition.PLAYOFF:
                            currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;

                    }

                    //if there is no competition for this one, create one and return it
                    if (currentComp == null)
                        return CreateCompetition(rc, league.CurrentYear);
                    //if there is a current competition, and it is not complete, return it
                    else if (!currentComp.Complete)
                        return currentComp;

                }

                return currentComp;
            }
        }



        public Competition CreateCompetition(ConfigCompetition reference, int year)
        {
            switch(reference.Type)
            {
                case ConfigCompetition.SEASON:
                    return SeasonService.CreateNewSeason(reference, year);                    
                case ConfigCompetition.PLAYOFF:
                    return PlayoffService.CreateNewPlayoff(reference, year);                    
            }
            return null;
        }
        
        public List<Game> GetNextGames(Competition competition)
        {
            //need to get next game number
            return competition.GetNextGames(0);
        }

        public List<Game> GetGames(Competition competition)
        {
            return competition.Games;
        }            

        public List<Game> GetGames(Competition competition, string teamName)
        {
            return competition.Games.Where(g => g.HomeTeam.Name.Equals(teamName) || g.AwayTeam.Name.Equals(teamName)).ToList();
        }
        public ListViewModel GetModelForNextGames(int competitionId, string type)
        {
            Competition competition = null;

            if (type == ConfigCompetitionViewModel.SEASON) competition = (Competition)SeasonService.GetById(competitionId);
            else if (type == ConfigCompetitionViewModel.PLAYOFF) competition = (Competition)PlayoffService.GetById(competitionId);

            var items = new List<GameViewModel>();

            GetNextGames(competition).ForEach(g =>
            {
                items.Add(GameToDTO(competition, g));
            });

            return new ListViewModel(items.ToList<BaseViewModel>());
        }

        public ListViewModel GetModelForGames(int competitionId, string type)
        {
            Competition competition = null;

            if (type == ConfigCompetitionViewModel.SEASON) competition = (Competition)SeasonService.GetById(competitionId);
            else if (type == ConfigCompetitionViewModel.PLAYOFF) competition = (Competition)PlayoffService.GetById(competitionId);

            var items = new List<GameViewModel>();

            GetGames(competition).ForEach(g =>
            {
                items.Add(GameToDTO(competition, g));
            });

            return new ListViewModel(items.ToList<BaseViewModel>());
        }        

        public ListViewModel GetModelForGames(int competitionId, string teamName, string type)
        {
            Competition competition = null;

            if (type == ConfigCompetitionViewModel.SEASON) competition = (Competition)SeasonService.GetById(competitionId);
            else if (type == ConfigCompetitionViewModel.PLAYOFF) competition = (Competition)PlayoffService.GetById(competitionId);



            var items = new List<GameViewModel>();

            GetGames(competition, teamName).ForEach(g =>
            {
                items.Add(GameToDTO(competition, g));
            });

            return new ListViewModel(items.ToList<BaseViewModel>());
        }
        public GameViewModel GameToDTO(Competition competition, Game game)
        {
            var model = new GameViewModel(
                game.Id,
                game.HomeTeam == null ? null : game.HomeTeam.Id,
                game.HomeTeam == null ? "None" : game.HomeTeam.Name,
                                game.AwayTeam == null ? null : game.AwayTeam.Id,
                game.AwayTeam == null ? "None" : game.AwayTeam.Name,
                game.HomeScore,
                game.AwayScore,
                competition.Name,
                competition.Year,
                game.Day,
                game.GameNumber, 
                game.Complete);

            return model;
        }

        public void PlayGames(List<Game> games, Competition competition, Random random)
        {
            competition.PlayGames(games, random);
            if (competition is Season)
            {
                SeasonService.SortAllDivisions((Season)competition);
            }
        }

        public ListViewModel GetAllByLeagueId(int leagueId, string type)
        {
            if (type == ConfigCompetitionViewModel.SEASON) return CreateListViewModelFromList(db.Seasons.Where(s => s.League.Id == leagueId).ToList<DomainObject>(), DomainToDTO);
            if (type == ConfigCompetitionViewModel.PLAYOFF) return CreateListViewModelFromList(db.Playoffs.Where(p => p.League.Id == leagueId).ToList<DomainObject>(), DomainToDTO);

            return null;
        }



        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var competition = (Competition)obj;
            string competitionType = (competition is Season) ? "Season":"Playoff";

            return new CompetitionViewModel(obj.Id, competition.League.Id, competition.League.Name, competition.Name, competition.Year, competitionType, competition.Started, competition.Complete, competition.StartingDay);
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
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

            var activeConfigDivisions = ConfigDivisionService.GetDivisions(referenceSeason).Where(cd => cd.IsActive(year)).ToList();
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

            var configRules = ScheduleRuleService.GetScheduleRulesByCompetition(referenceSeason).Where(rule => rule.IsActive(year)).ToList();

            season.Games = new List<Game>();
            ScheduleService.CreateGamesFromRules(configRules, seasonTeams, seasonDivisions, season.Games, 0);
            db.Games.AddRange(season.Games);

            season.SetupStandings();
            db.SaveChanges();

            return season;

        }
    }
}
