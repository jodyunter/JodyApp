using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    
    public interface ICompetition
    {
        int Year { get; set; }
        int StartingDay { get; set; }
        bool Complete { get; set; }
        bool Started { get; set; }
        League League { get; set; }
    }
}
