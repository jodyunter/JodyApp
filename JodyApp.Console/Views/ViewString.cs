using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class ViewString
    {
        //views
        public static string ViewLeague = "League";
        public static string ViewSesaon = "Season";

        //actions
        public static string ActionList = "List";
        public static string ActionGet = "Get";
        public static string ActionStartLeague = "Start";

        public static List<string> PrintLeague(int leagueId)
        {
            return GetList(ViewLeague, ActionGet, leagueId.ToString());
        }
        public static List<string> PrintLeagues()
        {
            return GetList(ViewLeague, ActionList);
        }

        public static List<string> GetList(params string[] data)
        {
            return data.ToList();
        }

        public static List<String> StartLeague(int leagueId)
        {
            return GetList(ViewLeague, ActionStartLeague, leagueId.ToString());
        }
    }
}
