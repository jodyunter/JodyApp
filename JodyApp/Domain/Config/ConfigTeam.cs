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
        virtual public ConfigDivision Division { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigTeam(string name, int skill, ConfigDivision division, int? firstYear, int? lastYear)
        {
            Name = name;
            Skill = skill;
            Division = division;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
