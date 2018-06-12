using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.Views;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Commands
{
    public class PlayoffCommands : BaseViewCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => throw new NotImplementedException();

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public PlayoffCommands() { }
        public PlayoffCommands(ApplicationContext context):base(context, "Playoff") {}


        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new PlayoffListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
