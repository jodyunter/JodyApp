using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyApp.Domain
{    
    public class Team : DomainObject
    {               
        public String Name;        
        public int Skill;        
        public Division Division;
    }
}
