using JodyApp.Console.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public abstract class Controller
    {
        public View View { get; set; }
        public abstract View ParseInput(List<string> input, int offset);

    }
}
