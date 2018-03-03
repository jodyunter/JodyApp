using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class Division
    {
        List<Team> Team { get; set; }
        Division Parent { get; set; }
    }
}
