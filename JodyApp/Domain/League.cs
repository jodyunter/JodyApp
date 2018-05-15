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
        virtual public List<ReferenceCompetition> ReferenceCompetitions { get; set; }

        public League(string name)
        {
            this.Name = name;
            CurrentYear = 0;
            ReferenceCompetitions = new List<ReferenceCompetition>();
        }
        public League() { ReferenceCompetitions = new List<ReferenceCompetition>(); }

        public override bool AreTheSame(DomainObject obj)
        {
            var that = (League)obj;

            return this.Name.Equals(that.Name);
        }
    }
}
