using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Domain;

namespace JodyApp.ConsoleApp
{
    public class ApplicationContext
    {
        public JodyAppContext DbContext { get; set; }
        public League SelectedLeague { get; set; }
        public string CurrentUser { get; set; }
        public List<BaseView> ViewHistory { get; set; }
        
        public ApplicationContext()
        {
            SelectedLeague = null;
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            DbContext = new JodyAppContext(connectionString);
            ViewHistory = new List<BaseView>();
        }

        public void AddView(BaseView view)
        {

            ViewHistory.Insert(0, view);

            if (ViewHistory.Count > 20) ViewHistory.RemoveRange(20, 1);
        }

        public BaseView GetLastView()
        {            
            for (int i = 0; i < ViewHistory.Count; i++)
            {
                if (!((ViewHistory[i] is ErrorView) || (ViewHistory[i] is MessageView)))
                {
                    return ViewHistory[i];
                }
            }

            return null;
        }
    }
}
