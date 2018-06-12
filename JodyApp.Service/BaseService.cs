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
    public abstract class BaseService<T> : JService where T : DomainObject
    {
        public abstract DbSet<T> Entities { get; }

        public JodyAppContext db { get; set; }

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

        public DomainObject GetById(int? id)
        {
            return Entities.Where(d => d.Id == id).FirstOrDefault();
        }

        public BaseViewModel GetModelById(int id)
        {
            var entity = GetById(id);
            if (entity == null) return null;
            else return DomainToDTO(entity);
        }
        public abstract BaseViewModel DomainToDTO(DomainObject obj);
        public abstract BaseViewModel Save(BaseViewModel model);

        public ListViewModel GetAll()
        {
            return CreateListViewModelFromList(Entities.ToList<DomainObject>(), DomainToDTO);
        }


        public ListViewModel CreateListViewModelFromList(List<DomainObject> obj, Func<DomainObject, BaseViewModel> domainToDTO)
        {
            var items = new List<BaseViewModel>();

            obj.ForEach(o =>
            {
                items.Add(DomainToDTO(o));
            });

            var teamList = new ListViewModel(items);

            return teamList;
        }
    }
}
