using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Domain;

namespace JodyApp.ConsoleApp
{
    public class ApplicationContext
    {
        public JodyAppContext DbContext { get; set; }
        public League SelectedLeague { get; set; }
        public string CurrentUser { get; set; }
        public List<BaseView> ViewHistory { get; set; }

        public string ReadPrompt { get; set; }
        public string CommandNameSpace { get; set; }

        public Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> CommandLibraries;

        public ApplicationContext()
        {
            SelectedLeague = null;
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            DbContext = new JodyAppContext(connectionString);
            ViewHistory = new List<BaseView>();
            ReadPrompt = "SportsApp>";
            CommandNameSpace = "JodyApp.ConsoleApp.Commands";
            SetupLibraries();
        }

        public void AddView(BaseView view)
        {

            ViewHistory.Insert(0, view);

            if (ViewHistory.Count > 20) ViewHistory.RemoveRange(20, 1);
        }

        public BaseView GetLastView()
        {            
            for (int i = 0; i < ViewHistory.Count; i++)
            {
                if (!((ViewHistory[i] is ErrorView) || (ViewHistory[i] is MessageView)))
                {
                    return ViewHistory[i];
                }
            }

            return null;
        }

        public void SetupLibraries()
        {
            // Any static classes containing commands for use from the 
            // console are located in the Commands namespace. Load 
            // references to each type in that namespace via reflection:
            CommandLibraries = new Dictionary<string, Dictionary<string,
                    IEnumerable<ParameterInfo>>>();

            // Use reflection to load all of the classes in the Commands namespace:
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == CommandNameSpace
                    select t;
            var commandClasses = q.ToList();

            foreach (var commandClass in commandClasses)
            {
                // Load the method info from each class into a dictionary:
                var methods = commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);
                var methodDictionary = new Dictionary<string, IEnumerable<ParameterInfo>>();
                foreach (var method in methods)
                {
                    string commandName = method.Name;
                    methodDictionary.Add(commandName, method.GetParameters());
                }

                // Add the dictionary of methods for the current class into a dictionary of command classes:
                CommandLibraries.Add(commandClass.Name, methodDictionary);
            }
        }
    }
}
