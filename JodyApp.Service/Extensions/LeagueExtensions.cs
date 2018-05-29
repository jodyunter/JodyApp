using JodyApp.Domain;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class LeagueService
    {
        public override BaseViewModel GetModelById(int id)
        {
            return DomainToDTO(db.Leagues.Where(l => l.Id == id).FirstOrDefault());
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
            }            
            else
            {
                league.Name = m.Name;
                league.CurrentYear = m.CurrentYear;
                //what about reference comps? Different scren!
            }

            return DomainToDTO(league);
        }

        public override ListViewModel GetAll()
        {
            var result = new ListViewModel(new List<BaseViewModel>());

            db.Leagues.ToList().ForEach(l =>
            {
                result.Items.Add(DomainToDTO(l));
            });

            return result;
        }

        public override DomainObject GetById(int? id)
        {
            if (id == null) return null;

            return db.Leagues.Where(l => l.Id == (int)id).FirstOrDefault();
        }

    }
}
