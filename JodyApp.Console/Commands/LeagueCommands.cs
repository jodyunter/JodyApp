using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class LeagueCommands
    {

        public static BaseView View(ApplicationContext context, List<BaseView> lastViews, int id)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var model = service.GetById(id);

            var view = new LeagueView(model);

            return view;
        }
        public static BaseView Create(ApplicationContext context, List<BaseView> lastViews, string name)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var model = service.DomainToDTO(service.CreateLeague(name));
            service.Save();

            LeagueView view = new LeagueView(model);

            return view;
            
        }
        public static BaseView List(ApplicationContext context, List<BaseView> lastViews)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var view = new LeagueListView(service.GetAll());
            
            return view;
        }
    }
}
