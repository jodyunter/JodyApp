using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class InputView:BaseView
    {
        public string Prompt { get; set; }
        public InputView(string prompt) : base(null) { Prompt = prompt; }
    }
}
