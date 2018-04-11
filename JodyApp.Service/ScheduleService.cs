using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Schedule;
using JodyApp.Domain;
using JodyApp.Domain.Season;

namespace JodyApp.Service
{
    public class ScheduleService : BaseService
    {
        DivisionService divisionService;
        TeamService teamService;

        public ScheduleService(JodyAppContext db) : base(db)
        {
            divisionService = new DivisionService(db);
            teamService = new TeamService(db);
        }

        public List<ScheduleGame> CreateGamesFromRules(List<SeasonScheduleRule> rules)
        {
            var games = new List<ScheduleGame>();
            rules.ForEach(rule =>
            {
                games.AddRange(CreateGamesFromRule(rule));
            });

            return games;
        }


        public List<ScheduleGame> CreateGamesFromRules(List<ScheduleRule> rules)
        {
            var games = new List<ScheduleGame>();
            rules.ForEach(rule =>
            {
                games.AddRange(CreateGamesFromRule(rule));
            });
             
            return games;
        }
        public List<ScheduleGame> CreateGamesFromRule(ScheduleRule rule)
        {
            var games = new List<ScheduleGame>();

            var homeTeams = new List<Team>();
            var awayTeams = new List<Team>();

            AddTeamsToListFromRule(homeTeams, rule.HomeType, rule.HomeTeam, rule.HomeDivision);
            AddTeamsToListFromRule(awayTeams, rule.AwayType, rule.AwayTeam, rule.AwayDivision);

            games.AddRange(Scheduler.ScheduleGames(homeTeams.ToArray(), awayTeams.ToArray(), rule.PlayHomeAway, rule.Rounds));

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
