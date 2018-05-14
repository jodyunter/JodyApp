using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;

using JodyApp.Domain.Schedule;

namespace JodyApp.Service
{
    public class SeasonService:BaseService
    {
        public DivisionService divisionService = new DivisionService();  

        public SeasonService(JodyAppContext context) : base(context) { Initialize(context); }

        public SeasonService() : base() { Initialize(null); }
        public override void Initialize(JodyAppContext db)
        {
            divisionService.db = db;
            divisionService.Initialize(db);
        }

        public bool IsSeasonStarted(Season season)
        {
            return season.Started;
        }

        public Season CreateNewSeason(Season referenceSeason, int year)
        {
            var divisionService = new DivisionService(db);
            var scheduleService = new ScheduleService(db);

            Season season = new Season();

            season.League = referenceSeason.League;
            season.Name = referenceSeason.Name;
            season.Year = year;

            Dictionary<string, Division> seasonDivisions = new Dictionary<string, Division>();
            Dictionary<string, Team> seasonTeams = new Dictionary<string, Team>();
            Dictionary<string, ScheduleRule> seasonScheduleRules = new Dictionary<string, ScheduleRule>();

            //loop once to create teams and new season divisions, order means we will not add a parent we haven't created yet
            divisionService.GetByReferenceSeason(referenceSeason).OrderBy(d => d.Level).ToList<Division>().ForEach(division => 
            {
                Division seasonDiv = division.CreateDivisionForSeason(season);
                if (division.Parent != null) seasonDiv.Parent = seasonDivisions[division.Parent.Name];
                seasonDivisions.Add(seasonDiv.Name, seasonDiv);

            });

            //now add new season related teams.
            foreach (Division d in divisionService.GetByReferenceSeason(referenceSeason))
            {
                d.Teams.ForEach(dt => {
                    Team seasonTeam = new Team(dt, seasonDivisions[d.Name]);
                    seasonDivisions[d.Name].Teams.Add(seasonTeam);
                    db.Teams.Add(seasonTeam);
                    seasonTeams.Add(seasonTeam.Name, seasonTeam);
                });
            }

            //loop to process the sorting rules, this requires the divisions be created first
            foreach (Division d in divisionService.GetByReferenceSeason(referenceSeason))
            {
                Division seasonDiv = seasonDivisions[d.Name];
                seasonDiv.SortingRules = new List<SortingRule>();

                d.SortingRules.ForEach(rule =>
                {
                    SortingRule newRule = new SortingRule(seasonDivisions[rule.Division.Name], seasonDivisions[rule.DivisionToGetTeamsFrom.Name], rule);
                    db.SortingRules.Add(newRule);
                });                
                
            }

            foreach (ScheduleRule rule in scheduleService.GetBySeasonReference(referenceSeason).OrderBy(rule => rule.Order)) 
            {
                Division homeDiv = null;
                Division awayDiv = null;
                Team homeTeam = null;
                Team awayTeam = null;

                if (rule.HomeDivision != null) { homeDiv = seasonDivisions[rule.HomeDivision.Name]; }
                if (rule.AwayDivision != null) { awayDiv = seasonDivisions[rule.AwayDivision.Name]; }
                if (rule.AwayTeam != null) { awayTeam = seasonTeams[rule.AwayTeam.Name]; }
                if (rule.HomeTeam != null) { homeTeam = seasonTeams[rule.HomeTeam.Name]; }

                ScheduleRule seasonRule = new ScheduleRule(
                                                    referenceSeason.League,
                                                    season,
                                                    rule.Name,
                                                    rule.HomeType,
                                                    homeTeam,
                                                    homeDiv,
                                                    rule.AwayType,
                                                    awayTeam,
                                                    awayDiv,
                                                    rule.PlayHomeAway,
                                                    rule.Rounds,
                                                    rule.DivisionLevel,
                                                    rule.Order,
                                                    rule.Reverse
                                                    );
                db.ScheduleRules.Add(seasonRule);
                seasonScheduleRules.Add(seasonRule.Name, seasonRule);
                
            }            


            //need to change season rules too
            season.TeamData = seasonTeams.Values.ToList<Team>();

            db.Seasons.Add(season);
            db.Divisions.AddRange(seasonDivisions.Values);            
            db.SaveChanges();

            seasonDivisions.Values.ToList().ForEach(seasonDiv =>
            {
                divisionService.GetAllTeamsInDivision(seasonDiv).ForEach(team => { seasonDiv.SetRank(0, team); });

                db.DivisionRanks.AddRange(seasonDiv.Rankings);
            });

            season.Games = new List<Game>();
            scheduleService.CreateGamesFromRules(season.ScheduleRules, season.Games, 0);
            db.Games.AddRange(season.Games);

            season.SetupStandings();
            db.SaveChanges();

            return season;
            
        }

        public void SortAllDivisions(Season season)
        {
            
            List<Division> divisions = divisionService.GetDivisionsBySeason(season);

            divisions.ForEach(div => { divisionService.SortByDivision(div); });

            db.SaveChanges();

        }

        public Competition GetReferenceByName(League league, string name, DivisionService divisionService)
        {
            return db.Seasons.Where(s => s.League.Id == league.Id && s.Year == 0 && s.Name == name).FirstOrDefault();
        }
     
        public List<Team> GetTeamsInDivisionByRank(Division division)
        {

            var teams = divisionService.GetAllTeamsInDivision(division).ToDictionary(t => t.Name, t => t);

            division.Rankings.Sort();
            int rank = 1;
            division.Rankings.ForEach(r => { teams[r.Team.Name].Stats.Rank = rank; rank++; });

            return teams.Values.ToList();
        }

        public Season GetSeason(League league, string name, int year)
        {
            return db.Seasons.Where(s => s.Name == name && s.Year == year && s.League.Id == league.Id).FirstOrDefault();
        }

        public Season GetById(int id)
        {
            return db.Seasons.Where(s => s.Id == id).FirstOrDefault();
        }

        public List<Season> GetAll(int leagueId)
        {
            return db.Seasons.Where(s => s.League.Id == leagueId).ToList();
        }

        public List<Season> GetAll()
        {
            return db.Seasons.ToList();
        }
    }
}
