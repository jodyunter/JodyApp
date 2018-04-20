using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Season;
using System.Data.Entity;
using JodyApp.Domain.Table;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {            
        }


        public List<SeasonDivision> GetDivisionsBySeason(Season season)
        {
            return db.SeasonDivisions.Include("Season").Where(d => d.Season.Id == season.Id).ToList<SeasonDivision>();
        }

        //this will return the list of teams, but more importantly will setup the division rankings
        public List<RecordTableTeam> SortByDivision(SeasonDivision division)
        {
            //decision to make.  Do we organize a higher teir based on lower tier rank without explicitly saying so?
            RecordTable table = new RecordTable();
            division.GetAllTeamsInDivision(db).ForEach(team => {
                table.Add((RecordTableTeam)team);                    
            });

            var sortedTeams = table.SortIntoDivisions();

            var sortedList = StandingsSorter.SortByRules(sortedTeams, division);            

            return sortedList;
            
        }


        public List<Division> GetByLeague(League league)
        {
            return db.Divisions.Where(d => d.League.Id == league.Id).ToList<Division>();
        }

        
     
    }
}

