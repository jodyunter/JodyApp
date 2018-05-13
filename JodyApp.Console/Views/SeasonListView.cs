using JodyApp.Console.Views.Display;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class SeasonListView : BaseListView
    {

        public override void CreateViewModel()
        {
            ViewModel = new SeasonListViewModel();
        }

        public override Func<string> GetHeaderStringForSingleEntity { get { return SeasonDisplay.GetHeaderStringForSingleEntity; } }

        public override Func<BaseViewModel, string> GetDisplayStringNoHeaderSingleEntity { get { return SeasonDisplay.GetDisplayStringNoHeaderSingleEntity; } }

    }
}
