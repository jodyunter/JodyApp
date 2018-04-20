using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    [Table("DivisionRanks")]
    public abstract class DivisionRank:DomainObject, IComparable<DivisionRank>
    {
        public Division Division { get; set; }
        public Team Team { get; set; }
        public int Rank { get; set; }


        public int CompareTo(DivisionRank other)
        {

            if (Division.Level.Equals(other.Division.Level))
            {
                if (Division.Order.Equals(other.Division.Order))
                {
                    return Rank.CompareTo(other.Rank);
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
