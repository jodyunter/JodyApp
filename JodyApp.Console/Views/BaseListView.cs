using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public abstract class BaseListView:BaseView
    {
        public string Header { get; set; }

        public abstract string Formatter { get; }
        public abstract string[] HeaderStrings { get; }        
        public abstract List<object> GetRowFromModel(BaseViewModel model);

        public abstract ListViewModel Model { get; set; }

        public override string GetView()
        {
            return GetView(false);
        }
        public string GetView(bool listWithOptions)
        {
            var formatter = Formatter;

            var result = "";
            if (!string.IsNullOrEmpty(Header)) result += Header + "\n";
            if (listWithOptions) formatter = "{" + HeaderStrings.Length + "5}. " + formatter;

            result += string.Format(formatter, HeaderStrings);

            int count = 1;

            if (listWithOptions) result += string.Format("{0,5}. None", 0);            

            Model.Items.ForEach(item =>
            {
                var data = GetRowFromModel(item);
                if (listWithOptions) data.Add(count);
                result += "\n" + string.Format(formatter, data.ToArray());
                count++;
            });

            return result;
        }


    }
}
