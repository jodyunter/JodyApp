using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Domain
{    
    [Table("Teams")]
    public partial class Team : DomainObject, IComparable<Team>
    {               
        public String Name { get; set; }
        public int Skill { get; set; }
        virtual public Division Division { get; set; }
        virtual public TeamStatistics Stats { get; set; }
        virtual public Season Season { get; set; }
        virtual public Playoff Playoff { get; set; }


        public Team() { }
        public Team(string name, int skill, Division div)
        {
            this.Name = name;
            this.Skill = skill;
            this.Division = Division;
            if (Division != null) Division.Teams.Add(this);            
        }
                
        public Team(string name, int skill, TeamStatistics stats, Division division)
        {
            this.Name = name;
            this.Skill = skill;
            this.Stats = stats;
            this.Division = division;
        }        
        
        public Team(Team team, Division division)
            : this(team.Name, team.Skill, new TeamStatistics(), division)
        {
            //this.Division = division;
        }

        public Boolean IsTeamInDivision(String divisionName)
        {
            Division p = Division;

            Boolean isInDivision = false;
            while (p != null && !isInDivision)
            {

                if (p.Name.Equals(divisionName)) isInDivision = true;

                p = p.Parent;
            }

            return isInDivision;

        }

        public int CompareTo(Team other)
        {
            if (this.Stats.Points.Equals(other.Stats.Points))
            {
                if (this.Stats.GamesPlayed.Equals(other.Stats.GamesPlayed))
                {
                    if (this.Stats.Wins.Equals(other.Stats.Wins))
                    {
                        if (this.Stats.GoalDifference.Equals(other.Stats.GoalDifference))
                        {
                            return -1 * this.CompareStatTo(TeamStatistics.Stats.GoalsFor, other);
                        }

                        return -1 * this.CompareStatTo(TeamStatistics.Stats.GoalDifference, other);
                    }
                    return -1 * this.CompareStatTo(TeamStatistics.Stats.Wins, other);
                }
                return -1 * this.CompareStatTo(TeamStatistics.Stats.GamesPlayed, other);
            }
            return -1 * this.CompareStatTo(TeamStatistics.Stats.Points, other);
        }

        public int CompareStatTo(TeamStatistics.Stats stat, Team other)
        {
            switch (stat)
            {
                case TeamStatistics.Stats.Points:
                    return this.Stats.Points.CompareTo(other.Stats.Points);
                case TeamStatistics.Stats.GamesPlayed:
                    return other.Stats.GamesPlayed.CompareTo(this.Stats.GamesPlayed);
                case TeamStatistics.Stats.Wins:
                    return this.Stats.Wins.CompareTo(other.Stats.Wins);
                case TeamStatistics.Stats.GoalDifference:
                    return this.Stats.GoalDifference.CompareTo(other.Stats.GoalDifference);
                case TeamStatistics.Stats.GoalsFor:
                    return this.Stats.GoalsFor.CompareTo(other.Stats.GoalsFor);
                default:
                    throw new NotImplementedException("This stat you are using isn't implemented.");
            }
        }
    }
}
