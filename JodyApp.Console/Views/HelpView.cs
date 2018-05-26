using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class HelpView : BaseView
    {
        public string NameSpace { get; set; }
        public string Method { get; set; }

        public HelpView(string nameSpace, string method):base(null)
        {
            NameSpace = nameSpace;
            Method = method;
        }

        public override string GetView()
        {
            var libraries = Program._commandLibraries;
            var nameSpaceKey = NameSpace.Contains("Commands") ? NameSpace : NameSpace + "Commands";

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
