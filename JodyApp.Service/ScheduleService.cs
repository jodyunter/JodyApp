using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Schedule;
using JodyApp.Domain; 

namespace JodyApp.Service
{
    public class ScheduleService : BaseService
    {
        DivisionService divisionService;        
        TeamService teamService;


        public List<ScheduleRule> GetLeagueRules(League league)
        {
            return ScheduleRule.GetRules(db, league, null);
        }
        public List<ScheduleRule> GetRules(League league, Season season)
        {
            return ScheduleRule.GetRules(db, league, season);
        }
        public ScheduleService(JodyAppContext db) : base(db)
        {
            divisionService = new DivisionService(db);
            teamService = new TeamService(db);
        }

        public List<Division> GetDivisionsByLevel(ScheduleRule rule)
        {
            return divisionService.GetDivisionsByLevel(rule.DivisionLevel, rule.Season);
        }

        public List<Game> CreateGamesFromRules(List<ScheduleRule> rules)
        {
            var games = new List<Game>();
            rules.ForEach(rule =>
            {
                games.AddRange(CreateGamesFromRule(rule));
            });

            return games;
        }

        public List<Game> CreateGamesFromRule(ScheduleRule rule)
        {
            var games = new List<Game>();

            var homeTeams = new List<Team>();
            var awayTeams = new List<Team>();

            if (rule.HomeType == ScheduleRule.BY_DIVISION_LEVEL)
            {
                List<Division> divisions;

                divisions = this.GetDivisionsByLevel(rule);

                divisions.ForEach(d =>
                {
                    homeTeams = new List<Team>();                    

                    homeTeams.AddRange(divisionService.GetAllTeamsInDivision(d));

                    games.AddRange(Scheduler.ScheduleGames(homeTeams.ToArray(), null, rule.PlayHomeAway, rule.Rounds));
                });
            }
            else
            {
                AddTeamsToListFromRule(homeTeams, rule.HomeType, rule.HomeTeam, rule.HomeDivision);
                AddTeamsToListFromRule(awayTeams, rule.AwayType, rule.AwayTeam, rule.AwayDivision);

                games.AddRange(Scheduler.ScheduleGames(homeTeams.ToArray(), awayTeams.ToArray(), rule.PlayHomeAway, rule.Rounds));

            }
            return games;
        }
        
        public void AddTeamsToListFromRule(List<Team> teamList, int ruleType, Team team, Division division)
        {
            switch (ruleType)
            {
                case ScheduleRule.BY_TEAM:
                    teamList.Add(team);
                    break;
                case ScheduleRule.BY_DIVISION:
                    teamList.AddRange(divisionService.GetAllTeamsInDivision(division));
                    break;
                case ScheduleRule.NONE:
                    break;
            }
        }
    }
}
