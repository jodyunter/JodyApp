using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public abstract class BaseListView : BaseView
    {
        public abstract Func<string> GetHeaderStringForSingleEntity { get; }
        public abstract Func<BaseViewModel, string> GetDisplayStringNoHeaderSingleEntity { get; }

        public override string GetDisplayString()
        {
            var vm = (BaseListViewModel)ViewModel;
            string result = vm.Header;

            result += "\n" + GetHeaderStringForSingleEntity();

            vm.BaseViewModels.ForEach(league =>
            {
                result += "\n" + GetDisplayStringNoHeaderSingleEntity(league);
            });

            return result;
        }
    }
}
