using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace JodyApp.Console.Views
{
    public abstract class BaseView
    {
        
        public string CurrentDisplay { get; set; }
        public string Options { get { return BuildOptions(); } }
        public int LastOption { get; set; }

        public static string OPTION_FORMATTER = "{0,5}.{1}";        
        
        abstract public string BuildOptions();
        abstract public string GetStartingView();
        abstract public void SetViewFromOption();        

        public BaseView() { LastOption = -1; }
        public string BuildOption(int number, string text)
        {
            return String.Format(OPTION_FORMATTER, number, text);
        }


    }
}
