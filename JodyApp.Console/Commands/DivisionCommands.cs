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

        public static BaseView View(List<BaseView> lastViews, int id)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            ConfigDivisionViewModel model = service.GetDivisionModelById(id);

            DivisionView view = new DivisionView(model);

            return view;
        }

        public static BaseView List(List<BaseView> lastViews)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            DivisionListView view = new DivisionListView(service.GetAllDivisions());

            return view;
        }
    }
}
