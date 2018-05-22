using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;

namespace JodyApp.Console
{
    public class ApplicationContext
    {
        public JodyAppContext DbContext { get; set; }
        public League SelectedLeague { get; set; }

        public ApplicationContext()
        {
            SelectedLeague = null;
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            DbContext = new JodyAppContext(connectionString);
        }
    }
}
