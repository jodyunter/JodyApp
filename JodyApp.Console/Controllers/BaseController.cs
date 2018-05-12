using JodyApp.Console.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public abstract class BaseController
    {
        public BaseView View { get; set; }
        public abstract BaseView ParseInput(List<string> input, int offset);

    }
}
