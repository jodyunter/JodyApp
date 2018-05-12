using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace JodyApp.Console.Views
{
    public abstract class SingleEntityView:BaseView
    {
        public SingleEntityViewModel viewModel { get; set; }
        
        public abstract string GetHeaderString();

        public override string GetDisplayString()
        {
            return GetHeaderString() + "\n" + GetDisplayStringNoHeader();             
        }

        public abstract string GetDisplayStringNoHeader();
    }
}
