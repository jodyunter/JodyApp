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
        public new ListViewModel Model { get; set; }        

        public bool ListWithOptions { get; set; }

        public abstract string Formatter { get; }
        public abstract string[] HeaderStrings { get; }        
        public abstract List<object> GetDataObjectFromModel(BaseViewModel model);

        public BaseListView(ListViewModel model) : base(model) { ListWithOptions = false; Model = model; }


        //currently the default is 0 is NONE
        public override string GetView()
        {
            var formatter = Formatter;

            var result = "";
            if (!string.IsNullOrEmpty(Header)) result += Header + "\n";
            if (ListWithOptions) formatter = "{" + HeaderStrings.Length + ",5}. " + formatter;

            var headerStrings = HeaderStrings.ToList();

            if (ListWithOptions) headerStrings.Add("Opt");

            result += string.Format(formatter, headerStrings.ToArray());

            int count = 1;

            if (ListWithOptions) result += string.Format("\n" + "{0,5}. None", 0);            

            Model.Items.ForEach(item =>
            {
                var data = GetDataObjectFromModel(item);
                if (ListWithOptions) data.Add(count);
                result += "\n" + string.Format(formatter, data.ToArray());
                count++;
            });

            return result;
        }

        public BaseViewModel GetBySelection(int selectionId)
        {
            if (selectionId == 0)
            {
                return null;
            }

            if (selectionId >= Model.Items.Count)
            {
                AddError("Selection is not a valid option.");
                return null;
            }
            else
            {
                return Model.Items[selectionId-1];
            }


        }
    }
}
