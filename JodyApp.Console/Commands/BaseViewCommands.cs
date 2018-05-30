﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service;
using JodyApp.ViewModel;


namespace JodyApp.ConsoleApp.Commands
{
    public abstract class BaseViewCommands
    {
        public const int SAVE = 1;
        public const int NONE= 0;
        public const int UNDO = 2;
        public BaseViewCommands() { }

        public BaseService Service { get; set; }
        public abstract BaseView GetView(BaseViewModel model);
        public abstract BaseListView GetList(ListViewModel model);
        public abstract Dictionary<string, string> GatherCreateData(ApplicationContext context);
        public abstract BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data);

        public Dictionary<string, Func<ApplicationContext, ReferenceObject>> InputDictionary = new Dictionary<string, Func<ApplicationContext, ReferenceObject>>();

        public abstract Func<ApplicationContext, ReferenceObject> SelectMethod { get; }

        [Command]
        public BaseView View(ApplicationContext context, int id)
        {            
            var model = Service.GetModelById(id);

            var view = GetView(model);
            
            return view;
        }

        [Command]
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

        [Command]
        public BaseListView List(ApplicationContext context)
        {
            var view = GetList(Service.GetAll());

            return view;
        }

        [Command]
        public BaseView Edit(ApplicationContext context, int? id = null)
        {
            var refObj = SelectMethod(context);

            var model = Service.GetModelById((int)refObj.Id);

            var view = GetView(model);

            view.Header = refObj.Name;
            view.EditMode = true;

            context.AddView(view);

            IOMethods.WriteToConsole(view);

            return Update(context);
        }

        //saves the most recent view
        [Command]
        public BaseView Save(ApplicationContext context)
        {
            var lastView = context.GetLastView();            
            Service.Save(context.GetLastView().Model);

            return GetView(context.GetLastView().Model);
        }

        [Command]
        public BaseView Undo(ApplicationContext context)
        {
            context.RemoveLastView();
            return context.GetLastView();
        }

        public BaseView Update(ApplicationContext context)
        {
            var selection = -1;
            var view = context.GetLastView();
            if (context.CurrentView != null) view = context.CurrentView;
            
            if (view == null) return new MessageView("Must Edit Something Prior to Updating");

            var newView = GetView(view.Model);
            newView.EditMode = true;
            
            var selectionInput = IOMethods.ReadFromConsole(context, "Enter Selection>");
            selection = (int)Program.CoerceArgument(typeof(int), selectionInput);

            while (selection >= BaseView.NUMBER_OF_DEFAULT_EDIT_COMMANDS || selection < 0)
            {
                selection -= BaseView.NUMBER_OF_DEFAULT_EDIT_COMMANDS;
                var prompt = newView.EditHeaders[selection];

                ReferenceObject objectInput = null;
                if (InputDictionary.ContainsKey(prompt))
                {
                    objectInput = InputDictionary[prompt].Invoke(context);
                    newView.UpdateAttribute(prompt, objectInput);
                }
                else
                {
                    var dataInput = IOMethods.ReadFromConsole(context, "Enter New Value>");

                    newView.UpdateAttribute(prompt, dataInput);
                }

                selectionInput = IOMethods.ReadFromConsole(context, "Enter Selection>");
                selection = (int)Program.CoerceArgument(typeof(int), selectionInput);

            }

            //handle save, undo, none here            
            return newView;
        }        
                
        protected int? GetNullableIntFromString(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            return int.Parse(input);
        }
        
        public static ReferenceObject GetSelectedObject(ApplicationContext context, string prompt, BaseListView view)
        {
            view.ListWithOptions = true;

            var input = IOMethods.ReadFromConsole(context, prompt, view.GetView());

            var searchSelection = (int)Program.CoerceArgument(typeof(int), input);

            var viewModel = view.GetBySelection(searchSelection);

            var selectedObject = new ReferenceObject(viewModel.Id, viewModel.Name);

            return selectedObject;
        }
    }
}
