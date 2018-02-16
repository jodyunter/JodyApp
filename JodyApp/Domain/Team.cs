using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class Team : DomainObject
    {       
        public String Name;
        public int Skill;

        public override bool Equals(object obj)
        {
            Team other = (Team)obj;

            return other.Name.Equals(this.Name);             
        }

        public override int GetHashCode()
        {
            var hashCode = -1879921716;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Skill.GetHashCode();
            return hashCode;
        }
    }
}
