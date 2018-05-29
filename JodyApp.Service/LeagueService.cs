using JodyApp.Domain.Playoffs;
using JodyApp.Database;
using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public partial class LeagueService:BaseService
    {

        public static BaseService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LeagueService();
                }
                return instance;
            }
        }
        public LeagueService() : base() { }
        public LeagueService(JodyAppContext db) : base(db)
        {            
        }
        //also good for getting current competition
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

        public League GetById(int? id)
        {
            if (id == null) return null;

            return db.Leagues.Where(l => l.Id == (int)id).FirstOrDefault();
        }


        
    }
}
