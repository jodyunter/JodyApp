using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database; 

namespace JodyApp.Domain
{
    public partial class Team
    {

        public static Team GetByName(string name, Season season, JodyAppContext db)
        {
            return db.Teams.Where(t => t.Name == name && t.Season == season).First();
            
        }
    }
}
