using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Playoff : DomainObject, Competition
    {
        public int Year { get; set; }
        public int StartingDay { get; set; }
        public bool Complete { get; set; }
        public bool Started { get; set; }
        public League League { get; set; }

        virtual public List<PlayoffSeries> Series { get; set; }
        
    }
}
