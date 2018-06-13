using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class ReferenceObjectListView : BaseListView
    {
        public ReferenceObjectListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{0,5}. {1,20}";

        public override string[] HeaderStrings => new string[] { "Id", "Name" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            return new List<object>() { model.Id, model.Name };
        }
    }
}
