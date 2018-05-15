using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;

using System.Data.Entity;
using JodyApp.Domain.Table;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {

        public DivisionService() : base() { Initialize(null); }

        public DivisionService(JodyAppContext context):base(context)
        {
            Initialize(context);
        }

        public override void Initialize(JodyAppContext db) { }

        public List<Team> GetAllTeamsInDivision(Division division)
        {


            var teams = new List<Team>();

            if (division.Teams != null) teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));
            });

            return teams;
        }
        

        public List<Division> GetDivisionsByParent(Division parent)
        {
            var divisions = new List<Division>();

            db.Divisions.Where(d => d.Parent != null && d.Parent.Id == parent.Id).ToList().ForEach(div =>
            {
                divisions.Add(div);
                divisions.AddRange(GetDivisionsByParent(div));
            });

            return divisions;
        }
        public List<Division> GetDivisionsBySeason(Season season)
        {
            return db.Divisions.Where(d => d.Season.Id == season.Id).ToList();
        }

        //this will return the list of teams, but more importantly will setup the division rankings
        public List<Team> SortByDivision(Division division)
        {
            //decision to make.  Do we organize a higher teir based on lower tier rank without explicitly saying so?
            RecordTable table = new RecordTable();
            var teams = new List<Team>();

            GetAllTeamsInDivision(division).ForEach(team => {
                table.Add(team);                    
            });

            var sortedTeams = table.SortIntoDivisions();

            var sortedList = StandingsSorter.SortByRules(sortedTeams, division);            

            return sortedList;
            
        }

        public Division GetByName(String name, League league, Season season)
        {
            return db.Divisions.Where(d => (d.Name == name) && (d.League.Id == league.Id) && (d.Season.Id == season.Id)).FirstOrDefault();
        }

        public List<Division> GetByLeague(League league)
        {
            return db.Divisions.Where(d => d.League.Id == league.Id && d.Season == null).ToList();
        }

        public List<Division> GetDivisionsByLevel(int level, Season season)
        {
            return db.Divisions.Where(d => d.Level == level && (d.Season.Id == season.Id)).ToList();
        }

        public Division GetByLeagueAndSeasonAndName(League league, Season season, string name)
        {
            return db.Divisions.Where(d => d.League.Id == league.Id && d.Season.Id == season.Id && d.Name == name).FirstOrDefault();
        }
        
    }
}

