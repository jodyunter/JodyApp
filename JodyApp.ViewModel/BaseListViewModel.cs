using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public abstract class BaseListViewModel:BaseViewModel
    {
        public abstract string Header { get; }
        public List<BaseViewModel> BaseViewModels { get; set; }

        public abstract List<DomainObject> GetDomainObjects();
        public abstract BaseViewModel CreateModel(DomainObject domainObject);
        public void SetData()
        {
            BaseViewModels = new List<BaseViewModel>();
            GetDomainObjects().ForEach(domainObject =>
            {
                var model = CreateModel(domainObject);
                BaseViewModels.Add(model);
            });
        }
    }
}
