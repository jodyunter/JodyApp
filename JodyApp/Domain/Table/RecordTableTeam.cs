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
            this.Stats = new TeamStatitistics();
        }
        public TeamStatitistics Stats { get; set; }

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
                            return this.CompareStatTo(TeamStatitistics.Stats.GoalsFor, other);
                        }

                        return this.CompareStatTo(TeamStatitistics.Stats.GoalDifference, other);
                    }
                    return this.CompareStatTo(TeamStatitistics.Stats.Wins, other);
                }
                return this.CompareStatTo(TeamStatitistics.Stats.GamesPlayed, other);
            }
            return this.CompareStatTo(TeamStatitistics.Stats.Points, other);
        }

        public bool Equals(RecordTableTeam other)
        {
            return this.Name.Equals(other.Name);
        }

        public int CompareStatTo(TeamStatitistics.Stats stat, RecordTableTeam other)
        {
            switch(stat) {
                case TeamStatitistics.Stats.Points:
                    return this.Stats.Points.CompareTo(other.Stats.Points);
                case TeamStatitistics.Stats.GamesPlayed:
                    return other.Stats.GamesPlayed.CompareTo(this.Stats.GamesPlayed);
                case TeamStatitistics.Stats.Wins:
                    return this.Stats.Wins.CompareTo(other.Stats.Wins);
                case TeamStatitistics.Stats.GoalDifference:
                    return this.Stats.GoalDifference.CompareTo(other.Stats.GoalDifference);
                case TeamStatitistics.Stats.GoalsFor:
                    return this.Stats.GoalsFor.CompareTo(other.Stats.GoalsFor);
                default:
                    throw new NotImplementedException("This stat you are using isn't implemented.");
            }
        }
    }
}
