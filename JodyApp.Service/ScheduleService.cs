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

        //update to get last game number in database for season
        public List<Game> CreateGamesFromRules(List<ConfigScheduleRule> rules, 
                    Dictionary<string, Team> teams, 
                    Dictionary<string, Division> divisions,
                    List<Game> games, int lastGameNumber)
        {                        
            rules.ForEach(rule =>
            {
                lastGameNumber = CreateGamesFromRule(rule, teams, divisions, games, lastGameNumber);
            });

            return games;
        }

        public int CreateGamesFromRule(ConfigScheduleRule rule, 
            Dictionary<string, Team> seasonTeams,
            Dictionary<string, Division> seasonDivisions,
            List<Game> games, int lastGameNumber)
        {        

            var homeTeams = new List<Team>();
            var awayTeams = new List<Team>();

            if (rule.HomeType == ConfigScheduleRule.BY_DIVISION_LEVEL)
            {
                List<Division> divisions;

                divisions = seasonDivisions.Values.ToList().Where(d => d.Level == rule.DivisionLevel).ToList();

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
                AddTeamsToListFromRule(homeTeams, rule.HomeType, seasonTeams[rule.HomeTeam.Name], seasonDivisions[rule.HomeDivision.Name], rule.Reverse);
                AddTeamsToListFromRule(awayTeams, rule.AwayType, seasonTeams[rule.AwayTeam.Name], seasonDivisions[rule.AwayDivision.Name], rule.Reverse);

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
