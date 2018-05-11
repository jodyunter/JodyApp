using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueView : BaseView
    {
        public string CurrentLeague { get; set; }
        public override string BuildOptions()
        {
            string result = "";

            result += "\n" + BuildOption(1, "Display Leagues");
            result += "\n" + BuildOption(2, "Create League");           

            return result;
                        
        }

        public override string GetStartingView()
        {
            string result = "League Menu";
            result += Options;
            result += "\nEnter Option: ";
            return result;
        }
        
        public override void SetViewFromOption()
        {
            switch(LastOption)
            {
                case -1:
                    CurrentDisplay = GetStartingView();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    CurrentDisplay = "Bad option when setting the view.";
                    break;

            }
 
        }

    }
}
