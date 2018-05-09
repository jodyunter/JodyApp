using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;

using System.Data.Entity;
using JodyApp.Domain.Table;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {                        
        }

        public List<Team> GetAllTeamsInDivision(Division division)
        {           

            return Division.GetAllTeamsInDivision(db, division);
        }
        
        public List<Division> GetByReferenceSeason(Season season)
        {
            return Division.GetDivisionsBySeason(db, season);
        }

        public List<Division> GetDivisionsByParent(Division parent)
        {
            return Division.GetDivisionsByParent(db, parent);
        }
        public List<Division> GetDivisionsBySeason(Season season)
        {
            return Division.GetDivisionsBySeason(db, season);
        }

        //this will return the list of teams, but more importantly will setup the division rankings
        public List<Team> SortByDivision(Division division)
        {
            //decision to make.  Do we organize a higher teir based on lower tier rank without explicitly saying so?
            RecordTable table = new RecordTable();
            var teams = new List<Team>();

            Division.GetAllTeamsInDivision(db, division).ForEach(team => {
                table.Add(team);                    
            });

            var sortedTeams = table.SortIntoDivisions();

            var sortedList = StandingsSorter.SortByRules(sortedTeams, division);            

            return sortedList;
            
        }

        public Division GetByName(String name, League league, Season season)
        {            
            return Division.GetByName(db, name, league, season);
        }

        public List<Division> GetByLeague(League league)
        {            
            return Division.GetByLeague(db, league);
        }

        public List<Division> GetDivisionsByLevel(int level, Season season)
        {
            return Division.GetDivisionsByLevel(db, level, season);
        }

        public Division GetByLeagueAndSeasonAndName(League league, Season season, string name)
        {
            return Division.GetByLeagueAndSeasonAndName(db, league, season, name);
        }
        
     
    }
}

