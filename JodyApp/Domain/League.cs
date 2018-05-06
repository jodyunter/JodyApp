using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class League:DomainObject
    {
        public string Name { get; set; }
        public int CurrentYear { get; set; }
        public String SeasonName { get; set; }
        public String PlayoffName { get; set; }
        virtual public List<ReferenceCompetition> ReferenceCompetitions { get; set; }

        public League() { ReferenceCompetitions = new List<ReferenceCompetition>(); }
    }
}
