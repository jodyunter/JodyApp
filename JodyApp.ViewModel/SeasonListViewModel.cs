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
        
        public override string Header { get { return "List of Seasons"; } }

        public SeasonListViewModel():base() { seasonService = new SeasonService(db); }

        public override BaseViewModel CreateModel(DomainObject domainObject)
        {
            throw new NotImplementedException();
        }

        public override List<DomainObject> GetDomainObjects()
        {
            seasonService.Get
        }
    }
}
