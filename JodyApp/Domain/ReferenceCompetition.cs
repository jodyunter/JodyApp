using JodyApp.Database;
using JodyApp.Domain.Config;
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
        virtual public League League { get; set; }
        virtual public ConfigSeason Season { get; set; }
        public int Order { get; set; }
        virtual public Playoff Playoff { get; set; }

    }
}
