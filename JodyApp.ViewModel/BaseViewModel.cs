using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public abstract class BaseViewModel:ReferenceObject
    {
        public BaseViewModel() : base() { }
        public BaseViewModel(int id, string name) : base(id, name) { }
    }
}
