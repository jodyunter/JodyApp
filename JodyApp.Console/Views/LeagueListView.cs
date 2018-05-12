using JodyApp.Console.Views.Display;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueListView : BaseListView
    {

        public LeagueListView()
        {
            ViewModel = new LeagueListViewModel();
        }

        public override Func<string> GetHeaderStringForSingleEntity { get { return LeagueDisplay.GetHeaderStringForSingleEntity; } }

        public override Func<BaseViewModel, string> GetDisplayStringNoHeaderSingleEntity { get { return LeagueDisplay.GetDisplayStringNoHeaderSingleEntity; } }

    }
}
