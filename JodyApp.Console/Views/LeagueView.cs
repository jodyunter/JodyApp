using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueView : SingleEntityView
    {
        public override string FORMATTER { get { return "{0,-5}{1,-15}{2,5}{3,10} {4,-15}"; } }                
       

        public LeagueView():base()
        {
            ViewModel = new LeagueViewModel();
        }

        public override object[] GetHeaderStrings()
        {
            return new object[] { "Id", "Name", "Year", "Complete", "Next" };
        }
        public override object[] GetDisplayStrings()
        {
            var vm = (LeagueViewModel)ViewModel;
            return new object[] { vm.Id, vm.LeagueName, vm.CurrentYear, vm.IsComplete, vm.CurrentCompetition };
        }
    }
}
