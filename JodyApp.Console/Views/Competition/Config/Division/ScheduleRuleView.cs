using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleRuleView : BaseView
    {
        public override string[] ViewHeaders => new string[] { "Id", "Name", "Home Type", "Home Team", "Home Div",
            "Away Type", "Away Team", "Away Div", "Home&Away", "Rounds", "Div Level", "Order",
            "Competition", "Reverse"};

        public override object[] ViewObjects
        {
            get
            {
                var m = (ScheduleRuleViewModel)Model;
                return new object[]
                {
                    m.Id, m.Name, m.HomeType,
                    m.HomeTeam != null ? m.HomeTeam.Name : "None",
                    m.HomeDivision != null ? m.HomeDivision.Name : "None",
                    m.AwayType,
                    m.AwayTeam != null ? m.AwayTeam.Name: "None",
                    m.AwayDivision != null ? m.AwayDivision.Name: "None",
                    m.PlayHomeAndAway,
                    m.Rounds,
                    m.DivisionLevel,
                    m.Order,
                    m.Competition != null ? m.Competition.Name : "None",
                    m.Reverse
                };
            }
        }

        public override string[] EditHeaders => new string[] { "Name", "Home Type", "Home Team", "Home Div",
            "Away Type", "Away Team", "Away Div", "Home&Away", "Rounds", "Div Level", "Order",
            "Competition", "Reverse"};
        public override object[] EditObjects
        {
            get
            {
                var m = (ScheduleRuleViewModel)Model;
                return new object[]
                {
                    m.Name, m.HomeType,
                    m.HomeTeam != null ? m.HomeTeam.Name : "None",
                    m.HomeDivision != null ? m.HomeDivision.Name : "None",
                    m.AwayType,
                    m.AwayTeam != null ? m.AwayTeam.Name: "None",
                    m.AwayDivision != null ? m.AwayDivision.Name: "None",
                    m.PlayHomeAndAway,
                    m.Rounds,
                    m.DivisionLevel,
                    m.Order,
                    m.Competition != null ? m.Competition.Name : "None",
                    m.Reverse
                };

            }
        }


        public ScheduleRuleView(BaseViewModel model) : base(model) { }
    }
}
