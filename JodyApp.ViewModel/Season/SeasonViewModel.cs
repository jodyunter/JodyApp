using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeasonViewModel:BaseViewModel
    {
        public int Id { get; set; }
        public string League { get; set; }
        public string Name { get; set; }  
        public int Year { get; set; }  
        public bool Started { get; set; }
        public bool Complete { get; set; }
        public int StartingDay { get; set; }
        
        public List<StandingsRecordViewModel> TeamData { get; set; }

        public SeasonViewModel(int id, string league, string name, int year, bool started, bool complete, int startingDay, List<StandingsRecordViewModel> teamData)
        {
            Id = id;
            League = league;
            Name = name;
            Year = year;
            Started = started;
            Complete = complete;
            StartingDay = startingDay;
            TeamData = teamData;
        }

        public List<StandingsRecordViewModel> GetByDivision(string divisionName)
        {
            return TeamData.Where(td => td.DivisionName == divisionName).OrderBy(td => td.Rank).ToList();
        }

    }

    public class StandingsViewModel:ListViewModel
    {
        public StandingsViewModel(List<BaseViewModel> items) : base(items) { }
    }

    public class StandingsRecordViewModel:BaseViewModel
    {
        public int Rank { get; set; }
        public string LeagueName { get; set; }
        public string DivisionName { get; set; }
        public int TeamName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int Points { get; set; }
        public int GamesPlayed { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        public StandingsRecordViewModel(int rank, string leagueName, string divisionName, int teamName, int wins, int loses, int ties, int points, int gamesPlayed, int goalsFor, int goalsAgainst, int goalDifference)
        {
            Rank = rank;
            LeagueName = leagueName;
            DivisionName = divisionName;
            TeamName = teamName;
            Wins = wins;
            Loses = loses;
            Ties = ties;
            Points = points;
            GamesPlayed = gamesPlayed;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            GoalDifference = goalDifference;
        }
    }
}
