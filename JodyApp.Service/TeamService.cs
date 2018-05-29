using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public partial class TeamService : BaseService
    {        

        public TeamService(JodyAppContext instance):base(instance)
        {
            
        }

    }
}
