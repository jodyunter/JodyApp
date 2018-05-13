using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views.Display
{
    public class SeasonDisplay
    {
        public static string SEASON_SINGLE_FORMATTER = "{0,-5}{1,-15}{2,-15}{3,5} {4,10}";

        public static string GetDisplayStringNoHeaderSingleEntity(BaseViewModel viewModel)
        {
            var vm = (SeasonViewModel)viewModel;
            return String.Format(SEASON_SINGLE_FORMATTER, vm.Id, vm.LeagueName, vm.Name, vm.Year, vm.Complete);
        }
        public static string GetHeaderStringForSingleEntity()
        {
            return string.Format(SEASON_SINGLE_FORMATTER, "Id", "League", "Name", "Year", "Complete");
        }
    }
}
