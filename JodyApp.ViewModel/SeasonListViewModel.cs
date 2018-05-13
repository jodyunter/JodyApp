using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Service;

namespace JodyApp.ViewModel
{
    public class SeasonListViewModel : BaseListViewModel
    {
        SeasonService seasonService;
        public int? LeagueId { get; set; }
        
        public override string Header { get { return "List of Seasons"; } }

        public SeasonListViewModel():base() { seasonService = new SeasonService(db); }

        public override BaseViewModel CreateModel(DomainObject domainObject)
        {
            var model = new SeasonViewModel();
            var season = (Season)domainObject;
            model.Id = season.Id;
            model.Name = season.Name;
            model.Year = season.Year;
            model.LeagueName = season.League.Name;
            model.Complete = season.IsComplete();

            return model;
        }

        public override List<DomainObject> GetDomainObjects()
        {            

            if (LeagueId != null)
                return seasonService.GetAll((int)LeagueId).ToList<DomainObject>();
            else
                return seasonService.GetAll().ToList<DomainObject>();
            




        }
    }
}
