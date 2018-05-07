using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;

namespace JodyApp.Domain
{

    public partial class Division
    {
        public static List<Division> GetDivisionsByParent(JodyAppContext db, Division parent)
        {
            if (parent == null) return new List<Division>();

            return db.Divisions.Where(div => div.Parent.Id == parent.Id).ToList<Division>();            
        }

        public static Division GetByName(JodyAppContext db, string name, League league, Season season)
        {            
                        
            return db.Divisions.Where(d => (d.Name == name) && (d.League.Id == league.Id) && (d.Season.Id == season.Id)).FirstOrDefault();

        }


        public static List<Team> GetAllTeamsInDivision(JodyAppContext db, Division division)
        {

            var teams = new List<Team>();

            if (division.Teams != null) teams.AddRange(division.Teams);

            Division.GetDivisionsByParent(db, division).ForEach(div =>
            {
                teams.AddRange(Division.GetAllTeamsInDivision(db, div));
            });

            return teams;
        }
        
        public static List<Division> GetByLeague(JodyAppContext db, League league)
        {
            return db.Divisions.Where(d => d.League.Id == league.Id && d.Season == null).ToList();
        }

        public static Division GetByLeagueAndSeasonAndName(JodyAppContext db, League league, Season season, String name)
        {
            return db.Divisions.Where(d => d.League.Id == league.Id && d.Season.Id == season.Id && d.Name == name).FirstOrDefault();
        }
        
        public static List<Division> GetDivisionsBySeason(JodyAppContext db, Season season)
        {
            return db.Divisions.Include("Season").Where(d => d.Season.Id == season.Id).ToList();
        }

        public static List<Division> GetDivisionsByLevel(JodyAppContext db, int level)
        {
            return GetDivisionsByLevel(db, level, null);
        }
        public static List<Division> GetDivisionsByLevel(JodyAppContext db, int level, Season season)
        {
          
            return db.Divisions.Where(d => d.Level == level && (d.Season.Id == season.Id)).ToList();


        }
    }


}

