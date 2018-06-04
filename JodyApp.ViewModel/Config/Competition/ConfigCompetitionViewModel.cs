using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigCompetitionViewModel:BaseViewModel
    {
        public static string PLAYOFF = "Playoff";
        public static string SEASON = "Season";

        public ReferenceObject League { get; set; }
        public string CompetitionType { get; set; }
        public ReferenceObject ReferenceCompetition { get; set; }
        public int  Order { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set;

        }
    }
}
