using JodyApp.ConsoleApp.Commands;
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

        public override string[] ViewHeaders => new string[] { "Id", "Name", "Skill", "League", "Division", "First Year", "Last Year" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (ConfigTeamViewModel)Model;
                return new object[] { m.Id, m.Name, m.Skill, m.League, m.Division, m.FirstYear, m.LastYear };
            }
        }

        public override string[] EditHeaders => new string[] { "Name", "Skill", "League", "Division", "First Year", "Last Year" };
        public override object[] EditObjects
        {
            get
            {
                var m = (ConfigTeamViewModel)Model;
                return new object[] { m.Name, m.Skill, m.League, m.Division, m.FirstYear, m.LastYear };
            }
        }
        public TeamView(BaseViewModel model):base(model)
        {         
            
        }

        public override void UpdateAttribute(string headerName, object value)
        {
            var m = (ConfigTeamViewModel)Model;

            switch (headerName)
            {
                case "Name":
                    m.Name =(string)value;
                    break;
                case "Skill":
                    m.Skill = int.Parse((string)value);
                    break;
                case "First Year":
                    if (string.IsNullOrEmpty((string)value)) m.FirstYear = null;
                    else m.FirstYear = int.Parse((string)value);
                    break;
                case "Last Year":
                    if (string.IsNullOrEmpty((string)value)) m.LastYear = null;
                    else m.LastYear = int.Parse((string)value);
                    break;
                case "League":
                    m.League = (ReferenceObject)value;
                    break;
                case "Division":
                    m.Division = (ReferenceObject)value;
                    break;

            }

            return;
        }
    }
}
