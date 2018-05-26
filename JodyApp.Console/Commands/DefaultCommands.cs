using JodyApp.ConsoleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public static class DefaultCommands
    {
        // Methods used as console commands must be public and must return a string

        public static string DoSomething(List<BaseView> lastViews, int id, string data)
        {
            return string.Format(
                "I did something to the record Id {0} and save the data {1}", id, data);
        }


        public static string DoSomethingElse(List<BaseView> lastViews, DateTime date)
        {
            return string.Format("I did something else on {0}", date);
        }

        public static string DoSomethingOptional(List<BaseView> lastViews, int id, string data = "No Data Provided")
        {
            var result = string.Format(
                "I did something to the record Id {0} and save the data {1}", id, data);

            if (data == "No Data Provided")
            {
                result = string.Format(
                "I did something to the record Id {0} but the optinal parameter "
                + "was not provided, so I saved the value '{1}'", id, data);
            }
            return result;
        }
    }
}
