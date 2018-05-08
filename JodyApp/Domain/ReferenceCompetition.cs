using JodyApp.Database;
using JodyApp.Domain.Playoffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class ReferenceCompetition:DomainObject
    {
        public League League { get; set; }
        public Season Season { get; set; }
        public int Order { get; set; }
        public Playoff Playoff { get; set; }

    }
}
