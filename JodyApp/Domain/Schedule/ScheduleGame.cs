using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class ScheduleGame
    {
        public Team Home { get; set; }
        public Team Away { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool Complete { get; set; }
        
    }
}
