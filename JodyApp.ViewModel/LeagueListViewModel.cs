using JodyApp.Domain;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueListViewModel:BaseListViewModel
    {
        LeagueService leagueService;
        public override string Header { get { return "List of Leagues"; } }
        //public List<LeagueViewModel> Leagues { get; set; }
        
        public LeagueListViewModel():base() { leagueService = new LeagueService(db); }

        public override List<DomainObject> GetDomainObjects()
        {
            return leagueService.GetAll().ToList<DomainObject>();
        }

        public override BaseViewModel CreateModel(DomainObject domainObject)
        {
            var league = (League)domainObject;

            var model = new LeagueViewModel();
            model.Id = league.Id;
            model.LeagueName = league.Name;
            model.CurrentYear = league.CurrentYear;
            var nextCompetition = leagueService.GetNextCompetition(league);
            if (nextCompetition == null) model.IsComplete = true;
            else
            {
                model.IsComplete = false;
                model.CurrentCompetition = nextCompetition.Name;
            }

            return model;
        }
    }
}
