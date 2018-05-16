using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Config;
using JodyApp.Domain.Table;

namespace JodyApp.Domain
{
    [Table("Divisions")]
    public class Division : DomainObject, IComparable<Division>
    {
        private string _shortName;

        public string Name { get; set; }
        public string ShortName { get { if (_shortName == null) return Name; else return _shortName; } set { _shortName = value; } }
        virtual public List<Team> Teams { get; set; }
        virtual public Division Parent { get; set; }        
        [InverseProperty("Division")]
        virtual public List<SortingRule> SortingRules { get; set; }
        virtual public List<DivisionRank> Rankings { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        virtual public Season Season { get; set; }
        virtual public ConfigDivision ConfigDivision { get; set; }

        virtual public League League { get; set; }
        public Division() { }
        public Division(ConfigDivision configDivision, League league, Season season, string name, string shortName, int level, int order, Division parent)
        {
            ConfigDivision = configDivision;
            this.Name = name;
            this.ShortName = shortName;
            this.Level = level;
            this.Order = order;
            this.Parent = parent;
            this.League = league;
            this.Season = season;
            Teams = new List<Team>();
            Rankings = new List<DivisionRank>();
            SortingRules = new List<SortingRule>();
                 
        }
        public Division(ConfigDivision configDivision, League league, Season season, string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules) : this(configDivision,league, season, name, shortName, level, order, parent)
        {
            if (sortingRules == null) sortingRules = new List<SortingRule>();
            SortingRules = sortingRules;
        }

        public int CompareTo(Division other)
        {
            if (Level.Equals(other.Level))
            {
                return Order.CompareTo(other.Order);
            } else
            {
                return Level.CompareTo(other.Level);
            }
        }
        public void SetRank(int rank, Team team)
        {
            if (Rankings == null) Rankings = new List<DivisionRank>();

            if (Rankings.Any(r => r.Team.Name.Equals(team.Name)))
            {
                Rankings.Find(r => r.Team.Name.Equals(team.Name)).Rank = rank;
            }
            else
            {
                Rankings.Add(new DivisionRank() { Division = this, Team = team, Rank = rank });
            }
        }

        public Team GetByRank(int rank)
        {
            if (Rankings != null)
            {
                return Rankings.Where(d => d.Rank == rank).First().Team;
            }

            return null;
        }

        public int GetRank(Team team)
        {
            int ArbitraryRankForTeamNotInDivisionOrRanked = 20000;
            if (Rankings != null && team != null)
            {
                //must be name because we will be comparing the playoff team to season team
                var dr = Rankings.Where(d => d.Team.Name == team.Name).FirstOrDefault();
                if (dr != null) return dr.Rank;
                else return ArbitraryRankForTeamNotInDivisionOrRanked;

            }
            //return an arbitrarily large number to ensure teams without rank are sorted at the bottom
            return ArbitraryRankForTeamNotInDivisionOrRanked;

        }

        public override bool Equals(object obj)
        {
            var division = obj as Division;
            return division != null &&
                   _shortName == division._shortName &&
                   Name == division.Name &&
                   ShortName == division.ShortName &&
                   EqualityComparer<Division>.Default.Equals(Parent, division.Parent) &&
                   Level == division.Level &&
                   Order == division.Order &&
                   EqualityComparer<Season>.Default.Equals(Season, division.Season) &&
                   EqualityComparer<League>.Default.Equals(League, division.League);
        }

        public override int GetHashCode()
        {
            var hashCode = -1501689248;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_shortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Division>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + Order.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Season>.Default.GetHashCode(Season);
            hashCode = hashCode * -1521134295 + EqualityComparer<League>.Default.GetHashCode(League);
            return hashCode;
        }
    }
}
