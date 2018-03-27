using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using JodyApp.Domain.Season;

namespace JodyApp.Service
{
    public class SeasonService:BaseService
    {
        TeamService teamService;
        DivisionService divisionService;

        public SeasonService(JodyAppContext context):base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);
        }

        public void CreateNewSeason(string name, int year)
        {
            Season season = new Season();

            season.Name = name;
            season.Year = year;

            
        }
            
    }
}
