using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class PlayoffListView:BaseListView
    {
        public PlayoffListView(ListViewModel model) : base(model) { }

        public override string Formatter => throw new NotImplementedException();

        public override string[] HeaderStrings => throw new NotImplementedException();

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
