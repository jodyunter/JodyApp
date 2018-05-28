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


        public static string ReadFromConsole(ApplicationContext context, string promptMessage = "")
        {
            // Show a prompt, and get input:
            Console.Write(context.ReadPrompt + promptMessage);
            return Console.ReadLine();
        }
    }
}
