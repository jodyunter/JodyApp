using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace JodyApp.Console.Views
{
    public abstract class SingleEntityView:View
    {
        public SingleEntityViewModel viewModel { get; set; }

        public abstract string FORMATTER { get; }
        public string GetHeaderString()
        {
            return string.Format(FORMATTER, GetHeaderStrings());
        }
        public override string GetDisplayString()
        {
            return GetHeaderString() + "\n" + GetDisplayStringNoHeader();             
        }
        public string GetDisplayStringNoHeader()
        {
            return String.Format(FORMATTER, GetDisplayStrings());
        }

        public abstract object[] GetHeaderStrings();
        public abstract object[] GetDisplayStrings();
        
    }
}
