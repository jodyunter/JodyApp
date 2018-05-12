using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Console.Views;
using JodyApp.ViewModel;

namespace JodyApp.Console.Controllers
{
    public class LeagueController : BaseController
    {
        public override BaseView ParseInput(List<string> input, int offset)
        {
            var variable = input[offset];
            
            switch(variable)
            {
                case "League":
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
                case "Leagues":
                    View = new LeagueListView();
                    ((LeagueListViewModel)View.ViewModel).SetData();
                    break;
            }

            return View;
        }
    }
}
