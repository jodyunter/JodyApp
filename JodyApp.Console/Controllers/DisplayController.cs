using JodyApp.Console.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class DisplayController:Controller
    {        

        public override View ParseInput(List<string> input, int offset)
        {            
            var variable = input[0 + offset];
            switch(variable)
            {
                case "League":                    
                    var leagueId = int.Parse(input[1 + offset]);
                    View = new LeagueView();
                    ((SingleEntityViewModel)View.ViewModel).SetById(leagueId);
                    break;
                case "Season":
                    var seasonId = int.Parse(input[1 + offset]);
                    View = new SeasonView();
                    ((SingleEntityViewModel)View.ViewModel).SetById(seasonId);
                    break;
                case "Leagues":
                    View = new LeagueListView();
                    ((LeagueListViewModel)View.ViewModel).SetData();
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");

            }

            return View;
        }
    }
}
