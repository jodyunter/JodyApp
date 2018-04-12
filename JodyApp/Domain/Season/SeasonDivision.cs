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
        public SeasonDivision(Division division, Season season)
        {

            this.Season = season;
            this.Name = division.Name;
            this.ShortName = division.ShortName;
            this.Level = division.Level;
            this.Order = division.Order;
            //parent must be handled seperately
        }
    }
}
