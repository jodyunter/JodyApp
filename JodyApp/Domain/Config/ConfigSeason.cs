using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSeason:DomainObject, BaseConfigItem
    {
        public League League { get; set; }
        public string Name { get; set; }  //may not be unique, name + year should be unique        

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }
        
        public ConfigSeason(League league, string name, int? firstYear, int? lastYear)
        {
            League = league;
            Name = name;            
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
