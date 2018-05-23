using JodyApp.Database;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeasonViewModel : SingleEntityViewModel
    {
        public static string SEASON_NAME = "Name";
        public static string YEAR = "Year";
        public static string LEAGUE_NAME = "League Name";
        
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string LeagueName { get; set; }
        public bool Complete { get; set; }

        SeasonService seasonService;

        public SeasonViewModel() : base() { seasonService = new SeasonService(db); }          

        public override void SetById(int id)
        {
            var season = seasonService.GetById(id);
            Id = season.Id;
            Name = season.Name;
            Year = season.Year;
            LeagueName = season.League.Name;
            Complete = season.IsComplete();
        }

        public override string[] GetInputFields()
        {
            return new string[]
            {
               SEASON_NAME, YEAR, LEAGUE_NAME
            };
        }
        public override void CreateModelFromInput(Dictionary<string, string> inputs)
        {            
            Name = inputs[SEASON_NAME];
            Year = int.Parse(inputs[YEAR]);
            LeagueName = inputs[LEAGUE_NAME];                  
        }

        
    }
}
