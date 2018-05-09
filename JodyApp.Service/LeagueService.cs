using JodyApp.Domain.Playoffs;
using JodyApp.Database;
using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class LeagueService:BaseService
    {
        SeasonService seasonService;
        PlayoffService playoffService;

        public LeagueService(JodyAppContext db) : base(db) {
            seasonService = new SeasonService(db);
            playoffService = new PlayoffService(db);
        }

        //also good for getting current competition
        public Competition GetNextCompetition(League league)
        {
            if (IsYearDone(league))
            {
                return null;
            }
            else
            {

                //step one find any outstanding competitions
                var referenceComps = db.ReferenceCompetitions.Where(rc => rc.League.Id == league.Id).OrderBy(rc => rc.Order).ToList();

                Competition currentComp = null;
                bool found = false;

                for (int i = 0; i < referenceComps.Count && !found; i++)
                {
                    //does the current year version exist?                
                    //if yes, is it complete?
                    //if yes, move on, we alreayd know at least one is not complete
                    //if no return it
                    //if no return it
                    ReferenceCompetition rc = referenceComps[i];
                    Competition competitionReference = null;

                    if (rc.Season != null)
                    {
                        currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Season.Name).FirstOrDefault();
                        competitionReference = rc.Season;
                    }
                    else if (rc.Playoff != null)
                    {
                        currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Playoff.Name).FirstOrDefault();
                        competitionReference = rc.Playoff;
                    }

                    if (currentComp == null) return CreateCompetition(competitionReference, league.CurrentYear);
                    else if (!currentComp.Complete) return currentComp;

                }

                return currentComp;
            }
        }
            

        public bool IsYearDone(League league)
        {            
            var completeCompetitions = new List<Competition>();

            completeCompetitions.AddRange(db.Seasons.Where(s => s.Year == league.CurrentYear && s.Complete).ToList());
            completeCompetitions.AddRange(db.Playoffs.Where(p => p.Year == league.CurrentYear && p.Complete).ToList());

            return completeCompetitions.Count == league.ReferenceCompetitions.Count;
            

        }
        public Competition CreateCompetition(Competition reference, int year)
        {
            if (reference is Season) return seasonService.CreateNewSeason((Season)reference, year);
            else if (reference is Playoff) return playoffService.CreateNewPlayoff((Playoff)reference, year);

            return null;
        }

        public List<Game> PlayNextGames(Competition competition, Random random)
        {
            return null;
        }


    }
}
