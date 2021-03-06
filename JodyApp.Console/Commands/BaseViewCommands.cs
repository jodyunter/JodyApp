﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.Domain;
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
        public BaseViewCommands(ApplicationContext context, string serviceName) { Service = context.ServiceLibraries[serviceName]; }

        public JService Service { get; set; }
        public abstract BaseView GetView(BaseViewModel model);
        public abstract BaseListView GetList(ListViewModel model);
        public abstract Dictionary<string, string> GatherCreateData(ApplicationContext context);
        public abstract BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data);

        public Dictionary<string, Func<ApplicationContext, string, ReferenceObject>> InputDictionary = new Dictionary<string, Func<ApplicationContext, string, ReferenceObject>>();

        public abstract Func<ApplicationContext, string, ReferenceObject> SelectMethod { get; }
        public abstract Action<ApplicationContext> ClearSelectedItem { get; }

        [Command]
        public BaseView Select(ApplicationContext context, string prompt = "Select>")
        {
            if (SelectMethod == null)
            {
                return new ErrorView("Cannot find a select method for this.");
            }

            ClearSelectedItem(context);

            SelectMethod(context, prompt);

            return new MessageView("Selected Item Changed");
        }
        [Command]
        public virtual BaseView View(ApplicationContext context, int? id = null, string prompt = "Select>")
        {
            if (id == null)
                id = SelectMethod(context, prompt).Id;
            
            var model = Service.GetModelById((int)id);

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
        public virtual BaseListView List(ApplicationContext context)
        {
            var view = GetList(Service.GetAll());

            return view;
        }

        //when editing we want to keep the initial view and the edited view.
        //we want to keep editing without having to retype "Blah.Edit"
        //we want to be able to use our pre-processing commands to back out
        [Command]
        public BaseView Edit(ApplicationContext context, int? id = null, string prompt = "Select>")
        {
            var refObj = SelectMethod(context, prompt);

            //if nothing selected, assume we have to go back
            if (refObj == null)
            {
                return context.GetLastView();
            }

            var model = Service.GetModelById((int)refObj.Id);

            var view = GetView(model);

            view.Header = refObj.Name;
            view.EditMode = true;

            context.AddView(view);

            //we should use the "Update <value>" command to update
            return view;
                        
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

        [Command]
        public BaseView Update(ApplicationContext context, int selection = -1)
        {            
            var view = context.GetLastView();
            if (context.CurrentView != null) view = context.CurrentView;
            
            if (view == null) return new MessageView("Must Edit Something Prior to Updating");

            var newView = GetView(view.Model);
            newView.EditMode = true;

            //var selectionInput = Application.ReadFromConsole(context, "Enter Selection>");                                
            //selection = (int)Application.CoerceArgument(typeof(int), selectionInput);

            if (selection >= BaseView.NUMBER_OF_DEFAULT_EDIT_COMMANDS && selection != 0)
            {
                selection -= BaseView.NUMBER_OF_DEFAULT_EDIT_COMMANDS;
                var prompt = newView.EditHeaders[selection];
                context.ClearSelectedItems();
                ReferenceObject objectInput = null;
                if (InputDictionary.ContainsKey(prompt))
                {
                    objectInput = InputDictionary[prompt].Invoke(context, "Select>");
                    newView.UpdateAttribute(prompt, objectInput);
                }
                else
                {
                    var dataInput = Application.ReadFromConsole(context, "Enter New Value>");                    
                    newView.UpdateAttribute(prompt, dataInput);
                }

            }

            //handle save, undo, none here            
            return newView;
        }        
                
        protected int? GetNullableIntFromString(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            return int.Parse(input);
        }
        
        //eventually have a way to exclude objects that were already selected
        public static ReferenceObject GetSelectedObject(ApplicationContext context, string prompt, BaseListView view)
        {
            view.ListWithOptions = true;

            var input = Application.ReadFromConsole(context, prompt, view.GetView());

            if (!(string.IsNullOrEmpty(input)))
            {
                var searchSelection = (int)Application.CoerceArgument(typeof(int), input);

                var viewModel = view.GetBySelection(searchSelection);

                if (viewModel == null) return null;

                var selectedObject = new ReferenceObject(viewModel.Id, viewModel.Name);

                return selectedObject;
            }
            else
                return null;

        }
    }
}
