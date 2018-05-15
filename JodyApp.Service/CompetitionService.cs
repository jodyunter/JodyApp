using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Playoffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class CompetitionService : BaseService
    {
        SeasonService seasonService = new SeasonService();
        PlayoffService playoffService = new PlayoffService();

        public CompetitionService():base(){ Initialize(null); }
        public CompetitionService(JodyAppContext db) : base(db) { Initialize(db); }

        public override void Initialize(JodyAppContext db)
        {
            seasonService.db = db;
            seasonService.Initialize(db);
            playoffService.db = db;
            playoffService.Initialize(db);
        }

        public Competition CreateCompetition(ConfigCompetition reference, int year)
        {
            switch(reference.Type)
            {
                case ConfigCompetition.SEASON:
                    return seasonService.CreateNewSeason(reference, year);                    
                case ConfigCompetition.PLAYOFF:
                    return playoffService.CreateNewPlayoff(reference, year);                    
            }
            return null;
        }
        
        public List<Game> GetNextGames(Competition competition)
        {
            //need to get next game number
            return competition.GetNextGames(0);
        }

        public void PlayGames(List<Game> games, Competition competition, Random random)
        {
            competition.PlayGames(games, random);
            if (competition is Season)
            {
                seasonService.SortAllDivisions((Season)competition);
            }
        }
    }
}
