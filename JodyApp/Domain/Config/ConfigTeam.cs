using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigTeam:DomainObject, BaseConfigItem
    {
        public League League { get; set; }
        public string Name { get; set; }

        public int Skill { get; set; }
        virtual public ConfigDivision Division { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigTeam() { }
        public ConfigTeam(string name, int skill, ConfigDivision division, League league, int? firstYear, int? lastYear)
        {
            Name = name;
            Skill = skill;
            Division = division;
            League = league;
            FirstYear = firstYear;
            LastYear = lastYear;
        }



        public override bool AreTheSame(DomainObject obj)
        {
            var that = (ConfigTeam)obj;

            return this.Name.Equals(that.Name);
        }

        public override bool Equals(object obj)
        {
            var team = obj as ConfigTeam;
            return team != null &&
                   EqualityComparer<League>.Default.Equals(League, team.League) &&
                   Name == team.Name &&
                   Skill == team.Skill &&
                   EqualityComparer<ConfigDivision>.Default.Equals(Division, team.Division) &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, team.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, team.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = 904675178;
            hashCode = hashCode * -1521134295 + EqualityComparer<League>.Default.GetHashCode(League);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Skill.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(Division);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
