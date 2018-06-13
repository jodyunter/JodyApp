using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ReferenceViewModel:BaseViewModel
    {
        public ReferenceViewModel() : base() { }
        public ReferenceViewModel(int id, string name) : base(id, name) { }
    }
}
