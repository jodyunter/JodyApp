using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Table;

namespace JodyApp.Domain.Season
{
    public class SeasonTeam:RecordTableTeam
    {
        public Season Season { get; set; }
    }
}
