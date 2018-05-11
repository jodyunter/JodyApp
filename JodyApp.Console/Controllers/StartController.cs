﻿using JodyApp.Console.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class StartController:Controller
    {
        public override View ParseInput(List<string> input, int offset)
        {
            string variable = input[0 + offset];
            switch(variable)
            {
                case "Display":
                    var displayController = new DisplayController();
                    View = displayController.ParseInput(input, offset + 1);
                    break;
                default:
                    throw new NotImplementedException(GetType().ToString() + " " + variable + " not implemented yet");
            }

            return View;
        }
    }
}
