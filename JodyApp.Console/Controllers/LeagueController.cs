using JodyApp.Console.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public class LeagueController:BaseController
    {
        LeagueView View = new LeagueView();

        public override void ParseInput(List<string> input, int offset) { throw new NotImplementedException("Parse not implemented in League Controller"); }

        public void DisplayLeagues()
        {
            
        }

        public void DisplayLeague(string input)
        {
            int id = int.Parse(input);

            View.viewModel.SetById(id);

            System.Console.WriteLine(View.GetDisplayString());
            

        }

    }
}
