using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using JodyApp.ViewModel;

namespace JodyApp.Service.CompetitionServices
{
    public class GroupService : BaseService<Group>
    {
        public GroupService(JodyAppContext db) : base(db) { }

        public override System.Data.Entity.DbSet<Group> Entities => db.Groups;

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public Group CreateGroup(string name, Playoff playoff, Division sortByDivision, List<GroupRule> groupRules)
        {
            var newGroup = new Group(name, playoff, sortByDivision, groupRules);

            Entities.Add(newGroup);

            return newGroup;
        }
    }
}
