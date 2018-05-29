using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public abstract class BaseService
    {

        public JodyAppContext db { get; set; }        


        public BaseService() { db = new JodyAppContext(); }

        public BaseService(JodyAppContext dbContext)
        {
            db = dbContext;            
        }

        public BaseService(string ConnectionString)
        {
            db = new JodyAppContext(ConnectionString);            
        }

        public void Rollback()
        {
            var changedEntries = db.ChangeTracker.Entries()
                 .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Reload();
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Save() { db.SaveChanges(); }

        public abstract DomainObject GetById(int? id);

        public abstract BaseViewModel GetModelById(int id);
        public abstract BaseViewModel DomainToDTO(DomainObject obj);
        public abstract BaseViewModel Save(BaseViewModel model);

        public abstract ListViewModel GetAll();

    }
}
