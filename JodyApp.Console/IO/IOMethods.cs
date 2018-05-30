using JodyApp.ConsoleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.IO
{
    public class IOMethods
    {
        public static void WriteToConsole(string message = "")
        {

            if (message.Length > 0)
            {
                Console.WriteLine(message);
            }
        }

        public static void WriteToConsole(BaseView view)
        {
            if (view == null)
            {
                view = new ErrorView("No view to display");
            }
            WriteToConsole(view.GetView());
        }


        //should not call this directly, call from Application
        public static string ReadFromConsole(ApplicationContext context, string promptMessage = "", string extraInfo = "")
        {            
            // Show a prompt, and get input
            if (!(string.IsNullOrEmpty(extraInfo)))
            {
                Console.WriteLine(extraInfo);    
            }
            Console.Write(context.ReadPrompt + promptMessage);
            return Console.ReadLine();
        }

    }
}
