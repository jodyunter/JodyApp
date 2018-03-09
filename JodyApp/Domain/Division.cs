using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class Division:DomainObject
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public Division Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }
}
