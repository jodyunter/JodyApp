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
    public class ConfigGroupService:BaseService<ConfigGroup>
    {

        public ConfigGroupService(JodyAppContext db) : base(db) { }

        public override DbSet<ConfigGroup> Entities => db.ConfigGroups;

        public ConfigGroup CreateGroup(string name, ConfigCompetition playoff, List<ConfigGroupRule> groupRules,
                            ConfigDivision sortByDivision, int? firstYear, int? lastYear)
        {
            var group = new ConfigGroup(name, playoff, groupRules, sortByDivision, firstYear, lastYear);
            db.ConfigGroups.Add(group);
            return group;

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
