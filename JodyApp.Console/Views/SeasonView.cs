using JodyApp.Database;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class SeasonView
    {
        public SeasonViewModel viewModel { get; set; }
        StandingsView standingsView;

        public SeasonView()
        {            
            standingsView = new StandingsView();
        }
        public String GetDisplayString()
        {
            standingsView.viewModel = viewModel.Standings;
            return standingsView.GetDisplayString();
        }
    }
}
