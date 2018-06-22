using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ScheduleViewModel:BaseViewModel
    {
        public CompetitionViewModel Competition { get; set; }
        public ListViewModel ScheduleDayViewModels { get; set; }

        public ScheduleViewModel():base() { }

        
        public ScheduleViewModel(CompetitionViewModel competition, ListViewModel scheduleDayViewModels)
        {
            Competition = competition;
            ScheduleDayViewModels = scheduleDayViewModels;
        }
    }
}
