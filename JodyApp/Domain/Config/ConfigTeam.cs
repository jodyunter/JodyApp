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

        public override bool Equals(object obj)
        {
            var that = (ConfigTeam)obj;

            return this.Id == that.Id &&
                this.Name.Equals(that.Name) &&
                this.League.AreTheSame(that.League) &&
                this.Skill.Equals(that.Skill) &&
                this.FirstYear.Equals(that.FirstYear) &&
                this.LastYear.Equals(that.LastYear);
        }

        public override bool AreTheSame(DomainObject obj)
        {
            var that = (ConfigTeam)obj;

            return this.Name.Equals(that.Name);
        }
    }
}
