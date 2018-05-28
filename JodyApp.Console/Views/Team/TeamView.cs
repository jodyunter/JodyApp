using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class TeamView:BaseView
    {

        public override string[] ViewHeaders => new string[] { "Id", "Name", "Skill", "League", "Division" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (ConfigTeamViewModel)Model;
                return new object[] { m.Id, m.Name, m.Skill, m.League, m.Division };
            }
        }

        public override string[] EditHeaders => new string[] { "Name", "Skill" };
        public override object[] EditObjects
        {
            get
            {
                var m = (ConfigTeamViewModel)Model;
                return new object[] { m.Name, m.Skill };
            }
        }
        public TeamView(ConfigTeamViewModel model):base(model)
        {         
        }

        public override void UpdateAttribute(int selection, string value)
        {
            var m = (ConfigTeamViewModel)Model;

            switch (selection)
            {
                case 0:
                    break;
                case 1:
                    m.Name = value;
                    break;
                case 2:
                    m.Skill = int.Parse(value);
                    break;
            }

            return;
        }
    }
}
