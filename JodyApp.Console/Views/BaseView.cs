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
        public BaseViewModel Model { get; set; }

        public BaseView(BaseViewModel model)
        {
            this.Model = model;
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
    }
}
