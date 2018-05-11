using JodyApp.Database;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeasonViewModel : SingleEntityViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string LeagueName { get; set; }
        public bool Complete { get; set; }

        SeasonService seasonService;

        public SeasonViewModel() : base() { seasonService = new SeasonService(db); }          

        public override void SetById(int id)
        {
            var season = seasonService.GetById(id);
            Id = season.Id;
            Name = season.Name;
            Year = season.Year;
            LeagueName = season.League.Name;
            Complete = season.IsComplete();
        }
    }
}
