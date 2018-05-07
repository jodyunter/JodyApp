using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Series:DomainObject
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Game> Games { get; set; }

        public Playoff Playoff { get; set; }
        //redundant because of series rule?
        public int Round { get; set; }
        //rule that determines number of games, wins home and away and game specific rules
        //we want total goal series, and best-of series
        public String Name { get; set; }
        //is name redundant since the rule has it?
                        
        virtual public SeriesRule Rule { get; set; }

        public int HomeWins { get; set; }
        public int AwayWins { get; set; }

        public Series() { HomeWins = 0; AwayWins = 0; Games = new List<Game>(); }
        public Series(Playoff playoff, SeriesRule rule, Team homeTeam, int homeWins, Team awayTeam, int awayWins, List<Game> games, string name)
        {
            Playoff = playoff;
            Rule = rule;            
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Games = games;
            Name = name;
            HomeWins = homeWins;
            AwayWins = awayWins;
            Round = rule.Round;
            
        }

        public int GetTeamWins(Team team)
        {
            if (HomeTeam.Id == team.Id) return HomeWins;
            else if (AwayTeam.Id == team.Id) return AwayWins;
            else return -1;
            
        }

        public bool Complete
        {
            get
            {
                switch (Rule.SeriesType)
                {
                    case SeriesRule.TYPE_BEST_OF:
                        if (HomeWins == Rule.GamesNeeded || AwayWins == Rule.GamesNeeded) return true;
                        break;
                }
                return false;
            }
        }

        public Team GetWinner()
        {
            if (Complete)
            {
                switch(Rule.SeriesType)
                {
                    case SeriesRule.TYPE_BEST_OF:
                        return HomeWins == Rule.GamesNeeded ? HomeTeam : AwayTeam;                        
                    default:
                        throw new ApplicationException("In Get Winner For Series: Unrecognized Type");
                }                
            }
            return null;
        }

        public Team GetLoser()
        {
            if (Complete)
            {
                switch (Rule.SeriesType)
                {
                    case SeriesRule.TYPE_BEST_OF:
                        return AwayWins == Rule.GamesNeeded ? HomeTeam : AwayTeam;
                    default:
                        throw new ApplicationException("In Get Loser For Series: Unrecognized Type");
                }
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

            int homeTeamWins = GetTeamWins(HomeTeam);
            int awayTeamWins = GetTeamWins(AwayTeam);

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
            Game game = new Game(null, this, null, null, -1, lastGameNumber, 0, 0, false, 0, false);            
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
     
        public void ProcessGame(Game g)
        {
            Team winner = g.GetWinner();
            if (winner != null)
            {
                if (winner.Id == HomeTeam.Id) HomeWins++;
                else if (winner.Id == AwayTeam.Id) AwayWins++;
            }
        }

        public void SetTeamWinsByGames()
        {
            HomeWins = 0;
            AwayWins = 0;

            Games.ForEach(game =>
            {
                ProcessGame(game);
            });
        }
    }
}
