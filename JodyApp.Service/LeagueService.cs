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
        public LeagueService(JodyAppContext db) : base(db) { }

        //also good for getting current competition
        public Competition GetNextCompetition(League league)
        {
            var referenceComps = db.ReferenceCompetitions
                                .Include("Season")
                                .Include("Playoff")
                                .Where(rc => rc.League.Id == league.Id).OrderBy(rc => rc.Order).ToList();

            Competition currentComp = null;

            bool found = false;
            referenceComps.ForEach(rc =>
            {
                if (rc.Season != null && !found)
                {
                    currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Season.Name && !s.Complete).FirstOrDefault();
                    if (currentComp != null) found = true;
                }
                else if (rc.Playoff != null  && !found)
                {
                    currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Playoff.Name && !s.Complete).FirstOrDefault();
                    if (currentComp != null)  found = true;

                }
            });

            return currentComp;
        }

        public List<Game> PlayNextGames(Competition competition, Random random)
        {
            return null;
        }

        public bool IsCurrentYearComplete(League league)
        {
            return GetNextCompetition(league) == null;
        }

    }
}
