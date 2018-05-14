using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigTeam:DomainObject, BaseConfigItem
    {
        public string Name { get; set; }

        public int Skill { get; set; }
        virtual public List<ConfigDivision> Divisions { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }
        
    }
}
