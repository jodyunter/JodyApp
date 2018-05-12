using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views.Display
{
    public class LeagueDisplay
    {
        public static string LEAGUE_SINGLE_FORMATTER = "{0,-5}{1,-15}{2,5}{3,10} {4,-15}";

        public static string GetDisplayStringNoHeaderSingleEntity(BaseViewModel viewModel)
        {
            var vm = (LeagueViewModel)viewModel;

            return String.Format(LEAGUE_SINGLE_FORMATTER, vm.Id, vm.LeagueName, vm.CurrentYear, vm.IsComplete, vm.CurrentCompetition);
        }
        public static string GetHeaderStringForSingleEntity()
        {
            return string.Format(LEAGUE_SINGLE_FORMATTER, "Id", "Name", "Year", "Complete", "Next" );
        }

    }
}
