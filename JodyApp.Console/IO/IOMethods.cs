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


        
        public static string ReadFromConsole(string prompt, string promptMessage = "", string extraInfo = "")
        {
            if (!(string.IsNullOrEmpty(extraInfo)))
            {
                Console.WriteLine(extraInfo);
            }
            Console.Write(prompt + promptMessage);
            return Console.ReadLine();
        }

    }
}
