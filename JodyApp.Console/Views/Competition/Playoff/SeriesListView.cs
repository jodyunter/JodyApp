using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class SeriesListView:BaseListView
    {
        public SeriesListView(ListViewModel model) : base(model) { }

        public override string Formatter => throw new NotImplementedException();

        public override string[] HeaderStrings => throw new NotImplementedException();

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
