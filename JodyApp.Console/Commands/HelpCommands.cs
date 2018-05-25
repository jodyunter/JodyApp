using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public static class HelpCommands
    {
        public static string List(string nameSpace = "All", string method = "All")
        {
            var libraries = Program._commandLibraries;
            var nameSpaceKey = nameSpace.Contains("Commands") ? nameSpace : nameSpace + "Commands";

            var result = "";
            if (!libraries.ContainsKey(nameSpaceKey))
            {
                result = "Command Libraries:";
                Program._commandLibraries.Keys.ToList().ForEach(key =>
                {
                    result += "\n" + key;
                });

            }
            else
            {
                result = "Command Library: " + nameSpace;

                var methodDictionary = libraries[nameSpaceKey];
                if (!methodDictionary.ContainsKey(method))
                {
                    methodDictionary.ToList().ForEach(m =>
                    {
                        result += "\n" + m.Key;
                    });
                }
                else
                {
                    var m = methodDictionary[method];

                    result += "\n" + "Need to putput info relating to : " + method;
                }
            }
            return result;
        }
    }
}
