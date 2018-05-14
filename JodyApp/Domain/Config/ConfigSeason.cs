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
        public int Year { get; set; }  //this is how we group everything together   
        public bool Started { get; set; }
        public bool Complete { get; set; }
        public int StartingDay { get; set; }

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigSeason(League league, string name, int year, bool started, bool complete, int startingDay, int? firstYear, int? lastYear)
        {
            League = league;
            Name = name;
            Year = year;
            Started = started;
            Complete = complete;
            StartingDay = startingDay;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
