using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class ConfigService
    {

        public ListViewModel GetAllDivisions()
        {
            var divisions = new List<BaseViewModel>();

            db.ConfigDivisions.ToList().ForEach(d =>
            {
                divisions.Add(DomainToDTO(d));
            });

            return new ListViewModel(divisions);
        }

        public override BaseViewModel GetModelById(int id)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }
        public override ListViewModel GetAll()
        {
            throw new NotImplementedException();
        }

        public override DomainObject GetById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
