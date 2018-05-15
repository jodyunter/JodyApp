using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigCompetition:DomainObject, BaseConfigItem
    {
        public const int SEASON = 0;
        public const int PLAYOFF = 1;

        virtual public League League { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        virtual public ConfigCompetition Reference { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigCompetition() { }
        public ConfigCompetition(League league, string name, int type, ConfigCompetition reference, int? firstYear, int? lastYear)
        {
            League = league;
            Name = name;
            Type = type;
            Reference = reference;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
