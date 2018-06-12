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
    public class ConfigGroupRuleService:BaseService<ConfigGroupRule>
    {
        public override DbSet<ConfigGroupRule> Entities => db.ConfigGroupRules;

        public ConfigGroupRuleService(JodyAppContext db) : base(db) { }
        
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

        public List<ConfigGroup> GetGroups(ConfigCompetition playoff)
        {
            return db.ConfigGroups.Where(group => group.Playoff.Id == playoff.Id).ToList();
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
