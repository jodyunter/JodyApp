using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Console.Views;
using JodyApp.ViewModel;

namespace JodyApp.Console.Controllers
{
    public class SeasonController : BaseController
    {
        public override BaseView ParseInput(List<string> input, int offset)
        {
            var variable = input[offset];

            switch (variable)
            {
                case "Get":
                    int id;
                    if (int.TryParse(input[offset + 1], out id))
                    {
                        View = new SeasonView();
                        ((SingleEntityViewModel)View.ViewModel).SetById(id);
                    }
                    else
                    {
                        //how to deal with errors
                    }
                    break;
                case "List":
                    int leagueId;
                    View = new SeasonListView();
                    if (input.Count >= offset + 1 + 1)
                        if (int.TryParse(input[offset + 1], out leagueId))
                            ((SeasonListViewModel)View.ViewModel).LeagueId = leagueId;                   
                    ((SeasonListViewModel)View.ViewModel).SetData();

                    break;
            }

            return View;
        }
    }
}
