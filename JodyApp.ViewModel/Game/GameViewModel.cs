namespace JodyApp.ViewModel
{
    public class GameViewModel:BaseViewModel
    {
        public ReferenceObject HomeTeam { get; set; }
        public ReferenceObject AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }
        public int DayNumber { get; set; }
        public int GameNumber { get; set; }
        public bool Complete { get; set; }

        public GameViewModel(int? id, ReferenceObject homeTeam, ReferenceObject awayTeam, int homeScore, int awayScore, string competition, int year, int dayNumber, int gameNumber, bool complete)
        {
            Id = id;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Competition = competition;
            Year = year;
            DayNumber = dayNumber;
            GameNumber = gameNumber;
            Complete = complete;
        }
    }
}
