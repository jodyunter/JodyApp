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
            WriteToConsole(view.GetView());
        }


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

        public static Dictionary<string, string> GatherData(ApplicationContext context, string dataContext, List<string> prompts)
        {
            var result = new Dictionary<string, string>();

            for (int i = 0; i < prompts.Count; i++)
            {                
                result[prompts[i]] = ReadFromConsole(context, dataContext + ">" + prompts[i] + ">");
            }

            return result;
            
        }
    }
}
