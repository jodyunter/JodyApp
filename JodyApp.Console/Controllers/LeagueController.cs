using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Console.Flow;
using JodyApp.Console.Views;
using JodyApp.Service;
using JodyApp.ViewModel;

namespace JodyApp.Console.Controllers
{
    public class LeagueController : BaseController
    {
        //need to remove references to the database from here
        LeagueService leagueService = new LeagueService(new Database.JodyAppContext(Database.JodyAppContext.CURRENT_DATABASE));

        public override BaseView ParseInput(List<string> input, int offset)
        {
            var variable = input[offset];
            
            switch(variable)
            {
                case "Get":
                    int id;
                    if (int.TryParse(input[offset + 1], out id))
                    {
                        View = new LeagueView();
                        ((SingleEntityViewModel)View.ViewModel).SetById(id);
                    } else
                    {
                        //how to deal with errors
                    }
                    break;
                case "List":
                    View = new LeagueListView();
                    ((LeagueListViewModel)View.ViewModel).SetData();
                    break;
                case "Save":
                    break;
                case "Edit":
                    //is there any point to edit in console mode? Just display it so they can save it
                    break;
            }

            return View;
        }

        public BaseView CreateLeague()
        {
            var input = GatherInput.GetInput(new string[] { "Name" });

            var league = leagueService.CreateLeague(input["Name"]);

            var view = new LeagueView();
            view.ViewModel = new LeagueViewModel(league.Id, league.Name, league.CurrentYear, false, "");

            return view;

                        
        }
    }
}
