using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigCompetition:DomainObject, BaseConfigItem
    {
        public const int SEASON = 0;
        public const int PLAYOFF = 1;

        virtual public League League { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        //this is the competition it will use to set things up.  Like a season for a playoff
        virtual public ConfigCompetition Reference { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigCompetition() { }
        public ConfigCompetition(League league, string name, int type, ConfigCompetition reference, int? firstYear, int? lastYear)
        {
            League = league;
            Name = name;
            Type = type;
            Reference = reference;
            FirstYear = firstYear;
            LastYear = lastYear;
        }

        public override bool Equals(object obj)
        {
            var competition = obj as ConfigCompetition;
            return competition != null &&
                   EqualityComparer<League>.Default.Equals(League, competition.League) &&
                   Name == competition.Name &&
                   Type == competition.Type &&                   
                   EqualityComparer<int?>.Default.Equals(FirstYear, competition.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, competition.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = -1318247555;
            hashCode = hashCode * -1521134295 + EqualityComparer<League>.Default.GetHashCode(League);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();            
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
