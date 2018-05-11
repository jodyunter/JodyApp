using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class LeagueController:BaseController
    {

        public override void ProcessInput(string input)
        {
            switch(View.LastOption)
            {
                case -1:
                    int choice = int.Parse(input);
                    View.LastOption = choice;                    
                    break;
            }

        }
        public override string ValidateInput(string input)
        {

            switch (View.LastOption)
            {
                case -1:
                    return ValidateDefaultInput(input);
                default:
                    return "Woops";
            }


        }

        public string ValidateDefaultInput(string s)
        {
            string message = "";

            try
            {
                int.Parse(s);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is FormatException)
                {
                    message = "Please Input a Number.";
                    return message;
                }
                throw;
            }

            return message;
        }
    }
}
