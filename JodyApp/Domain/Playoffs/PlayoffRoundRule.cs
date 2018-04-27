using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class PlayoffRoundRule:DomainObject
    {
        public League League { get; set; }
        public Playoff Playoff { get; set; }
        int Round { get; set; }        
    }
}
