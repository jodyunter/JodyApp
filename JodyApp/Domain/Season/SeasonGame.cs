using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Schedule;

namespace JodyApp.Domain.Season
{
    public class SeasonGame:ScheduleGame
    {
        public Season Season { get; set; }
    }
}
