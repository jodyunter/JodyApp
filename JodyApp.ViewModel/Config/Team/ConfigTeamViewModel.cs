namespace JodyApp.ViewModel
{
    public class ConfigTeamViewModel:BaseViewModel
    {        
        public int Skill { get; set; }        
        public ReferenceObject League { get; set; }        
        public ReferenceObject Division { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigTeamViewModel(int? id, string name, int skill, ReferenceObject league, ReferenceObject division, int? firstYear, int? lastYear)
        {
            Id = id;
            Name = name;
            Skill = skill;
            League = league;
            Division = division;
            FirstYear = firstYear;
            LastYear = lastYear;
        }

  
    }
}
