using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueView : BaseView
    {
        public static string LEAGUE_FORMATTER = "{0,-5}{1,-15}{2,5}{3,10} {4,-15}";        
        public LeagueViewModel viewModel { get; set; }
        
        public LeagueView():base()
        {
            viewModel = new LeagueViewModel();
        }

        public string GetDisplayHeaderString()
        {
            return string.Format(LEAGUE_FORMATTER, "Id", "Name", "Year", "Complete", "Next");
        }
        public override string GetDisplayString()
        {            
            return 
                GetDisplayHeaderString() + "\n" +
                String.Format(LEAGUE_FORMATTER, viewModel.Id, viewModel.LeagueName, viewModel.CurrentYear, viewModel.IsComplete, viewModel.CurrentCompetition);
        }
    }
}
