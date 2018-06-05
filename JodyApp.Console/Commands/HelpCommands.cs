using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class HelpCommands
    {
        public HelpCommands(ApplicationContext context) : base() { }
        public HelpCommands() : base() { }

        [Command]
        public BaseView List(ApplicationContext context, string nameSpace = "All", string method = "All")
        {
            var view = new HelpView(nameSpace, method, context.CommandLibraries);

            return view;
        }
    }
}
