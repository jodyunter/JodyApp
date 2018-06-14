using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Playoffs;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.CompetitionServices
{
    public class GroupRuleService : BaseService<GroupRule>
    {
        public GroupRuleService(JodyAppContext db) : base(db) { }
        public override DbSet<GroupRule> Entities => db.GroupRules;

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public GroupRule CreateGroupRule(ConfigGroupRule rule, Division fromDivision, Team team, Group g)
        {
            var newGroupRule = new GroupRule(rule, fromDivision, team, g);

            Entities.Add(newGroupRule);

            return newGroupRule;
        }
    }
}
