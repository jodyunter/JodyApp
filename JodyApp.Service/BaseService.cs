using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class BaseService
    {
        public JodyAppContext db { get; }

        public BaseService(JodyAppContext dbContext)
        {
            this.db = dbContext;

        }
    }
}
