using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ScheduleDayViewModel:BaseViewModel
    {
        public int Day { get; set; }
        public ListViewModel GameViewModels { get; set; }

        public ScheduleDayViewModel():base() { }
        public ScheduleDayViewModel(int day, ListViewModel gameViewModels):base()
        {
            this.Day = day;
            this.GameViewModels = gameViewModels;
        }
    }
}
