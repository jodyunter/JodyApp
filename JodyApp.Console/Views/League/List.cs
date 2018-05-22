using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views.League
{
    public class List:BaseListView
    {
        public override void CreateViewModel()
        {
            ViewModel = new LeagueListViewModel();
        }
        public override Func<string> GetHeaderStringForSingleEntity { get { return null; } }

        public override Func<BaseViewModel, string> GetDisplayStringNoHeaderSingleEntity { get { return null; } }

    }
}
