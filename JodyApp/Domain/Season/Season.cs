using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Season
{
    public class Season
    {        
        public int Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }

        public SeasonTable Standings { get; set; }
    }
}
