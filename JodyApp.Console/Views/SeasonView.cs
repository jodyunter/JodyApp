using JodyApp.Console.Views.Display;
using JodyApp.Database;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class SeasonView:SingleEntityView
    {            

        public SeasonView()
        {
            ViewModel = new SeasonViewModel();
        }

        public override string GetHeaderString()
        {
            return SeasonDisplay.GetHeaderStringForSingleEntity();
        }

        public override string GetDisplayStringNoHeader()
        {
            return SeasonDisplay.GetDisplayStringNoHeaderSingleEntity((SeasonViewModel)ViewModel);
        }

    }
}
