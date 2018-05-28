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

        public BaseView(BaseViewModel model)
        {
            Model = model;
            ErrorMessages = new List<string>();
        }
        public abstract string GetView();

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

        public void AddError(string message)
        {
            if (ErrorMessages == null) ErrorMessages = new List<string>();

            ErrorMessages.Add(message);
            Error = true;
        }
    }
}
