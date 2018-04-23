using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Table;

namespace JodyApp.Domain.Season
{ 
    public partial class SeasonDivision:RecordTableDivision
    {        
        virtual public Season Season { get; set; }

        public SeasonDivision() { }

        //todo:  Maybe we should use the base constructors when ready
        public SeasonDivision(Division division, Season season)
        {

            this.Season = season;
            this.Name = division.Name;
            this.ShortName = division.ShortName;
            this.Level = division.Level;
            this.Order = division.Order;
            this.League = division.League;
            this.Teams = new List<Team>();
            this.Rankings = new List<DivisionRank>();
            //sorting rules must be handled seperately
            //parent must be handled seperately
        }
        
    }
}
