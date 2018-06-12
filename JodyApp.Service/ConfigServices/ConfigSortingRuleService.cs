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
    public class ConfigSortingRuleService:BaseService<ConfigSortingRule>
    {


        public ConfigSortingRuleService(JodyAppContext db) : base(db) { }

        public override DbSet<ConfigSortingRule> Entities => db.ConfigSortingRules;

        public ConfigSortingRule CreateSortingRule(string name, int groupNumber, ConfigDivision division, ConfigDivision divisionToGetTeamsFrom,
                                    string positionsToUse, int divisionLevel, int type, int? firstYear, int? lastYear)
        {
            var rule = new ConfigSortingRule(name, groupNumber, division, divisionToGetTeamsFrom, positionsToUse, divisionLevel, type, firstYear, lastYear);
            db.ConfigSortingRules.Add(rule);
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
