using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public class TeamService : BaseService<Team>
    {
        public override DbSet<Team> Entities => db.Teams;

        public TeamService(JodyAppContext instance):base(instance)
        {
            
        }
        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

    }
}
