using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigTeamListViewModel
    {
        public List<ConfigTeamViewModel> Items { get; set; }
        public ConfigTeamListViewModel(List<ConfigTeamViewModel> items)
        {
            Items = items;
        }
    }
}
