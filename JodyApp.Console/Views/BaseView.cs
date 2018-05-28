using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public abstract class BaseView
    { 
        //eventually have an error message class so it can be a warning or not
        public List<string> ErrorMessages { get; set; }
        public bool Error { get; set; }
        public virtual BaseViewModel Model { get; set; }
        public bool EditMode { get; set; }

        public virtual string Header { get; set; }
        public virtual string[] ViewHeaders { get; }
        public virtual object[] ViewObjects { get; }
        public virtual string[] EditHeaders { get; }
        public virtual object[] EditObjects { get; }

        public BaseView(BaseViewModel model)
        {
            Model = model;
            ErrorMessages = new List<string>();
            EditMode = false;
        }        

        public virtual string GetView()
        {

            if (EditMode) return GetEditView(Header, EditHeaders, EditObjects);
            return GetView(Header, ViewHeaders, ViewObjects);
        }

        public string GetView(string header, string[] fieldNames, object[] data)
        {
            var template = "{0,10}: {1,-15}";
            var result = header;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (i > 0) result += "\n";

                result += string.Format(template, fieldNames[i], data[i]);
            }

            return result;

        }

        public string GetEditView(string header, string[] fieldNames, object[] data)
        {
            var template = "{0,5}. {1,10} - {2,-15}";
            var result = header;
            

            for (int i = 0; i <= fieldNames.Length; i++)
            {
                if (i == 0) result = string.Format(template, 0, "No Edit", "");
                else result += "\n" + string.Format(template, i, fieldNames[i-1], data[i-1]);
            }

            return result;
        }

        public void AddError(string message)
        {
            if (ErrorMessages == null) ErrorMessages = new List<string>();

            ErrorMessages.Add(message);
            Error = true;
        }

        public virtual void UpdateAttribute(int selection, string value)
        {
            return;
        }
    }
}
