using JodyApp.Database;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeasonViewModel:BaseViewModel
    {
        string Name { get; set; }
        int Year { get; set; }
        string LeagueName { get; set; }
        public StandingsViewModel Standings { get; set; }
        

        public SeasonViewModel(JodyAppContext db)
        {
            Standings = new StandingsViewModel(db);
        }

        public void SetSeason(string leagueName, string seasonName, params string[] divisions)
        {
            LeagueName = leagueName;
            Name = seasonName;
            Year = Standings.Year;
            SetStandings(leagueName, seasonName, divisions);
        }
        public void SetStandings(string leagueName, string seasonName, params string[] divisions) 
        {         
            Standings.SetStandingsCurrentYear(leagueName, Name, divisions);            
        }
    }
}
