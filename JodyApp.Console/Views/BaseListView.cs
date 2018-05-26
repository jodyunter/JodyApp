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

        public bool ListWithOptions { get; set; }

        public abstract string Formatter { get; }
        public abstract string[] HeaderStrings { get; }        
        public abstract List<object> GetDataObjectFromModel(BaseViewModel model);

        public BaseListView(ListViewModel model) : base(model) { ListWithOptions = false; }

        public override string GetView()
        {
            var formatter = Formatter;

            var result = "";
            if (!string.IsNullOrEmpty(Header)) result += Header + "\n";
            if (ListWithOptions) formatter = "{" + HeaderStrings.Length + ",5}. " + formatter;

            var headerStrings = HeaderStrings.ToList();

            if (ListWithOptions) headerStrings.Add("O");

            result += string.Format(formatter, headerStrings.ToArray());

            int count = 1;

            if (ListWithOptions) result += string.Format("\n" + "{0,5}. None", 0);            

            ((ListViewModel)Model).Items.ForEach(item =>
            {
                var data = GetDataObjectFromModel(item);
                if (ListWithOptions) data.Add(count);
                result += "\n" + string.Format(formatter, data.ToArray());
                count++;
            });

            return result;
        }


    }
}
