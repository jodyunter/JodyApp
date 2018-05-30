using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class LeagueView : BaseView
    {
        public override string[] ViewHeaders => new string[] { "Id", "Name", "Year" };
        public override string[] EditHeaders => new string[] { "Name" };

        public override object[] ViewObjects
        {
            get
            {
                var m = (LeagueViewModel)Model;
                return new object[] { m.Id, m.Name, m.CurrentYear };
            }
        }
        public override object[] EditObjects => new object[] { ((LeagueViewModel)Model).Name };

        public LeagueView(BaseViewModel model) : base(model)
        {
        }

        public override void UpdateAttribute(string headerName, object value)
        {
            var m = (LeagueViewModel)Model;

            switch (headerName)
            {
                case "Name":
                    m.Name = (string)value;
                    break;
            }

            return;
        }
    }



}
