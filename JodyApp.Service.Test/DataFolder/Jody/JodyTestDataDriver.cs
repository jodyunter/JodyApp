using System;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain.Playoffs;
using JodyApp.Service.CompetitionServices;


namespace JodyApp.Service.Test
{
    public class JodyTestDataDriver : AbstractTestDataDriver
    {
        string LeagueName = "Jody League";
        string PlayoffName = "Playoffs";
        string RegularSeasonName = "Regular Season";
        
        DivisionService divisionService;
        LeagueService leagueService;
        CompetitionService competitionService;        
        public JodyTestDataDriver() : base() { }
        public JodyTestDataDriver(JodyAppContext db) : base(db)
        {            
            divisionService = new DivisionService(db);
            leagueService = new LeagueService(db);            
            competitionService = new CompetitionService(db);
        }

        ConfigDivision League, Premier, Division1;
        ConfigDivision Division2;

        ConfigTeam Toronto, Montreal, Ottawa, NewYork, Boston, QuebecCity;
        ConfigTeam Vancouver, Edmonton, Calgary;
        ConfigTeam Winnipeg, Minnesota, Chicago;
        ConfigTeam Colorado, Pittsburgh, Philadelphia, NewJersey, Hamilton, Nashville;
        ConfigTeam Washington, Victoria, Columbus, Seattle, SanJose, LosAngelas;

        ConfigGroup PremierQualificationGroup;
        ConfigGroup ChampionshipGroup;
        ConfigGroup Division1QualificationGroup;
        ConfigGroup SemiFinalGroup;
        string ChampionshipGroupName = "ChampionshipGroup";
        string SemiFinalGroupName = "SemiFinalGroup";

        ConfigCompetition RegularSeason;

        League MyLeague;
        ConfigCompetition Playoffs;

        ConfigSeriesRule PremierQualificationSeriesRule, ChampionshipSeriesRule;
        ConfigSeriesRule Division1QualificationSeriesRule;
        ConfigSeriesRule SemiFinal1Rule, SemiFinal2Rule;
        string ChampionshipSeriesName = "Final";
        string SemiFinal1SeriesName = "Semi Final 1";
        string SemiFinal2SeriesName = "Semi Final 2";

        public override void PrivateCreateConfigDivisions()
        {
            League = CreateAndAddConfigDivision(MyLeague, RegularSeason, "League", null, 0, 1, null, null, 1, null);
            Premier = CreateAndAddConfigDivision(MyLeague, RegularSeason, "Premier", null, 1, 1, League, null, 1, null);
            Division1 = CreateAndAddConfigDivision(MyLeague, RegularSeason, "Division1", null, 1, 2, League, null, 1, null);            
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules()
        {
            ConfigScheduleRule rule1, rule2;

            rule1 = ConfigScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 1", Premier, true, 5,1, false, 1, null);
            rule2 = ConfigScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 2", Division1, true, 5, 1, false, 1, null);

            CreateAndAddScheduleRule(rule1);
            CreateAndAddScheduleRule(rule2);

        }

        public override void PrivateCreateConfigTeams()
        {
            Boston = CreateAndAddConfigTeam("Boston", 5, Premier, MyLeague, 1, null);                
            Calgary = CreateAndAddConfigTeam("Calgary", 5, Division1, MyLeague, 1, null);
            Chicago = CreateAndAddConfigTeam("Chicago", 5, Premier, MyLeague, 1, null);
            Edmonton = CreateAndAddConfigTeam("Edmonton", 5, Division1, MyLeague, 1, null);
            Montreal = CreateAndAddConfigTeam("Montreal", 5, Premier, MyLeague, 1, null);
            Minnesota = CreateAndAddConfigTeam("Minnesota", 5, Division1, MyLeague, 1, null);
            NewYork = CreateAndAddConfigTeam("New York", 5, Premier, MyLeague, 1, null);
            Ottawa = CreateAndAddConfigTeam("Ottawa", 5, Division1, MyLeague, 1, null);
            QuebecCity = CreateAndAddConfigTeam("Quebec City", 5, Division1, MyLeague, 1, null);
            Toronto = CreateAndAddConfigTeam("Toronto", 5, Premier, MyLeague, 1, null);
            Vancouver = CreateAndAddConfigTeam("Vancouver", 5, Division1, MyLeague, 1, null);
            Winnipeg = CreateAndAddConfigTeam("Winnipeg", 5, Premier, MyLeague, 1, null);
        }

        public override void PrivateCreateConfigSortingRules()
        {
            CreateAndAddConfigSortingRule(League, "Sorting Rule 1", 0, Premier,  null, 0, -1, 1, null);
            CreateAndAddConfigSortingRule(League, "Sorting Rule 2", 1, Division1, null, 0,-1, 1, null);
        }
        public override void PrivateCreateLeagues()
        {
            MyLeague = CreateAndAddLeague(LeagueName);
        }

        public override void PrivateCreateConfigCompetitions()
        {
            RegularSeason = CreateAndAddConfigCompetition(MyLeague, RegularSeasonName, ConfigCompetition.SEASON, null, 1, 1, null);
            Playoffs = CreateAndAddConfigCompetition(MyLeague, PlayoffName, ConfigCompetition.PLAYOFF, RegularSeason, 2, 1, null);
        }

        public override void PrivateCreateConfigPlayoffs()
        {
            return;
        }

        public override void PrivateCreateConfigSeriesRules()
        {
            PremierQualificationSeriesRule = CreateAndAddConfigSeriesRule(Playoffs, "Qualification", 1, PremierQualificationGroup, 1, PremierQualificationGroup, 2, SeriesRule.TYPE_BEST_OF, 4, false, SeriesRule.SEVEN_GAME_SERIES_HOME_GAMES, 1, null);
            ChampionshipSeriesRule = CreateAndAddConfigSeriesRule(Playoffs, ChampionshipSeriesName, 2, ChampionshipGroup, 1, ChampionshipGroup, 2, SeriesRule.TYPE_BEST_OF, 4, false,SeriesRule.SEVEN_GAME_SERIES_HOME_GAMES, 1, null);

            
        }

        public override void PrivateCreateConfigGroups()
        {
            PremierQualificationGroup = CreateAndAddConfigGroup(Playoffs, "Premier Qualification Group", League, 1, null);
            ChampionshipGroup = CreateAndAddConfigGroup(Playoffs, ChampionshipGroupName, Premier, 1, null);
        }

        public override void PrivateCreateConfigGroupRules()
        {
            CreateAndAddConfigGroupRule(ConfigGroupRule.CreateFromDivisionBottom(PremierQualificationGroup, "Group Rule 1", Premier, 1, 1, 1, null));
            CreateAndAddConfigGroupRule(ConfigGroupRule.CreateFromDivision(PremierQualificationGroup, "Group Rule 2", Division1, 1, 1, 1, null));
            CreateAndAddConfigGroupRule(ConfigGroupRule.CreateFromDivision(ChampionshipGroup, "Group Rule 3", Premier, 1, 2, 1, null));

        }
        /*
        //how to add a new division
        public void RunUpdate1()
        {
            MyLeague = db.Leagues.Where(l => l.Name == LeagueName).First();
            RegularSeason = (Season)competitionService.GetReferenceCompetitionByName(MyLeague, RegularSeasonName);
            League = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "League");

            Division2 = new Division(MyLeague, RegularSeason, "Division2", null, 1, 2, League);
            db.Divisions.Add(Division2);

            Colorado = teamService.CreateTeam("Colorado", 1, Division2);
            Pittsburgh = teamService.CreateTeam("Pittsburgh", 1, Division2);
            Philadelphia = teamService.CreateTeam("Philadelphia", 1, Division2);
            NewJersey = teamService.CreateTeam("New Jersey", 1, Division2);
            Hamilton = teamService.CreateTeam("Hamilton", 1, Division2);
            Nashville = teamService.CreateTeam("Nashville", 1, Division2);

            //may not be needed if no division specific schedule rules are needed
            ConfigScheduleRule division2ScheduleRule = ConfigScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 3", Division2, true, 5, 1, false);
            db.ScheduleRules.Add(division2ScheduleRule);

            //where does it fit in when sorting?  They will be "rest of" if nothing is specified
            //here we are sorting by League so we have to put all members of this division behind all members of other divisions, so this is Group 2 (0, 1, 2)
            SortingRule division2SortingRule = new SortingRule()
            {
                Division = League,
                DivisionToGetTeamsFrom = Division2,
                Name = "Sorting Rule 3",
                GroupNumber = 2,
                PositionsToUse = null,
                DivisionLevel = 0,
                Type = -1

            };            
            db.SortingRules.Add(division2SortingRule);

            db.SaveChanges();
        }

        //how to add a new playoff series
        public void RunUpdate2()
        {
            MyLeague = db.Leagues.Where(l => l.Name == LeagueName).First();
            RegularSeason = (Season)competitionService.GetReferenceCompetitionByName(MyLeague, RegularSeasonName);
            Playoffs = (Playoff)competitionService.GetReferenceCompetitionByName(MyLeague, PlayoffName);
            League = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "League");
            Division1 = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "Division1");
            Division2 = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "Division2");
            
            //First create the group that will determine the teams
            Division1QualificationGroup = new Group("Division 1 Qualification Group", Playoffs, League, new List<GroupRule>());
            GroupRule.CreateFromDivisionBottom(Division1QualificationGroup, "D1Q Rule 1", Division1, 1, 1);
            GroupRule.CreateFromDivision(Division1QualificationGroup, "D1Q Rule 2", Division2, 1, 1);            

            //add the groups to prevent the multiplicity issue
            db.Groups.Add(Division1QualificationGroup);

            //next create the series rule
            Division1QualificationSeriesRule = new SeriesRule(Playoffs, "D1 Qualification", 1, Division1QualificationGroup, 1, Division1QualificationGroup, 2, SeriesRule.TYPE_BEST_OF, 4, false, SeriesRule.SEVEN_GAME_SERIES_HOME_GAMES);
            db.SeriesRules.Add(Division1QualificationSeriesRule);
            //do any other series need modifications?  Does the winner/loser go somewhere else, do other rounds need to be bumped or changed?

            db.SaveChanges();


        }

        public void RunUpdate3()
        {            
            MyLeague = db.Leagues.Where(l => l.Name == LeagueName).First();
            Playoffs = (Playoff)competitionService.GetReferenceCompetitionByName(MyLeague, PlayoffName);
            RegularSeason = (Season)competitionService.GetReferenceCompetitionByName(MyLeague, RegularSeasonName);

            ChampionshipSeriesRule = Playoffs.SeriesRules.Where(sr => sr.Name == ChampionshipSeriesName).First();
            ChampionshipGroup = Playoffs.Groups.Where(gr => gr.Name == ChampionshipGroupName).First();

            League = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "League");
            Premier = divisionService.GetByLeagueAndSeasonAndName(MyLeague, RegularSeason, "Premier");

            SemiFinalGroup = new Group(SemiFinalGroupName, Playoffs, Premier, new List<GroupRule>());
            GroupRule.CreateFromDivision(SemiFinalGroup, "SF Group Rule 1", Premier, 1, 4);

            db.Groups.Add(SemiFinalGroup);

            SemiFinal1Rule = new SeriesRule(Playoffs, SemiFinal1SeriesName, 2, SemiFinalGroup, 1, SemiFinalGroup, 4,
                SeriesRule.TYPE_BEST_OF, 4, false, SeriesRule.SEVEN_GAME_SERIES_HOME_GAMES);
            SemiFinal2Rule = new SeriesRule(Playoffs, SemiFinal2SeriesName, 2, SemiFinalGroup, 2, SemiFinalGroup, 3,
                SeriesRule.TYPE_BEST_OF, 4, false, SeriesRule.SEVEN_GAME_SERIES_HOME_GAMES);

            db.SeriesRules.AddRange(new SeriesRule[] { SemiFinal1Rule, SemiFinal2Rule });

            ChampionshipSeriesRule.Round = 3;
            ChampionshipGroup.GroupRules[0].FromDivision = null;
            ChampionshipGroup.GroupRules[0].FromSeries = SemiFinal1SeriesName;
            ChampionshipGroup.GroupRules[0].RuleType = GroupRule.FROM_SERIES;
            ChampionshipGroup.GroupRules[0].FromStartValue = GroupRule.SERIES_WINNER;
            GroupRule.CreateFromSeriesWinner(ChampionshipGroup, "CH Grp Rule 2", SemiFinal2SeriesName);

            var errorMessages = new List<string>();

            bool valid = true;
            valid = valid && SemiFinalGroup.ValidateConfiguration(errorMessages);
            valid = valid && ChampionshipGroup.ValidateConfiguration(errorMessages);

            errorMessages.ForEach(s => Console.WriteLine(s));

            if (!valid)
            {
                competitionService.Rollback();
            }
            else
            {
                db.SaveChanges();
            }

            db.SaveChanges();


        }
        */
        public override void UpdateData()
        {

            //DeleteAllData();
            //InsertData();
            //RunUpdate1();
            //RunUpdate2();
            //RunUpdate3();
            //RunUpdate4();
            //RunUpdate5();
            //RunUpdate6();                  
            //RunUpdate7();
 


        }

        public override void PrivateCreateTeams()
        {
            return;
        }

        public override void PrivateCreateDivisions()
        {
            return;
        }

        public override void PrivateCreateSortingRules()
        {
            return;
        }

        public override void PrivateCreateSeriesRules()
        {
            return;
        }

        public override void PrivateCreateGroups()
        {
            return;
        }

        public override void PrivateCreateGroupRules()
        {
            return;
        }

        public override void PrivateCreatePlayoffs()
        {
            return;
        }

        public override void PrivateCreateSeries()
        {
            return;
        }

        public override void PrivateCreateSeasons()
        {
            throw new NotImplementedException();
        }
    }
}

