using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSeason
    {
        public League League { get; set; }
        public string Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }  //this is how we group everything together   
        public bool Started { get; set; }
        public bool Complete { get; set; }
        public int StartingDay { get; set; }
    }
}
