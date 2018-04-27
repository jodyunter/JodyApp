using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class RoundRules:DomainObject
    {
        public League League { get; set; }
        public Playoff Playoff { get; set; }
        public int Round { get; set; }
        virtual public List<RoundGroupRule> GroupRules { get; set; }
    }
}
