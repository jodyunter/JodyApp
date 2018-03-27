using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Table
{    
    public class RecordTableTeam:Team, IEquatable<RecordTableTeam>, IComparable<RecordTableTeam>
    {            
        public RecordTableTeam() { }

        public RecordTableTeam(Team team)
        {
            this.Name = team.Name;
            this.Skill = team.Skill;
            this.Stats = new TeamStatistics();
            this.Division = team.Division;            
        }
        virtual public TeamStatistics Stats { get; set; }

        public int CompareTo(RecordTableTeam other)
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

        public bool Equals(RecordTableTeam other)
        {
            return this.Name.Equals(other.Name);
        }

        public int CompareStatTo(TeamStatistics.Stats stat, RecordTableTeam other)
        {
            switch(stat) {
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
