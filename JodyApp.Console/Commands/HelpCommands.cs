using JodyApp.ConsoleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public static class HelpCommands
    {
        public static BaseView List(ApplicationContext context, string nameSpace = "All", string method = "All")
        {
            var view = new HelpView(nameSpace, method);

            return view;
        }
    }
}
