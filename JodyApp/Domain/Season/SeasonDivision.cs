using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyApp.Domain.Season
{ 
    public class SeasonDivision:Division
    {        
        virtual public Season Season { get; set; }

        public SeasonDivision() { }
        public SeasonDivision(Season season)
        {
            this.Season = season;
        }
    }
}
