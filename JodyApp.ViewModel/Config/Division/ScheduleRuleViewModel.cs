﻿namespace JodyApp.ViewModel
{
    public class ScheduleRuleViewModel:BaseViewModel
    {
        public const string TYPE_DIVISION = "By Division";
        public const string TYPE_TEAM = "By Team";
        public const string TYPE_DIVISION_LEVEL = "By Division Level";
        public const string TYPE_NONE = "None";

        public ReferenceObject League { get; set; }

        public string HomeType { get; set; }
        public ReferenceObject HomeTeam { get; set; }
        public ReferenceObject HomeDivision { get; set; }

        public string AwayType { get; set; }
        public ReferenceObject AwayTeam { get; set; }
        public ReferenceObject AwayDivision { get; set; }

        public bool PlayHomeAndAway { get; set; }
        
        public int Rounds { get; set; }

        public int DivisionLevel { get; set; }

        public int Order { get; set; }

        public ReferenceObject Competition { get; set; }
           
        public bool Reverse { get; set; } //reverse default order

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ScheduleRuleViewModel(int? id, ReferenceObject league, 
            string homeType, ReferenceObject homeTeam, ReferenceObject homeDivision, 
            string awayType, ReferenceObject awayTeam, ReferenceObject awayDivision,
            bool playHomeAndAway, int rounds, int divisionLevel, int order, 
            ReferenceObject competition, bool reverse, int? firstYear, int? lastYear)
        {
            Id = id;
            League = league;
            HomeType = homeType;
            HomeTeam = homeTeam;
            HomeDivision = homeDivision;
            AwayType = awayType;
            AwayTeam = awayTeam;
            AwayDivision = awayDivision;
            PlayHomeAndAway = playHomeAndAway;
            Rounds = rounds;
            DivisionLevel = divisionLevel;
            Order = order;
            Competition = competition;
            Reverse = reverse;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
