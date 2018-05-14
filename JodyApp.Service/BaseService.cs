﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Service
{
    public abstract class BaseService
    {        

        public JodyAppContext db { get; set;  }

        public abstract void Initialize(JodyAppContext db);

        public BaseService() { db = null; Initialize(db);  }

        public BaseService(JodyAppContext dbContext)
        {
            db = dbContext;
            Initialize(db);
        }

        public BaseService(string ConnectionString)
        {
            db = new JodyAppContext(ConnectionString);
            Initialize(db);
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

    }
}
