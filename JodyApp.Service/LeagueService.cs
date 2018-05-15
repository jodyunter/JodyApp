﻿using JodyApp.Domain.Playoffs;
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
                var referenceComps = league.ReferenceCompetitions.OrderBy(m => m.Order).ToList();

                Competition currentComp = null;                
                                
                for (int i = 0; i < referenceComps.Count; i++)
                {
                    //does the current year version exist?                
                    //if yes, is it complete?
                    //if yes, move on, we alreayd know at least one is not complete
                    //if no return it
                    //if no return it
                    ReferenceCompetition rc = referenceComps[i];                    
                    
                    switch(rc.Competition.Type)
                    { 
                        case ConfigCompetition.SEASON:
                            currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Competition.Name).FirstOrDefault();                            
                            break;
                        case ConfigCompetition.PLAYOFF:
                            currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Competition.Name).FirstOrDefault();                            
                            break;

                    }
                    
                    //if there is no competition for this one, create one and return it
                    if (currentComp == null)
                        return competitionService.CreateCompetition(rc.Competition, league.CurrentYear);
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

            var completeCompetitions = new List<Competition>();

            completeCompetitions.AddRange(db.Seasons.Where(s => s.Year == league.CurrentYear && s.Complete).ToList());
            completeCompetitions.AddRange(db.Playoffs.Where(p => p.Year == league.CurrentYear && p.Complete).ToList());

            return completeCompetitions.Count == league.ReferenceCompetitions.Count;
            
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
