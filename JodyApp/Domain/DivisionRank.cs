using JodyApp.Domain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class DivisionRank: DomainObject, IComparable<RecordTableTeam>
    {
        public RecordTableDivision Division { get; set; }
        public Team Team { get; set; }
        public int Rank { get; set; }

        int IComparable<RecordTableTeam>.CompareTo(RecordTableTeam other)
        {
            if (Division.Level.Equals(other.Division.Level))
            {
                if (Division.Order.Equals(other.Division.Order))
                {
                    return Rank.CompareTo(Rank);
                }
                else
                {
                    return Division.Order.CompareTo(other.Division.Order);
                }
            }
            else
            {
                return Division.Level.CompareTo(other.Division.Level);
            }
        }
    }
}
