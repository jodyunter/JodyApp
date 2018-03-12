using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Table;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyApp.Domain.Season
{    
    public class SeasonTeam:RecordTableTeam
    {
        virtual public Season Season { get; set; }

        public SeasonTeam() { }
        public SeasonTeam(Team team, SeasonDivision division)
            :base(team)
        {
            this.Division = division;
        }
        
    }
}
