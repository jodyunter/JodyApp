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

            Dictionary<string, Division> seasonDivisions = new Dictionary<string, Division>();
            Dictionary<string, Team> seasonTeams = new Dictionary<string, Team>();
            Dictionary<string, ScheduleRule> seasonScheduleRules = new Dictionary<string, ScheduleRule>();

            //loop once to create teams and new season divisions, order means we will not add a parent we haven't created yet
            divisionService.GetByLeague(league).OrderBy(d => d.Level).ToList<Division>().ForEach(division => 
            {
                Division seasonDiv = division.CreateDivisionForSeason(season);
                if (division.Parent != null) seasonDiv.Parent = seasonDivisions[division.Parent.Name];
                seasonDivisions.Add(seasonDiv.Name, seasonDiv);

            });

            //now add new season related teams.
            foreach (Division d in divisionService.GetByLeague(league))
            {
                d.Teams.ForEach(dt => {
                    Team seasonTeam = new Team(dt, seasonDivisions[d.Name]);
                    seasonDivisions[d.Name].Teams.Add(seasonTeam);
                    db.Teams.Add(seasonTeam);
                    seasonTeams.Add(seasonTeam.Name, seasonTeam);
                });
            }

            //loop to process the sorting rules, this requires the divisions be created first
            foreach (Division d in divisionService.GetByLeague(league))
            {
                Division seasonDiv = seasonDivisions[d.Name];
                seasonDiv.SortingRules = new List<SortingRule>();

                d.SortingRules.ForEach(rule =>
                {
                    SortingRule newRule = new SortingRule(seasonDivisions[rule.Division.Name], seasonDivisions[rule.DivisionToGetTeamsFrom.Name], rule);
                    db.SortingRules.Add(newRule);
                });                
                
            }

            foreach(ScheduleRule rule in scheduleService.GetLeagueRules(league))
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
                                                    league,
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
                                                    rule.Order
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

            db.SaveChanges();

            return season;
            
        }

        public void SortAllDivisions(Season season)
        {
            List<Division> divisions = divisionService.GetDivisionsBySeason(season);

            divisions.ForEach(div => { divisionService.SortByDivision(div); });

        }
     
        public List<Team> GetTeamsInDivisionByRank(Division division)
        {

            var teams = divisionService.GetAllTeamsInDivision(division).ToDictionary(t => t.Name, t => t);

            division.Rankings.Sort();
            int rank = 1;
            division.Rankings.ForEach(r => { teams[r.Team.Name].Stats.Rank = rank; rank++; });

            return teams.Values.ToList();
        }
    }
}
