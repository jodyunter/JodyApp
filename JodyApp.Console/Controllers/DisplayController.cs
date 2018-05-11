using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class DisplayController:BaseController
    {
        public override void ParseInput(List<string> input, int offset)
        {
            var variable = input[0 + offset];
            switch(variable)
            {
                case "League":
                    var leagueController = new LeagueController();
                    var leagueId = input[1 + offset];
                    leagueController.DisplayLeague(leagueId);
                    break;
                case "Leagues":
                    break;
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");
            }
        }
    }
}
