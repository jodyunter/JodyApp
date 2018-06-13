using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class PlayoffView:BaseCompetitionView
    {
        public PlayoffView(BaseViewModel model) : base(model) { }

        public override string GetView()
        {
            var result = base.GetView();

            var m = (PlayoffViewModel)Model;




            return null;
        }
    }
}
