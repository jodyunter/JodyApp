using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.ConsoleApp.Views.Division;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class DivisionCommands : BaseViewCommands
    {
        public override Func<ApplicationContext, ReferenceObject> SelectMethod => SelectDivision;

        public DivisionCommands() : base() { Service = new ConfigDivisionService(); }



        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -1)
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context);
                searchId = (int)selectedLeague.Id;
            }

            var listView = new DivisionListView(((ConfigDivisionService)Service).GetByLeagueId(searchId));

            return listView;

        }
        
        public static ReferenceObject SelectDivision(ApplicationContext context)
        {
            return SelectDivisionWithLeagueId(context, -1);
        }
        public static ReferenceObject SelectDivisionWithLeagueId(ApplicationContext context, int leagueId = -1)
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            var commands = new DivisionCommands();

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context);
                searchId = (int)selectedLeague.Id;
            }

            var view = commands.ListByLeague(context, searchId);

            return GetSelectedObject(context, "Choose Division>", view);
        }


        public override BaseView GetView(BaseViewModel model)
        {
            return new DivisionView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new DivisionListView(model);
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
