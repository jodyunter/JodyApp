using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Table;

namespace JodyApp.Domain.Season
{
    public class SeasonTable: RecordTable
    { 
        public Season Season { get; set; }
    }
}
