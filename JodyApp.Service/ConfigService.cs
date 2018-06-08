using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public class ConfigService : BaseService
    {
        LeagueService LeagueService { get; set; }
       

        public ConfigService(JodyAppContext db, LeagueService leagueService) : base(db)
        {
            LeagueService = leagueService;
        }

        public ConfigService(JodyAppContext db) : base(db)
        {
            LeagueService = new LeagueService(db);
        }

        public List<ConfigDivision> GetDivisions(ConfigCompetition season)
        {
            return db.ConfigDivisions.Where(division => division.Competition.Id == season.Id).ToList();
        }

        public List<ConfigGroup> GetGroups(ConfigCompetition playoff)
        {
            return db.ConfigGroups.Where(group => group.Playoff.Id == playoff.Id).ToList();
        }

        public List<ConfigSeriesRule> GetSeriesRules(ConfigCompetition playoff)
        {
            return db.ConfigSeriesRules.Where(series => series.Playoff.Id == playoff.Id).ToList();
        }

        public List<BaseConfigItem> GetActiveItems(List<BaseConfigItem> items, int currentYear)
        {
            return items.Where(i => i.FirstYear <= currentYear && (i.LastYear == null || i.LastYear >= currentYear)).ToList();
        }
        #region competition
        public ConfigCompetition CreateCompetition(League league, string name, int type, ConfigCompetition reference, int order, int? firstYear, int? lastYear)
        {
            var newComp = new ConfigCompetition(null, league, name, type, reference, order, firstYear, lastYear);
            db.ConfigCompetitions.Add(newComp);

            return newComp;
        }

        public ConfigCompetition GetCompetitionByName(League league, string name)
        {
            return db.ConfigCompetitions.Where(c => c.League.Id == league.Id && c.Name == name).FirstOrDefault();
        }

        public ConfigCompetition GetCompetitionById(int id)
        {
            return db.ConfigCompetitions.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<ConfigCompetition> GetCompetitionsByLeague(League league, int year)
        {
            return db.ConfigCompetitions.Where(c =>
                    c.League.Id == league.Id &&
                    c.FirstYear != null && c.FirstYear <= year &&
                    (c.LastYear == null || c.LastYear >= year)).ToList();
        }
        #endregion
        #region Teams  
        public void SetNewSkills(League league, Random random)
        {
            int[] chanceToIncrease = new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0 };            

            GetTeams(league, league.CurrentYear).ForEach(team =>
            {
                int max = 200;
                int chgNum = 100;
                int num = random.Next(0, max);
                if (num <= chgNum)
                {
                    if (num <= chanceToIncrease[team.Skill]) team.Skill++;
                    else team.Skill--;                                            
                }                

                if (team.Skill > 10) team.Skill = 10;
                if (team.Skill < 0) team.Skill = 0;
            });
        }

        public void ChangeDivision(ConfigTeamViewModel team, int leagueId, string newDivisionName)
        {
            var league = (League)LeagueService.GetById(leagueId);
            var configTeam = GetTeamById((int)team.Id);
            
            var division = GetDivisionByName(league, newDivisionName);

            configTeam.Division = division;
            
        }
        public void ChangeDivision(ConfigTeam team, string newDivisionName)
        {
            //not good enough for division            
            team.Division = db.ConfigDivisions.Where(d => d.Name == newDivisionName).FirstOrDefault();            
        }

        public List<ConfigTeam> GetTeams(League league, int currentYear)
        {
            return db.ConfigTeams.Where(team =>
            team.League.Id == league.Id &&
            team.FirstYear != null &&
            team.FirstYear <= currentYear &&
            (team.LastYear == null || team.LastYear >= currentYear)
            ).ToList();
        }

        public ConfigTeam CreateTeam(string name, int skill, ConfigDivision division, League league, int? firstYear, int? lastYear)
        {
            var newTeam = new ConfigTeam(name, skill, division, league, firstYear, lastYear);
            db.ConfigTeams.Add(newTeam);
            return newTeam;
        }

        public ConfigTeam GetTeamById(int id)
        {
            return db.ConfigTeams.Where(t => t.Id == id).FirstOrDefault();
        }
        public ConfigTeam GetTeamByName(string name)
        {
            return db.ConfigTeams.Where(t => t.Name == name).FirstOrDefault();
        }
        #endregion
        #region Divisions
        public ConfigDivision CreateDivision(League league, ConfigCompetition competition, string name,
                                            string shortName, int level, int order, ConfigDivision parent,
                                            int? firstYear, int? lastYear)
        {
            var division = new ConfigDivision(league, competition, name, shortName, level, order, parent, firstYear, lastYear);
            db.ConfigDivisions.Add(division);

            return division;
        }

        public ConfigDivision GetDivisionByName(League league, string name)
        {
            return db.ConfigDivisions.Where(d => d.League.Id == league.Id && d.Name == name).FirstOrDefault();
        }

        public ConfigDivision GetDivisionById(int id)
        {
            return db.ConfigDivisions.Where(d => d.Id == id).FirstOrDefault();
        }
        #endregion
        #region Schedule Rules

        public List<ConfigScheduleRule> GetScheduleRulesByCompetition(ConfigCompetition competition)
        {
            return db.ConfigScheduleRules.Where(sr => sr.Competition.Id == competition.Id).ToList();
        }
        public ConfigScheduleRule GetScheduleRuleById(int id)
        {
            return db.ConfigScheduleRules.Where(sr => sr.Id == id).FirstOrDefault();
        }

        public ConfigScheduleRule GetScheduleRuleByName(League league, ConfigCompetition competition, string name)
        {
            return db.ConfigScheduleRules.Where(sr => sr.Name == name && sr.League.Id == league.Id && sr.Competition.Id == competition.Id).FirstOrDefault();
        }
        public ConfigScheduleRule CreateScheduleRuleByDivisionLevel(League league, ConfigCompetition competition, string name, int divisionLevel,
                                            bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByDivisionLevel(league, competition, name, divisionLevel, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;
        }
        public ConfigScheduleRule CreateScheduleRuleByDivisionVsDivision(League league, ConfigCompetition competition, string name, 
                                    ConfigDivision homeDivision, ConfigDivision awayDivision,
                                    bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByDivisionVsDivision(league, competition, name, homeDivision, awayDivision, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;            
        }
        public ConfigScheduleRule CreateScheduleRuleByDivisionVsSelf(League league, ConfigCompetition competition, string name,
                            ConfigDivision division, bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByDivisionVsSelf(league, competition, name, division, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;
        }
        public ConfigScheduleRule CreateScheduleRuleByDivisionVsTeam(League league, ConfigCompetition competition, string name,
                    ConfigDivision division, ConfigTeam team, bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByDivisionVsTeam(league, competition, name, division, team, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;            
        }
        public ConfigScheduleRule CreateScheduleRuleByTeamVsDivision(League league, ConfigCompetition competition, string name,
                             ConfigTeam team, ConfigDivision division, bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByTeamVsDivision(league, competition, name, team, division, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;                        
        }
        public ConfigScheduleRule CreateScheduleRuleByTeamVsTeam(League league, ConfigCompetition competition, string name,
                        ConfigTeam homeTeam, ConfigTeam awayTeam, bool playHomeAway, int rounds, int order, bool reverse, int? firstYear, int? lastYear)
        {
            var rule = ConfigScheduleRule.CreateByTeamVsTeam(league, competition, name, homeTeam, awayTeam, playHomeAway, rounds, order, reverse, firstYear, lastYear);
            db.ConfigScheduleRules.Add(rule);

            return rule;         
        }
        #endregion
        #region Sorting Rules
        public ConfigSortingRule CreateSortingRule(string name, int groupNumber, ConfigDivision division, ConfigDivision divisionToGetTeamsFrom,
                                    string positionsToUse, int divisionLevel, int type, int? firstYear, int? lastYear)
        {
            var rule = new ConfigSortingRule(name, groupNumber, division, divisionToGetTeamsFrom, positionsToUse, divisionLevel, type, firstYear, lastYear);
            db.ConfigSortingRules.Add(rule);
            return rule;
        }      
        
        public ConfigSortingRule GetSortingRuleById(int id)
        {
            return db.ConfigSortingRules.Where(r => r.Id == id).FirstOrDefault();
        }

        #endregion
        #region Groups
        public ConfigGroup CreateGroup(string name, ConfigCompetition playoff, List<ConfigGroupRule> groupRules, 
                            ConfigDivision sortByDivision, int? firstYear, int? lastYear)
        {
            var group = new ConfigGroup(name, playoff, groupRules, sortByDivision, firstYear, lastYear);
            db.ConfigGroups.Add(group);
            return group;

        }

        public ConfigGroup GetGroupById(int id)
        {
            return db.ConfigGroups.Where(g => g.Id == id).FirstOrDefault();
        }
        #endregion
        #region Group Rules
        public ConfigGroupRule CreateGroupRuleFromDivision(ConfigGroup group, string name, ConfigDivision fromDivision, int highestRank, int lowestRank, int? firstYear, int? lastYear)
        {
            var groupRule = ConfigGroupRule.CreateFromDivision(group, name, fromDivision, highestRank, lowestRank, firstYear, lastYear);
            db.ConfigGroupRules.Add(groupRule);
            return groupRule;
        }
        public ConfigGroupRule CreateGroupRuleFromDivisionBottom(ConfigGroup group, string name, ConfigDivision fromDivision, int highestRank, int lowestRank, int? firstYear, int? lastYear)
        {
            var groupRule = ConfigGroupRule.CreateFromDivisionBottom(group, name, fromDivision, highestRank, lowestRank, firstYear, lastYear);
            db.ConfigGroupRules.Add(groupRule);
            return groupRule;
        }
        public ConfigGroupRule CreateGroupRuleFromSeriesLoser(ConfigGroup group, string name, string seriesName, int? firstYear, int? lastYear)
        {
            var groupRule = ConfigGroupRule.CreateFromSeriesLoser(group, name, seriesName, firstYear, lastYear);
            db.ConfigGroupRules.Add(groupRule);
            return groupRule;            
        }
        public ConfigGroupRule CreateGroupRuleFromSeriesWinner(ConfigGroup group, string name, string seriesName, int? firstYear, int? lastYear)
        {
            var groupRule = ConfigGroupRule.CreateFromSeriesWinner(group, name, seriesName, firstYear, lastYear);
            db.ConfigGroupRules.Add(groupRule);
            return groupRule;
            //ConfigGroupRule.CreateFromTeam();
        }
        public ConfigGroupRule CreateGroupRuleFromTeam(ConfigGroup group, string name, ConfigTeam team, int? firstYear, int? lastYear)
        {
            var groupRule = ConfigGroupRule.CreateFromTeam(group, name, team, firstYear, lastYear);
            db.ConfigGroupRules.Add(groupRule);
            return groupRule;            
        }

        public ConfigGroupRule GetGroupRuleById(int id)
        {
            return db.ConfigGroupRules.Where(gr => gr.Id == id).FirstOrDefault();
        }
        #endregion
        #region Series Rules
        public ConfigSeriesRule CreateSeriesRule(ConfigCompetition playoff, string name, int round,
                                ConfigGroup homeTeamFromGroup, int homeTeamFromRank,
                                ConfigGroup awayTeamFromGroup, int awayTeamFromRank,
                                int seriesType, int gamesNeeded, bool canTie, string homeGames, int? firstYear, int? lastYear)
        {
            var rule = new ConfigSeriesRule(playoff, name, round, homeTeamFromGroup, homeTeamFromRank,
                                    awayTeamFromGroup, awayTeamFromRank, seriesType, gamesNeeded,
                                    canTie, homeGames, firstYear, lastYear);

            db.ConfigSeriesRules.Add(rule);
            return rule;
        }

        public ConfigSeriesRule GetSeriesRuleById(int id)
        {
            return db.ConfigSeriesRules.Where(s => s.Id == id).FirstOrDefault();
        }

        public override DomainObject GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel GetModelById(int id)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public override ListViewModel GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
