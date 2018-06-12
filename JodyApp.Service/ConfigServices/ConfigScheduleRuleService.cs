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
    public class ConfigScheduleRuleService:BaseService<ConfigScheduleRule>
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
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
