using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel.Competition
{
    public class ScheduleViewModel:BaseViewModel
    {
        CompetitionViewModel Competition { get; set; }
        ListViewModel Games { get; set; }
            
    }
}
