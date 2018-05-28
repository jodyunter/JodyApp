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

        public static BaseView View(ApplicationContext context, int id)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var model = service.GetById(id);

            var view = new LeagueView(model);

            return view;
        }
        public static BaseView Create(ApplicationContext context, string name)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var model = service.DomainToDTO(service.CreateLeague(name));
            service.Save();

            var view = new LeagueView(model);

            return view;
            
        }
        public static BaseView List(ApplicationContext context)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var view = new LeagueListView(service.GetAll());
            
            return view;
        }

        public static BaseView Edit(ApplicationContext context, int id)
        {
            var service = new LeagueService(JodyAppContext.Instance);
            var model = service.GetById(id);

            var view = new LeagueView(model);
            view.EditMode = true;
            return view;
        }
    }
}
