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
        public String Name { get; set; }
        public int Skill { get; set; }
        virtual public Division Division { get; set; }

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
    }
}
