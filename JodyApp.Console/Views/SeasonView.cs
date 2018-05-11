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
        public override string FORMATTER { get { return "{0,-5}{1,-15}{2,-15}{3,5} {4,10}"; } }          

        public SeasonView()
        {
            ViewModel = new SeasonViewModel();
        }

        public override object[] GetHeaderStrings()
        {
            return new object[] { "Id", "League", "Name", "Year", "Complete" };
        }
        public override object[] GetDisplayStrings()
        {
            var vm = (SeasonViewModel)ViewModel;
            return new object[] { vm.Id, vm.LeagueName, vm.Name, vm.Year, vm.Complete};
        }


    }
}
