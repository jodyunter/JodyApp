﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Table;

namespace JodyApp.Domain.Season
{
    public class Season:DomainObject
    {        
        public string Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }
        
        [NotMapped]
        public RecordTable Standings { get; set; }

        [InverseProperty("Season")]
        virtual public List<SeasonTeam> TeamData { get; set; }

        virtual public List<SeasonScheduleRule> ScheduleRules { get; set; }
    }
}
