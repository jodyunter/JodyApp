using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Group:DomainObject
    {
        public string Name { get; set; }
        virtual public Playoff Playoff { get; set; }
        virtual public List<GroupRule> GroupRules { get; set; }
        virtual public Division SortByDivision { get; set; }

        public Group() { }

        public Group(string name, Playoff playoff, Division sortByDivision, List<GroupRule> groupRules)
        {
            Name = name;
            Playoff = playoff;
            GroupRules = groupRules;
            SortByDivision = sortByDivision;
        }

    }
}
