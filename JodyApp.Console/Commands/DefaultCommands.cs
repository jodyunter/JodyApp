using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class DefaultCommands
    {
        public DefaultCommands() : base() { }
        public DefaultCommands(ApplicationContext context):base() { }
        // Methods used as console commands must be public and must return a string

        public void QUIT(ApplicationContext context)
        {
            IOMethods.WriteToConsole("Exiting Program, press enter to close the windoer");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void BACK(ApplicationContext context)
        {
            context.CurrentView = context.GetLastView();
            
        }
        public BaseView DoSomething(ApplicationContext context, int id, string data)
        {
            return new MessageView(string.Format(
                "I did something to the record Id {0} and save the data {1}", id, data));
        }


        public BaseView DoSomethingElse(ApplicationContext context, DateTime date)
        {
            return new MessageView(string.Format("I did something else on {0}", date));
        }

        public BaseView DoSomethingOptional(ApplicationContext context, int id, string data = "No Data Provided")
        {
            var result = string.Format(
                "I did something to the record Id {0} and save the data {1}", id, data);

            if (data == "No Data Provided")
            {
                result = string.Format(
                "I did something to the record Id {0} but the optinal parameter "
                + "was not provided, so I saved the value '{1}'", id, data);
            }
            return new MessageView(result);
        }


    }
}
