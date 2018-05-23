using JodyApp.Database;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueViewModel:SingleEntityViewModel
    {
        public static string LEAGUE_NAME = "League Name";
        public static string CURRENT_YEAR = "Current Year";
                
        LeagueService leagueService;

        public int? Id { get; set; }
        public string LeagueName { get; set; }
        public int CurrentYear { get; set; }
        public bool IsComplete { get; set; }
        public string CurrentCompetition { get; set; }

        public LeagueViewModel():base() { leagueService = new LeagueService(db); }

        public LeagueViewModel(int? id, string leagueName, int currentYear, bool isComplete, string currentCompetition)
        {            
            Id = id;
            LeagueName = leagueName;
            CurrentYear = currentYear;
            IsComplete = isComplete;
            CurrentCompetition = currentCompetition;
        }


        public override string[] GetInputFields()
        {
            return new string[]
            {
                LEAGUE_NAME, CURRENT_YEAR
            };
        }

        public override void CreateModelFromInput(Dictionary<string, string> inputs)
        {
            LeagueName = inputs[LEAGUE_NAME];
            CurrentYear = int.Parse(inputs[CURRENT_YEAR]);            
        }

        public override void SetById(int id)
        {
            var league = leagueService.GetById(id);

            Id = league.Id;
            LeagueName = league.Name;
            CurrentYear = league.CurrentYear;
            var nextCompetition = leagueService.GetNextCompetition(league);
            if (nextCompetition == null) IsComplete = true;
            else
            {
                IsComplete = false;
                CurrentCompetition = nextCompetition.Name;
            }

        }

    }
}
