using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain; 

namespace JodyApp.Service
{
    public class ScheduleService : BaseService
    {
        DivisionService divisionService = new DivisionService();

        public ScheduleService() : base() { Initialize(null);  }
        public ScheduleService(JodyAppContext db) : base(db) { Initialize(db); }

        public override void Initialize(JodyAppContext db) { divisionService.db = db; divisionService.Initialize(db); }
        public List<ConfigScheduleRule> GetBySeasonReference(Season season)
        {
            return GetRules(season.League, season);
        }
        public List<ConfigScheduleRule> GetRules(League league, Season season)
        {
            return ConfigScheduleRule.GetRules(db, league, season);
        }

        public List<Division> GetDivisionsByLevel(ConfigScheduleRule rule)
        {
            return divisionService.GetDivisionsByLevel(rule.DivisionLevel, rule.Season);
        }

        //update to get last game number in database for season
        public List<Game> CreateGamesFromRules(List<ConfigScheduleRule> rules, List<Game> games, int lastGameNumber)
        {            
            rules.ForEach(rule =>
            {
                lastGameNumber = CreateGamesFromRule(rule, games, lastGameNumber);
            });

            return games;
        }

        public int CreateGamesFromRule(ConfigScheduleRule rule, List<Game> games, int lastGameNumber)
        {        

            var homeTeams = new List<Team>();
            var awayTeams = new List<Team>();

            if (rule.HomeType == ConfigScheduleRule.BY_DIVISION_LEVEL)
            {
                List<Division> divisions;

                divisions = this.GetDivisionsByLevel(rule);

                divisions.ForEach(d =>
                {
                    homeTeams = new List<Team>();

                    var list = divisionService.GetAllTeamsInDivision(d);
                    if (rule.Reverse) list.Reverse();
                    homeTeams.AddRange(list);

                    lastGameNumber = Scheduler.ScheduleGames(games, lastGameNumber, homeTeams.ToArray(), null, rule.PlayHomeAway, rule.Rounds);
                });
            }
            else
            {
                AddTeamsToListFromRule(homeTeams, rule.HomeType, rule.HomeTeam, rule.HomeDivision, rule.Reverse);
                AddTeamsToListFromRule(awayTeams, rule.AwayType, rule.AwayTeam, rule.AwayDivision, rule.Reverse);

                lastGameNumber = Scheduler.ScheduleGames(games, lastGameNumber, homeTeams.ToArray(), awayTeams.ToArray(), rule.PlayHomeAway, rule.Rounds);

            }
            return lastGameNumber;
        }
        
        public void AddTeamsToListFromRule(List<Team> teamList, int ruleType, Team team, Division division, bool reverse)
        {
            switch (ruleType)
            {
                case ConfigScheduleRule.BY_TEAM:
                    teamList.Add(team);
                    break;
                case ConfigScheduleRule.BY_DIVISION:
                    var list = divisionService.GetAllTeamsInDivision(division);
                    if (reverse) list.Reverse();
                    teamList.AddRange(list);
                    break;
                case ConfigScheduleRule.NONE:
                    break;
            }
        }
    }
}
