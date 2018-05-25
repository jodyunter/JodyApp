using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class TeamViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
        public string League { get; set; }
        public string Division { get; set; }

        public TeamViewModel(int? id, string name, int skill, string league, string division)
        {
            Id = id;
            Name = name;
            Skill = skill;
            League = league;
            Division = division;
        }
    }
}
