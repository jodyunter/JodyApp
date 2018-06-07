using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Commands;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Service;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp
{
    public class ApplicationContext
    {
        private string _promptSplitter = ">";
        private string _baseReadPrompt;
        private ReferenceObject _selectedLeague;
        private ReferenceObject _selectedSeason;

        public JodyAppContext DbContext { get; set; }
        public ReferenceObject SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                _selectedSeason = value;
                SetReadPrompt();
            }
        }
        public ReferenceObject SelectedLeague
        {
            get { return _selectedLeague; }
            set
            {
                _selectedLeague = value;
                _selectedSeason = null;
                //reset other selections here
                SetReadPrompt();
            }
        }
        public string CurrentUser { get; set; }
        public List<BaseView> ViewHistory { get; set; }
        public BaseView CurrentView { get; set; }
        public string ReadPrompt { get; set; }
        public string CommandNameSpace { get; set; }

        public Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> CommandLibraries;
        public Dictionary<string, object> CommandObjects;

        public Dictionary<string, BaseService> ServiceLibraries;        

        public ApplicationContext(string baseReadPrompt)
        {
            SelectedLeague = null;
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            DbContext = new JodyAppContext(connectionString);
            ViewHistory = new List<BaseView>();
            _baseReadPrompt = baseReadPrompt;
            CommandNameSpace = "JodyApp.ConsoleApp.Commands";
            SetupServiceLibraries();
            SetupCommandLibraries();
            SetReadPrompt();
        }

        public void SetCurrentView(BaseView view)
        {
            AddView(view);
            CurrentView = view;
        }
        public void AddView(BaseView view)
        {

            if (view.IsViewNew)
            {
                ViewHistory.Insert(0, view);

                if (ViewHistory.Count > 20) ViewHistory.RemoveRange(20, 1);
                view.IsViewNew = false;
            }
        }


        public BaseView RemoveLastView()
        {
            for (int i = 0; i < ViewHistory.Count; i++)
            {
                if (!((ViewHistory[i] is ErrorView) || (ViewHistory[i] is MessageView)))
                {
                    ViewHistory.RemoveAt(i);
                    return null;
                }
            }

            return null;
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


        public void SetupViewLibraries()
        {
            
        }
        public void SetupServiceLibraries()
        {
            ServiceLibraries = new Dictionary<string, BaseService>();

            ServiceLibraries.Add("League", new LeagueService(DbContext));
            ServiceLibraries.Add("ConfigTeam", new ConfigTeamService(DbContext));
            ServiceLibraries.Add("ConfigDivision", new ConfigDivisionService(DbContext));
            ServiceLibraries.Add("Season", new SeasonService(DbContext));
            ServiceLibraries.Add("Standings", new StandingsService(DbContext));
        }

        public void SetupCommandLibraries()
        {
            // Any static classes containing commands for use from the 
            // console are located in the Commands namespace. Load 
            // references to each type in that namespace via reflection:
            CommandLibraries = new Dictionary<string, Dictionary<string,
                    IEnumerable<ParameterInfo>>>();
            CommandObjects = new Dictionary<string, object>();

            // Use reflection to load all of the classes in the Commands namespace:
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == CommandNameSpace
                    select t;
            var commandClasses = q.ToList();

            foreach (var commandClass in commandClasses)
            {
                if (!commandClass.IsAbstract && !commandClass.IsDefined(typeof(CompilerGeneratedAttribute)))
                {
                    //create an instance and add it to CommandObjects
                    var ctor = commandClass.GetConstructor(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance,
                        null, new Type[] { GetType() }, null);
                    var instance = ctor.Invoke(new object[] { this });
                    CommandObjects.Add(commandClass.Name, instance);
                    
                    // Load the method info from each class into a dictionary:
                    //var methods = commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);                
                    var methods = commandClass.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    var methodDictionary = new Dictionary<string, IEnumerable<ParameterInfo>>();
                    foreach (var method in methods)
                    {
                        var attribute = method.GetCustomAttributes(typeof(Command));
                        if (attribute.Count() > 0)
                        {
                            string commandName = method.Name;
                            methodDictionary.Add(commandName, method.GetParameters());
                        }
                        
                    }

                    // Add the dictionary of methods for the current class into a dictionary of command classes:
                    CommandLibraries.Add(commandClass.Name, methodDictionary);
                }
            }
        }

        void SetReadPrompt()
        {
            ReadPrompt = _baseReadPrompt + _promptSplitter;
            if (SelectedLeague != null)
            {
                ReadPrompt += SelectedLeague.Name + _promptSplitter;
                if (SelectedSeason != null)
                {
                    ReadPrompt += SelectedSeason.Name + _promptSplitter;
                }
            }
        }
    }
}
