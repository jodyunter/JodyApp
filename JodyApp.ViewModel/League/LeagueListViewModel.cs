using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class LeagueListViewModel
    {
        public List<LeagueViewModel> Items {get; set;}
        public LeagueListViewModel(List<LeagueViewModel> items)
        {
            Items = items;
        }

    }

}


