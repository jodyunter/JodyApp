using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class HelpView : BaseView
    {
        public string NameSpace { get; set; }
        public string Method { get; set; }

        public Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> CommandLibraries;

        public HelpView(string nameSpace, string method, Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> commandLibraries):base(null)
        {
            NameSpace = nameSpace;
            Method = method;
            CommandLibraries = commandLibraries;
        }

        public override string GetView()
        {
            var libraries = CommandLibraries;
            var nameSpaceKey = NameSpace.Contains("Commands") ? NameSpace : NameSpace + "Commands";

            var result = "";
            if (!libraries.ContainsKey(nameSpaceKey))
            {
                result = "Command Libraries:";
                CommandLibraries.Keys.ToList().ForEach(key =>
                {
                    result += "\n" + key;
                });

            }
            else
            {
                result = "Command Library: " + NameSpace;

                var methodDictionary = libraries[nameSpaceKey];
                if (!methodDictionary.ContainsKey(Method))
                {
                    methodDictionary.ToList().ForEach(m =>
                    {
                        result += "\n" + m.Key;
                    });
                }
                else
                {
                    var m = methodDictionary[Method];

                    result += "\n" + "Need to putput info relating to : " + Method;
                }
            }
            return result;
        }
    }
}
