using JodyApp.Domain.Playoffs;
using JodyApp.Database;
using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class LeagueService:BaseService
    {
        CompetitionService competitionService = new CompetitionService();

        public override void Initialize(JodyAppContext db)
        {
            competitionService.db = db;
            competitionService.Initialize(db);
        }

        public LeagueService() : base() { }
        public LeagueService(JodyAppContext db) : base(db) { }
        public LeagueService(string ConnectionString) : base(ConnectionString) { }

        //also good for getting current competition
        public Competition GetNextCompetition(League league)
        {
            if (IsYearDone(league))
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
                    
                    switch(rc.Type)
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
                        return competitionService.CreateCompetition(rc, league.CurrentYear);
                    //if there is a current competition, and it is not complete, return it
                    else if (!currentComp.Complete)
                        return currentComp;

                }

                return currentComp;
            }
        }
            
        public League GetById(int id)
        {
            return db.Leagues.Where(l => l.Id == id).FirstOrDefault();
        }
        public League GetByName(string name)
        {
            return db.Leagues.Where(l => l.Name == name).FirstOrDefault();
        }

        public bool IsYearDone(League league)
        {
            if (league.CurrentYear == 0) return true;

            bool done = true;

            league.GetActiveConfigCompetitions().ForEach(rc =>
            {
                Competition comp = null;
                switch (rc.Type)
                {
                    case ConfigCompetition.SEASON:
                        comp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                        break;
                    case ConfigCompetition.PLAYOFF:
                        comp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                        break;
                }

                done = done && comp != null && comp.IsComplete();
            }
            );

            return done;
            
        }

        public League CreateLeague(string name)
        {
            var league = new League(name);

            db.Leagues.Add(league);

            return league;
        }
        public List<League> GetAll()
        {
            return db.Leagues.ToList();
        }




    }
}
