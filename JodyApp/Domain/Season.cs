using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Table;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain
{
    public class Season:DomainObject
    {        
        public string Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }
        
        [NotMapped]
        public RecordTable Standings { get; set; }

        [InverseProperty("Season")]
        virtual public List<Team> TeamData { get; set; }

        virtual public List<ScheduleRule> ScheduleRules { get; set; }

        public League League { get; set; }

        public bool Started { get; set; }
        public bool Complete { get; set; }
        public int StartingDay { get; set; }

        public override bool Equals(object obj)
        {
            var season = obj as Season;
            return season != null &&
                   Name == season.Name &&
                   Year == season.Year;
        }

        public void SetupStandings()
        {
            Standings = new RecordTable();
            TeamData.ForEach(team =>
           {
               Standings.Add(team);
           });
        }


    }
}
