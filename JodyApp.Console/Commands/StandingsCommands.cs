using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Domain;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class StandingsCommands:BaseViewCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => throw new NotImplementedException("Standings select method not applicable");

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public StandingsCommands(ApplicationContext context) : base(context, "Standings") { }        
        public BaseView View(ApplicationContext context, int seasonId, int divisionLevel)
        {
            var seasonService = (SeasonService)context.ServiceLibraries["Season"];
            var standingsService = (StandingsService)context.ServiceLibraries["Standings"];

            var model = standingsService.GetBySeasonAndDivisionLevel((Season)seasonService.GetById(seasonId), divisionLevel);
            var view = new StandingsView(model);

            return view;
                        
        }

        [Command]
        public override BaseView View(ApplicationContext context, int? id = null, string prompt = "Select>")
        {
            if (context.SelectedSeason == null)
            {
                context.SelectedSeason = SeasonCommands.SelectSeason(context);
            }

            int divisionLevel = int.Parse(Application.ReadFromConsole(context, "Enter Division Level>"));

            return View(context, (int)context.SelectedSeason.Id, divisionLevel);
        }


        public override BaseView GetView(BaseViewModel model)
        {
            return new StandingsRecordView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new StandingsView(model);
        }

        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }
}
