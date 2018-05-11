using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueListViewModel:BaseViewModel
    {
        LeagueService leagueService = new LeagueService();
        public string Header = "List of Leagues";
        public List<LeagueViewModel> Leagues { get; set; }
        
        public void SetData()
        {
            Leagues = new List<LeagueViewModel>();
            leagueService.GetAll().ForEach(league =>
            {
                var model = new LeagueViewModel();
                model.Id = league.Id;
                model.LeagueName = league.Name;
                model.CurrentYear = league.CurrentYear;                
                var nextCompetition = leagueService.GetNextCompetition(league);
                if (nextCompetition == null) model.IsComplete = true;
                else
                {
                    model.IsComplete = false;
                    model.CurrentCompetition = nextCompetition.Name;
                } 
            });
        }
    }
}
