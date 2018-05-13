using JodyApp.Console.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class DisplayController:BaseController
    {        

        public override BaseView ParseInput(List<string> input, int offset)
        {            
            var variable = input[0 + offset];
            switch(variable)
            {
                case "League":
                case "Leagues":
                    return new LeagueController().ParseInput(input, offset);                    
                case "Season":
                case "Seasons":
                    return new SeasonController().ParseInput(input, offset);                                                   
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");

            }
            
        }
    }
}
