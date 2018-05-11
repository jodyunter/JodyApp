using JodyApp.Console.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace JodyApp.Console.Controllers
{
    public abstract class BaseController
    {
        public BaseView View { get; set; }        
        public bool Error { get; set; }
        public void Display()
        {
            View.SetViewFromOption();
            WriteLine(View.CurrentDisplay);
        }
        public string GetInput()
        {
            Error = false;
            string input = ReadLine();

            string result = ValidateInput(input);

            if (!String.IsNullOrEmpty(result))
            {
                Error = true;
                input = result;
            }
            return input;
        }

        public abstract string ValidateInput(string input);
        public abstract void ProcessInput(string input);

    }
}
