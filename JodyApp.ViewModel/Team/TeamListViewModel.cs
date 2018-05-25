using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class TeamListViewModel
    {
        public List<TeamViewModel> Items { get; set; }
        public TeamListViewModel(List<TeamViewModel> items)
        {
            Items = items;
        }
    }
}
