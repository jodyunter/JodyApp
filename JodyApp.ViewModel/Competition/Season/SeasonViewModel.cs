using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeasonViewModel:CompetitionViewModel
    {                
        public SeasonViewModel(int? id, ReferenceObject league, string name, int year, string competitionType,  bool started, bool complete, int startingDay)
            : base(id, league, name, year, competitionType, started, complete, startingDay) { }

    
    }
}
