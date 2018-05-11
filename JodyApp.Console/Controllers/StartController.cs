using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class StartController:BaseController
    {
        public override void ParseInput(List<string> input, int offset)
        {
            string variable = input[0 + offset];
            switch(variable)
            {
                case "Display":
                    var displayController = new DisplayController();
                    displayController.ParseInput(input, offset + 1);
                    break;
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");
            }
        }
    }
}
