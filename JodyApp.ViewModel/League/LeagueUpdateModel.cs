using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel.League
{
    public class LeagueUpdateModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsComplete { get; set; }
        

        public LeagueUpdateModel() { }
    }
}
