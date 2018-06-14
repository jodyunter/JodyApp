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
using System.Data.Entity;

namespace JodyApp.Service
{
    public class LeagueService:BaseService<League>
    {
        public override DbSet<League> Entities => db.Leagues;

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

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var league = (League)obj;

            var viewModel = new LeagueViewModel(league.Id, league.Name, league.CurrentYear);


            return viewModel;

        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            var m = (LeagueViewModel)model;
            var league = (League)GetById(m.Id);

            if (league == null)
            {
                //new entity
                league = new League(m.Name);
                db.Leagues.Add(league);
            }
            else
            {
                league.Name = m.Name;
                league.CurrentYear = m.CurrentYear;
                db.Entry(league).CurrentValues.SetValues(league);
                //what about reference comps? Different scren!
            }

            db.SaveChanges();
            return DomainToDTO(league);
        }

        public BaseViewModel GetLeagueWinners(int leagueId)
        {
            return null;
        }

    }
}
