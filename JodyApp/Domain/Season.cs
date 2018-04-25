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
    public class Season:DomainObject, Competition
    {        
        public string Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }  //this is how we group everything together   
        public bool Started { get; set; }
        public bool Complete { get; set; }        
        public int StartingDay { get; set; }
        [NotMapped]
        public RecordTable Standings { get; set; }

        [InverseProperty("Season")]
        virtual public List<Team> TeamData { get; set; }

        virtual public List<ScheduleRule> ScheduleRules { get; set; }     
        
        virtual public List<Game> Games { get; set; }
                
        public League League { get; set; }


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
