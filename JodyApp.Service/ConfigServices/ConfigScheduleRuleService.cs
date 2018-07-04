using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigScheduleRuleService : BaseService<ConfigScheduleRule>
    {
        public override DbSet<ConfigScheduleRule> Entities => db.ConfigScheduleRules;

        public ConfigScheduleRuleService(JodyAppContext db) : base(db) { }

        public List<ConfigScheduleRule> GetScheduleRulesByCompetition(ConfigCompetition competition)
        {
            return db.ConfigScheduleRules.Where(sr => sr.Competition.Id == competition.Id).ToList();
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

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var rule = (ConfigScheduleRule)obj;

            var model = new ScheduleRuleViewModel(rule.Id, 
                GetReferenceObject(rule.League),
                GetScheduleRuleConstant(rule.HomeType),
                GetReferenceObject(rule.HomeTeam),
                GetReferenceObject(rule.HomeDivision),
                GetScheduleRuleConstant(rule.AwayType),
                GetReferenceObject(rule.AwayTeam),
                GetReferenceObject(rule.AwayDivision),
                rule.PlayHomeAway, rule.Rounds, rule.DivisionLevel, rule.Order,
                GetReferenceObject(rule.Competition),
                rule.Reverse
                );

            return model;
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public int GetScheduleRuleConstant(string constant)
        {
            switch(constant)
            {
                case ScheduleRuleViewModel.TYPE_DIVISION:
                    return ConfigScheduleRule.BY_DIVISION;
                case ScheduleRuleViewModel.TYPE_DIVISION_LEVEL:
                    return ConfigScheduleRule.BY_DIVISION_LEVEL;
                case ScheduleRuleViewModel.TYPE_TEAM:
                    return ConfigScheduleRule.BY_TEAM;
                case ScheduleRuleViewModel.TYPE_NONE:
                    return ConfigScheduleRule.NONE;
                default:
                    return -5000;
            }
        }
        public string GetScheduleRuleConstant(int constant)
        {
            switch (constant)
            {
                case ConfigScheduleRule.BY_DIVISION:
                    return ScheduleRuleViewModel.TYPE_DIVISION;
                case ConfigScheduleRule.BY_DIVISION_LEVEL:
                    return ScheduleRuleViewModel.TYPE_DIVISION_LEVEL;
                case ConfigScheduleRule.BY_TEAM:
                    return ScheduleRuleViewModel.TYPE_TEAM;
                case ConfigScheduleRule.NONE:
                    return ScheduleRuleViewModel.TYPE_NONE;                    
                default:
                    return "Bad data";
            }
        }
    }
}
