using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyApp.Domain
{    
    public class Division:DomainObject
    {
        public string Name { get; set; }
        virtual public List<Team> Teams { get; set; }
        virtual public Division Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }
}
