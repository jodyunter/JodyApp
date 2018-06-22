using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using JodyApp.ViewModel;
using System.Data.Entity;

namespace JodyApp.Service.CompetitionServices
{
    public class ScheduleService : BaseService<DomainObject>
    {
        DivisionService DivisionService { get; set; }          

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public ScheduleService(JodyAppContext db) : base(db)
        {
            DivisionService = new DivisionService(db);            
        }
       

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

                    var list = DivisionService.GetAllTeamsInDivision(d);
                    if (rule.Reverse) list.Reverse();
                    homeTeams.AddRange(list);

                    lastGameNumber = Scheduler.ScheduleGames(games, lastGameNumber, homeTeams.ToArray(), null, rule.PlayHomeAway, rule.Rounds);
                });
            }
            else
            {
                var homeTeam = rule.HomeTeam == null ? null : seasonTeams[rule.HomeTeam.Name];
                var homeDivision = rule.HomeDivision == null ? null : seasonDivisions[rule.HomeDivision.Name];
                var awayTeam = rule.AwayTeam == null ? null : seasonTeams[rule.AwayTeam.Name];
                var awayDivision = rule.AwayDivision == null ? null : seasonDivisions[rule.AwayDivision.Name];

                AddTeamsToListFromRule(homeTeams, rule.HomeType, homeTeam, homeDivision, rule.Reverse);
                AddTeamsToListFromRule(awayTeams, rule.AwayType, awayTeam, awayDivision, rule.Reverse);

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
                    var list = DivisionService.GetAllTeamsInDivision(division);
                    if (reverse) list.Reverse();
                    teamList.AddRange(list);
                    break;
                case ConfigScheduleRule.NONE:
                    break;
            }
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

    }
}
