using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Domain;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using JodyApp.Service.CompetitionServices;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public class StandingsCommands:BaseViewCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => throw new NotImplementedException("Standings select method not applicable");

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public StandingsCommands(ApplicationContext context) : base(context, SERVICE_STANDINGS) { }        
        public BaseView View(ApplicationContext context, int seasonId, int divisionLevel)
        {
            var seasonService = (SeasonService)context.ServiceLibraries[SERVICE_SEASON];            

            var model = ((StandingsService)Service).GetModelBySeasonAndDivisionLevel(seasonId, divisionLevel);
            var view = new StandingsView(model);

            return view;
                        
        }

        [Command]
        public override BaseView View(ApplicationContext context, int? id = null, string prompt = "Select>")
        {
            var seasonId = SeasonCommands.SelectSeason(context).Id;

            SeasonCommands.ClearSelectedSeason(context);

            int divisionLevel = int.Parse(Application.ReadFromConsole(context, "Enter Division Level>"));

            return View(context, (int)seasonId, divisionLevel);
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

        [Command]
        public BaseListView ViewByDivision(ApplicationContext context)
        {
            var selectedSeason = SeasonCommands.SelectSeason(context);

            var divisionService = (DivisionService)context.ServiceLibraries[SERVICE_DIVISION];

            var selectedDivision = GetSelectedObject(context, "select division>", new ReferenceObjectListView(divisionService.GetModelsBySeasonId((int)selectedSeason.Id)));

            var service = (StandingsService)Service;

            return GetList(service.GetModelsBySeasonIdAndDivisionId((int)selectedSeason.Id, (int)selectedDivision.Id));
            
        }

        [Command]
        public BaseListView ViewByTeam(ApplicationContext context)
        {
            var selectedTeam = TeamCommands.SelectTeam(context);

            var service = (StandingsService)Service;

            return GetList(service.GetModelsByTeam((int)selectedTeam.Id));
        }
    }
}
