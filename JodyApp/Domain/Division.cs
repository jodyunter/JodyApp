using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyApp.Domain
{    
    public class Division: DomainObject,IEquatable<Division>, IComparable<Division>
    {
        public string Name { get; set; }
        virtual public List<Team> Teams { get; set; }
        virtual public Division Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }

        public Division() { }
        public Division(string name, int level, int order, Division parent)
        {
            this.Name = name;
            this.Level = level;
            this.Order = order;
            this.Parent = parent;
            Teams = new List<Team>();
                 
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

        public bool Equals(Division other)
        {
            return this.Name.Equals(other.Name);
        }
    }
}
