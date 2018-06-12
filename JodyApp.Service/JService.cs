using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public interface JService
    {        
        BaseViewModel GetModelById(int id);
        ListViewModel GetAll();
        void Save();
        BaseViewModel Save(BaseViewModel model);
    }
}
