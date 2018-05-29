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
    public partial class CompetitionService : BaseService
    {        

        SeasonService SeasonService { get; set; }
        PlayoffService PlayoffService { get; set; }
        LeagueService LeagueService { get; set; }

        public CompetitionService():base(){  }

        public CompetitionService(JodyAppContext db, ConfigService configService) : base(db)
        {
            LeagueService = new LeagueService(db);
            SeasonService = new SeasonService(db, LeagueService, configService);
            PlayoffService = new PlayoffService(db, configService);
        }

        public CompetitionService(JodyAppContext db, LeagueService leagueService, SeasonService seasonService, PlayoffService playoffService) : base(db)
        {
            LeagueService = leagueService;
            SeasonService = seasonService;
            PlayoffService = playoffService;
        }

        public CompetitionService(JodyAppContext db):base(db)
        {
            LeagueService = new LeagueService(db);
            SeasonService = new SeasonService(db);
            PlayoffService = new PlayoffService(db);
        }

        public Competition GetNextCompetition(League league)
        {
            if (LeagueService.IsYearDone(league))
            {
                return null;
            }
            else
            {
                //get all the possible competitions for this league
                var referenceComps = league.GetActiveConfigCompetitions();

                Competition currentComp = null;

                for (int i = 0; i < referenceComps.Count; i++)
                {
                    //does the current year version exist?                
                    //if yes, is it complete?
                    //if yes, move on, we alreayd know at least one is not complete
                    //if no return it
                    //if no return it
                    ConfigCompetition rc = referenceComps[i];

                    switch (rc.Type)
                    {
                        case ConfigCompetition.SEASON:
                            currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;
                        case ConfigCompetition.PLAYOFF:
                            currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;

                    }

                    //if there is no competition for this one, create one and return it
                    if (currentComp == null)
                        return CreateCompetition(rc, league.CurrentYear);
                    //if there is a current competition, and it is not complete, return it
                    else if (!currentComp.Complete)
                        return currentComp;

                }

                return currentComp;
            }
        }



        public Competition CreateCompetition(ConfigCompetition reference, int year)
        {
            switch(reference.Type)
            {
                case ConfigCompetition.SEASON:
                    return SeasonService.CreateNewSeason(reference, year);                    
                case ConfigCompetition.PLAYOFF:
                    return PlayoffService.CreateNewPlayoff(reference, year);                    
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
                SeasonService.SortAllDivisions((Season)competition);
            }
        }
    }
}
