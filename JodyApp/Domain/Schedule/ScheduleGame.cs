using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    [Table("Games")]
    public class ScheduleGame:Game
    {
        public int Day { get; set; }
    }
}
