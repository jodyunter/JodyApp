using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.ConsoleApp.App;
using JodyApp.Database;

namespace JodyApp.ConsoleApp
{
    class Program
    {                              
        static void Main(string[] args)
        {
            Console.Title = "Jody's App";
            ApplicationContext AppContext = new ApplicationContext("Sports App");
            Application app = new Application(AppContext);


            app.Run();
        }
    }
}
