using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service
{
    public class SeasonService:BaseService
    {
        TeamService teamService;
        DivisionService divisionService;
        ScheduleService scheduleService;

        public SeasonService(JodyAppContext context):base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);
            scheduleService = new ScheduleService(context);
        }

        public Season CreateNewSeason(League league, string name, int year)
        {
            Season season = new Season();

            season.League = league;
            season.Name = name;
            season.Year = year;

            Dictionary<string, SeasonDivision> seasonDivisions = new Dictionary<string, SeasonDivision>();
            Dictionary<string, SeasonTeam> seasonTeams = new Dictionary<string, SeasonTeam>();

            //loop once to create teams and new season divisions
            foreach (Division d in divisionService.GetByLeague(league))
            {
                //in the event the parent is added in the recursive steps, we don't want to do it agian
                if (!seasonDivisions.ContainsKey(d.Name))
                {
                    seasonDivisions.Add(d.Name, CreateSeasonDivision(season, d, seasonDivisions));
                }
              
            }

            foreach (Division d in divisionService.GetByLeague(league))
            {
                d.Teams.ForEach(dt => {
                    SeasonTeam seasonTeam = new SeasonTeam(dt, seasonDivisions[d.Name]);
                    seasonDivisions[d.Name].Teams.Add(seasonTeam);
                    db.SeasonTeams.Add(seasonTeam);
                    seasonTeams.Add(seasonTeam.Name, seasonTeam);
                });
            }

            //lop to process the sorting rules, this requires the divisions be created first
            foreach (Division d in divisionService.GetByLeague(league))
            {
                SeasonDivision seasonDiv = seasonDivisions[d.Name];
                seasonDiv.SortingRules = new List<SortingRule>();

                d.SortingRules.ForEach(rule =>
                {
                    SortingRule newRule = new SortingRule(seasonDivisions[rule.Division.Name], seasonDivisions[rule.DivisionToGetTeamsFrom.Name], rule);
                    db.SortingRules.Add(newRule);
                });                
                
            }

            foreach(ScheduleRule rule in scheduleService.GetConfigRules())
            {
                SeasonDivision homeDiv = null;
                SeasonDivision awayDiv = null;
                SeasonTeam homeTeam = null;
                SeasonTeam awayTeam = null;

                if (rule.HomeDivision != null) { homeDiv = seasonDivisions[db.Divisions.Find(rule.HomeDivision.Id).Name]; }
                if (rule.AwayDivision != null) { awayDiv = seasonDivisions[db.Divisions.Find(rule.AwayDivision.Id).Name]; }
                if (rule.AwayTeam != null) { awayTeam = seasonTeams[db.Teams.Find(rule.AwayTeam.Id).Name]; }
                if (rule.HomeTeam != null) { homeTeam = seasonTeams[db.Teams.Find(rule.HomeTeam.Id).Name]; }

                SeasonScheduleRule seasonRule = new SeasonScheduleRule(
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
                                                    rule.DivisionLevel
                                                    );
                db.SeasonScheduleRules.Add(seasonRule);
                
            }            


            //need to change season rules too
            season.TeamData = seasonTeams.Values.ToList<SeasonTeam>();

            db.Seasons.Add(season);
            db.SeasonDivisions.AddRange(seasonDivisions.Values);            
            db.SaveChanges();

            seasonDivisions.Values.ToList().ForEach(seasonDiv =>
            {
                seasonDiv.GetAllTeamsInDivision(db).ForEach(team => { seasonDiv.SetRank(0, (RecordTableTeam)team); });

                db.DivisionRanks.AddRange(seasonDiv.Rankings);
            });

            db.SaveChanges();

            return season;
            
        }
        private SeasonDivision CreateSeasonDivision(Season season, Division d, Dictionary<string, SeasonDivision> seasonDivisions)
        {
            SeasonDivision division = new SeasonDivision(d, season); 
            if (d.Parent != null)
            {
                //if the parent isn't there add it
                if (!seasonDivisions.ContainsKey(d.Parent.Name))
                {
                    SeasonDivision parent = CreateSeasonDivision(season, d.Parent, seasonDivisions);
                    seasonDivisions.Add(parent.Name, parent);
                }

                division.Parent = seasonDivisions[d.Parent.Name];
            }

            return division;
        }
        

        public void SortAllDivisions(Season season)
        {
            List<SeasonDivision> divisions = divisionService.GetDivisionsBySeason(season);

            divisions.ForEach(div => { divisionService.SortByDivision(div); });

        }
     
        public List<SeasonTeam> GetSeasonTeamsInDivisionByRank(SeasonDivision division)
        {

            var teams = division.GetAllTeamsInDivision(db).ToDictionary(t => t.Name, t => t);

            division.Rankings.Sort();
            int rank = 1;
            division.Rankings.ForEach(r => { teams[r.Team.Name].Stats.Rank = rank; rank++; });

            return teams.Values.ToList();
        }
    }
}
