using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigTeamViewModel:BaseViewModel
    {        
        public int Skill { get; set; }        
        public ReferenceObject League { get; set; }        
        public ReferenceObject Division { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigTeamViewModel(int? id, string name, int skill, int? leagueId, string league, int? divisionId, string division, int? firstYear, int? lastYear)
        {
            Id = id;
            Name = name;
            Skill = skill;
            League = leagueId == null ? null : new ReferenceObject(leagueId, league);
            Division = divisionId == null ? null : new ReferenceObject(divisionId, division);
            FirstYear = firstYear;
            LastYear = lastYear;
        }

  
    }
}
