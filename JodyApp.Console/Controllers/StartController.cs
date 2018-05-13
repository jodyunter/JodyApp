using JodyApp.Console.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class StartController:BaseController
    {
        public override BaseView ParseInput(List<string> input, int offset)
        {
            string variable = input[0 + offset];
            switch(variable)
            {
                case "League":
                    var leagueController = new LeagueController();
                    View = leagueController.ParseInput(input, offset + 1);
                    break;
                case "Season":
                    var seasonController = new SeasonController();
                    View = seasonController.ParseInput(input, offset + 1);
                    break;
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");
            }

            return View;
        }
    }
}
