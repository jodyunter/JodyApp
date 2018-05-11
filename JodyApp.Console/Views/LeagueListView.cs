using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueListView : View
    {
        public LeagueListView()
        {
            ViewModel = new LeagueListViewModel();
        }
        public override string GetDisplayString()
        {
            var vm = (LeagueListViewModel)ViewModel;
            string result = vm.Header;

            vm.Leagues.ForEach(league =>
            {
                result += "\n" + league.
            });
        }
    }
}
