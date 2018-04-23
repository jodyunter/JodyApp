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

        public static Team GetByName(JodyAppContext db, string name, Season season)
        {
            return db.Teams.Where(t => t.Name == name && t.Season == season).First();           
        }

        public static List<Team> GetTeams(JodyAppContext db, Season season)
        {
            if (season == null) return db.Teams.Where(t => t.Season == null).ToList<Team>();
            else return db.Teams.Where(t => t.Season.Id == season.Id).ToList<Team>();
        }
    }
}
