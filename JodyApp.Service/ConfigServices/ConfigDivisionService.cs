using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigDivisionService : BaseService
    {
        public ConfigDivisionService(JodyAppContext db) : base(db) { }

        public ConfigDivisionService():base()
        {
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override ListViewModel GetAll()
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel GetModelById(int id)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public override DomainObject GetById(int? id)
        {
            if (id == null) return null;

            return db.ConfigDivisions.Where(t => t.Id == id).FirstOrDefault();
        }

        public ListViewModel GetByLeagueId(int leagueId)
        {
            var items = new List<BaseViewModel>();

            db.ConfigDivisions.Where(t => t.League.Id == leagueId).ToList().ForEach(d =>
           {
               items.Add(DomainToDTO(d));
           });

            return new ListViewModel(items);
        }
    }
}
