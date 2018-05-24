using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace JodyApp.Console.Flow
{
    public class GatherInput
    {
        public static Dictionary<string, string> GetInput(string[] fields)
        {
            var results = new Dictionary<string, string>();

            fields.ToList().ForEach(key =>
            {
                results[key] = ReadInput(key, false);
            });

            return results;

        }

        public static string ReadInput(string text, bool textOnNewLine)
        {
            if (textOnNewLine)
                WriteLine(text + ":");
            else
                Write(text + ":");

            var input = ReadLine();

            return input;
        }
    }
}
