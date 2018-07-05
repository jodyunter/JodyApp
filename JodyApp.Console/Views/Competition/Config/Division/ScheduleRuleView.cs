using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleRuleView : BaseView
    {
        public ScheduleRuleView(BaseViewModel model) : base(model) { }

        public override string[] ViewHeaders => new string[]{ INPUT_ID, INPUT_NAME, INPUT_LEAGUE, INPUT_HOMETYPE, INPUT_HOMETEAM, INPUT_HOMEDIVISION,
            INPUT_AWAYTYPE, INPUT_AWAYTEAM, INPUT_AWAYDIVISION, INPUT_HOMEANDAWAY, INPUT_ROUNDS, INPUT_DIVISIONLEVEL, INPUT_ORDER,
            INPUT_COMPETITION, INPUT_REVERSE, INPUT_FIRSTYEAR, INPUT_LASTYEAR };

        public override object[] ViewObjects
        {
            get
            {
                var m = (ScheduleRuleViewModel)Model;
                return new object[]
                {
                    m.Id, m.Name,
                    m.League != null ? m.League.Name : LABEL_NONE,
                    m.HomeType,
                    m.HomeTeam != null ? m.HomeTeam.Name : LABEL_NONE,
                    m.HomeDivision != null ? m.HomeDivision.Name : LABEL_NONE,
                    m.AwayType,
                    m.AwayTeam != null ? m.AwayTeam.Name: LABEL_NONE,
                    m.AwayDivision != null ? m.AwayDivision.Name: LABEL_NONE,
                    m.PlayHomeAndAway,
                    m.Rounds,
                    m.DivisionLevel,
                    m.Order,
                    m.Competition != null ? m.Competition.Name : LABEL_NONE,
                    m.Reverse,
                    m.FirstYear,
                    m.LastYear
                };
            }
        }

        public override string[] EditHeaders => new string[] { INPUT_NAME, INPUT_LEAGUE, INPUT_HOMETYPE, INPUT_HOMETEAM, INPUT_HOMEDIVISION,
            INPUT_AWAYTYPE, INPUT_AWAYTEAM, INPUT_AWAYDIVISION, INPUT_HOMEANDAWAY, INPUT_ROUNDS, INPUT_DIVISIONLEVEL, INPUT_ORDER,
            INPUT_COMPETITION, INPUT_REVERSE, INPUT_FIRSTYEAR, INPUT_LASTYEAR };
        public override object[] EditObjects
        {
            get
            {
                var m = (ScheduleRuleViewModel)Model;
                return new object[]
                {
                    m.Name,
                    m.League != null ? m.League.Name : LABEL_NONE,
                    m.HomeType,
                    m.HomeTeam != null ? m.HomeTeam.Name : LABEL_NONE,
                    m.HomeDivision != null ? m.HomeDivision.Name : LABEL_NONE,
                    m.AwayType,
                    m.AwayTeam != null ? m.AwayTeam.Name: LABEL_NONE,
                    m.AwayDivision != null ? m.AwayDivision.Name: LABEL_NONE,
                    m.PlayHomeAndAway,
                    m.Rounds,
                    m.DivisionLevel,
                    m.Order,
                    m.Competition != null ? m.Competition.Name : LABEL_NONE,
                    m.Reverse    ,
                    m.FirstYear,
                    m.LastYear
                };

            }
        }


        public override void UpdateAttribute(string headerName, object value)
        {
            var m = (ScheduleRuleViewModel)Model;

            switch (headerName)
            {
                case INPUT_NAME:
                    m.Name = GetString(value);
                    break;
                case INPUT_LEAGUE:
                    m.League = GetRefObj(value);
                    break;
                case INPUT_HOMETYPE:
                    m.HomeType = GetString(value);
                    break;
                case INPUT_HOMETEAM:
                    m.HomeTeam = GetRefObj(value);
                    break;
                case INPUT_HOMEDIVISION:
                    m.HomeDivision = GetRefObj(value);
                    break;
                case INPUT_AWAYTYPE:
                    m.AwayType = GetString(value);
                    break;
                case INPUT_AWAYTEAM:
                    m.AwayTeam = GetRefObj(value);
                    break;
                case INPUT_AWAYDIVISION:
                    m.AwayDivision = GetRefObj(value);
                    break;
                case INPUT_HOMEANDAWAY:
                    m.PlayHomeAndAway = GetBool(value);
                    break;
                case INPUT_ROUNDS:
                    m.Rounds = GetInt(value);
                    break;
                case INPUT_DIVISIONLEVEL:
                    m.DivisionLevel = GetInt(value);
                    break;
                case INPUT_ORDER:
                    m.Order = GetInt(value);
                    break;
                case INPUT_COMPETITION:
                    m.Competition = GetRefObj(value);
                    break;
                case INPUT_REVERSE:
                    m.Reverse = GetBool(value);
                    break;
                case INPUT_FIRSTYEAR:
                    m.FirstYear = GetNullInt(value);
                    break;
                case INPUT_LASTYEAR:
                    m.LastYear = GetNullInt(value);
                    break;
            }

            return;
        }


    }
}
