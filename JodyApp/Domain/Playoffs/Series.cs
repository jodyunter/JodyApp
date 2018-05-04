using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class Series:DomainObject
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Game> Games { get; set; }

        public Playoff Playoff { get; set; }
        public int Round { get; set; }
        //rule that determines number of games, wins home and away and game specific rules
        //we want total goal series, and best-of series
        public String Name { get; set; }
        //is name redundant since the rule has it?
        
        [Required]
        public SeriesRule Rule { get; set; }

        public Series() { }
        public Series(Playoff playoff, SeriesRule rule, Team homeTeam, Team awayTeam, List<Game> games, string name)
        {
            Playoff = playoff;
            Rule = rule;            
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Games = games;
            Name = name;
        }

        public int TeamWins(Team team)
        {
            return Games.Where(g => g.GetWinner() != null && g.GetWinner().Id == team.Id).ToList().Count;
            
        }

        public bool Complete
        {
            get
            {
                switch (Rule.SeriesType)
                {
                    case SeriesRule.TYPE_BEST_OF:
                        int homeWins = TeamWins(HomeTeam);
                        int awayWins = TeamWins(AwayTeam);
                        if (homeWins == Rule.GamesNeeded || awayWins == Rule.GamesNeeded) return true;
                        break;
                }
                return false;
            }
        }

        public Team GetWinner()
        {
            if (Complete)
            {
                return TeamWins(HomeTeam) == Rule.GamesNeeded ? HomeTeam : AwayTeam;
            }

            return null;
        }

        public Team GetLoser()
        {
            if (Complete)
            {
                return TeamWins(HomeTeam) == Rule.GamesNeeded ? AwayTeam : HomeTeam;
            }

            return null;
        }
        //these should move to a playoff scheduler eventually, because we don't have game numbers
        public int CreateNeededGames(int lastGameNumber, List<Game> games)
        {
            switch(Rule.SeriesType)
            {
                case SeriesRule.TYPE_BEST_OF:
                    return CreateNeededGamesForBestOf(lastGameNumber, games);                    
            }

            return lastGameNumber;
        }
        public int CreateNeededGamesForBestOf(int lastGameNumber, List<Game> games)
        {
            int currentGamesCreated = Games.Count();

            int homeTeamWins = TeamWins(HomeTeam);
            int awayTeamWins = TeamWins(AwayTeam);

            int mostWins = homeTeamWins >= awayTeamWins ? homeTeamWins : awayTeamWins;

            int winsNeeded = Rule.GamesNeeded - mostWins;

            int neededGames = winsNeeded + homeTeamWins + awayTeamWins - currentGamesCreated;
            
            for (int i = 0; i < neededGames; i++)
            {
                lastGameNumber = CreateGameForSeries(lastGameNumber, games);
            }

            return lastGameNumber;
        }


        //to do need to create constructor game
        public int CreateGameForSeries(int lastGameNumber, List<Game> games)
        {
            //todo determine home and away teams here
            Game game = new Game(null, Playoff, null, null, -1, lastGameNumber, 0, 0, false, 0, false);
            games.Add(game);
            Games.Add(game);
            SetHomeTeamForGame(game);
            return lastGameNumber;
        }

        public int[] GetHomeGamesList()
        {
            if (String.IsNullOrEmpty(Rule.HomeGames))
            {
                return new int[] { };
            }
            return Rule.HomeGames.Split(',').Select(a => int.Parse(a)).ToArray();
        }

        //if no games are defined, then EVEN games go to AWAY series team and ODD games go to HOME series team (to give that team the first and final game)
        public void SetHomeTeamForGame(Game game)
        {

            int[] gameList = GetHomeGamesList();

            Team homeTeam = HomeTeam;
            Team awayTeam = AwayTeam;

            if (gameList.Length == 0 || Games.Count > gameList.Length)
            {
                if (Games.Count % 2 == 0)
                {
                    homeTeam = AwayTeam;
                    awayTeam = HomeTeam;
                }
            }
            else if (gameList[Games.Count - 1] == 0)
            {
                homeTeam = AwayTeam;
                awayTeam = HomeTeam;
            }

            game.HomeTeam = homeTeam;
            game.AwayTeam = awayTeam;
        }
        
    }
}
