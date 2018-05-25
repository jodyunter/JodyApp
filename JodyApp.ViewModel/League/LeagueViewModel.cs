using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int CurrentYear { get; set; }        
        public string CurrentCompetition { get; set; }

        public LeagueViewModel(int? id, string name, int currentYear,string currentCompetition)
        {
            this.Id = id;
            this.Name = name;
            this.CurrentCompetition = currentCompetition;            
            this.CurrentYear = currentYear;
        }
    }
}
