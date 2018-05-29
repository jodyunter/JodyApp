using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueViewModel:BaseViewModel
    {
        private string v1;
        private int? v2;

        public int? Id { get; set; }
        public string Name { get; set; }
        public int CurrentYear { get; set; }                

        public LeagueViewModel(int? id, string name, int currentYear)
        {
            this.Id = id;
            this.Name = name;            
            this.CurrentYear = currentYear;
        }

        public LeagueViewModel(int? id, string v1, int? v2)
        {
            Id = id;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
