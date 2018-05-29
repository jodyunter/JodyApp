using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service;
using JodyApp.ViewModel;


namespace JodyApp.ConsoleApp.Commands
{
    public abstract class BaseViewCommands
    {
        public BaseViewCommands() { }
        
        public BaseService Service { get; set; }
        public abstract BaseView GetView(BaseViewModel model);
        public abstract BaseListView GetList(ListViewModel model);
        public abstract Dictionary<string, string> GatherCreateData(ApplicationContext context);
        public abstract BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data);

        public BaseView View(ApplicationContext context, int id)
        {            
            var model = Service.GetModelById(id);

            var view = GetView(model);

            return view;
        }
        public BaseView Create(ApplicationContext context)
        {
            var data = GatherCreateData(context);

            var model = ConstructViewModelFromData(data);

            //save should be a seperate step
//            Service.Save();

            var view = GetView(model);

            view.EditMode = true;

            return view;

        }
        public BaseListView List(ApplicationContext context)
        {
            var view = GetList(Service.GetAll());

            return view;
        }

        public BaseView Edit(ApplicationContext context, int id)
        {            
            var model = Service.GetModelById(id);

            var view = GetView(model);

            view.EditMode = true;
            return view;
        }

        public BaseView Update(ApplicationContext context, int selection, string newData = "None")
        {
            var view = context.GetLastView();

            int numberOfDefaultCommands = BaseView.NUMBER_OF_DEFAULT_EDIT_COMMANDS;

            if (selection == 0)
            {
                return new MessageView("Nothing chosen to edit");
            }
            else if (selection == 1)
            {
                Service.Save(view.Model);
            }
            else if (selection == 2)
            {
                IOMethods.WriteToConsole("We should be undoing here");
            }
            else if (newData.Equals("None"))
            {
                return new MessageView("Nothing chosen to edit");
            }
            else
                view.UpdateAttribute(view.EditHeaders[selection - numberOfDefaultCommands], newData);

            return view;
        }

    }
}
