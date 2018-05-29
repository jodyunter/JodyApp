using JodyApp.ConsoleApp.Views;
using JodyApp.ConsoleApp.Views.Division;
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
    public class DivisionCommands
    {
        public DivisionCommands() : base() { }
        public BaseView View(ApplicationContext context, int id)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var model = service.GetDivisionModelById(id);

            var view = new DivisionView(model);

            return view;
        }

        public BaseView List(ApplicationContext context)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new DivisionListView(service.GetAllDivisions());

            return view;
        }
    }
}
