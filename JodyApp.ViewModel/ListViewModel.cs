using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ListViewModel:BaseViewModel
    {        
        public List<BaseViewModel> Items { get; set; }

        public ListViewModel(List<BaseViewModel> items)
        {
            Items = items;
        }
    }
}
