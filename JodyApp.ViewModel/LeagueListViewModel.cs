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
        public List<LeagueViewModel> Leagues { get; set; }
        
    }
}
