using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Service.Test.DataFolder.Jody
{
    public class JodyTestDataDriver : AbstractTestDataDriver
    {
        String LeagueName = "Jody League";
        TeamService teamService;
        public JodyTestDataDriver() : base() { }
        public JodyTestDataDriver(JodyAppContext db) : base(db) {  teamService = new TeamService(db); }

        Division League, Premier, Division1;
        Division Division2;
        Team Toronto, Montreal, Ottawa, NewYork, Boston, QuebecCity;
        Team Vancouver, Edmonton, Calgary;
        Team Winnipeg, Minnesota, Chicago;
        Team Colorado, Pittsburgh, Philadelphia, NewJersey, Hamilton, Nashville;
        Team Washington, Victoria, Columbus, Seattle, SanJose, LosAngelas;
        Group PremierQualificationGroup;
        Group ChampionshipGroup;
        Season RegularSeason;
        League MyLeague;
        Playoff Playoffs;
                
        SeriesRule QualificationRule, FinalRule;
        SeriesRule SemiFinal1, SemiFinal2;
        SeriesRule D1QualificationRule;

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {
            League = CreateAndAddDivision(MyLeague, RegularSeason, "League", null, 0, 1, null, null, divs);
            Premier = CreateAndAddDivision(MyLeague, RegularSeason, "Premier", null, 1, 1, League, null, divs);
            Division1 = CreateAndAddDivision(MyLeague, RegularSeason, "Division1", null, 1, 2, League, null, divs);            
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {
            ScheduleRule rule1, rule2;

            rule1 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 1", Premier, true, 5,1, false);
            rule2 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 2", Division1, true, 5, 1, false);

            CreateAndAddScheduleRule(rule1, rules);
            CreateAndAddScheduleRule(rule2, rules);

        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {                                                                                 
            Boston = CreateAndAddTeam("Boston", 5, Premier, teams);
            Calgary = CreateAndAddTeam("Calgary", 5, Division1, teams);
            Chicago = CreateAndAddTeam("Chicago", 5, Premier, teams);
            Edmonton = CreateAndAddTeam("Edmonton", 5, Division1, teams);
            Montreal = CreateAndAddTeam("Montreal", 5, Premier, teams);
            Minnesota = CreateAndAddTeam("Minnesota", 5, Division1, teams);
            NewYork = CreateAndAddTeam("New York", 5, Premier, teams);
            Ottawa = CreateAndAddTeam("Ottawa", 5, Division1, teams);
            QuebecCity = CreateAndAddTeam("Quebec City", 5, Division1, teams);
            Toronto = CreateAndAddTeam("Toronto", 5, Premier, teams);
            Vancouver = CreateAndAddTeam("Vancouver", 5, Division1, teams);
            Winnipeg = CreateAndAddTeam("Winnipeg", 5, Premier, teams);
        }

        public override void PrivateCreateSortingRules(Dictionary<string, Division> divs, Dictionary<string, SortingRule> rules)
        {
            CreateAndAddSortingRule(League, "Sorting Rule 1", 0, Premier,  null, 0, -1, rules);
            CreateAndAddSortingRule(League, "Sorting Rule 2", 1, Division1, null, 0, -1, rules);
        }
        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {
            MyLeague = CreateAndAddLeague(LeagueName, leagues);
        }

        public override void PrivateCreateSeasons(Dictionary<string, League> leagues, Dictionary<string, Season> seasons)
        {
            RegularSeason = CreateAndAddSeason(MyLeague, "Regular Season", seasons, 1);
        }

        public override void PrivateCreatePlayoffs(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Playoff> playoffs)
        {
            Playoffs = CreateAndAddPlayoff(MyLeague, "Playoffs", playoffs, 2, RegularSeason);
        }

        public override void PrivateCreateSeriesRules(Dictionary<string, Playoff> playoffs, Dictionary<string, Group> groups, Dictionary<string, SeriesRule> rules)
        {
            QualificationRule = CreateAndAddSeriesRule(Playoffs, "Qualification", 1, PremierQualificationGroup, 1, PremierQualificationGroup, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            FinalRule = CreateAndAddSeriesRule(Playoffs, "Final", 2, ChampionshipGroup, 1, ChampionshipGroup, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);

            
        }

        public override void PrivateCreateGroups(Dictionary<string, Playoff> playoffs, Dictionary<string, Division> divs, Dictionary<string, Group> groups)
        {
            PremierQualificationGroup = CreateAndAddGroup(Playoffs, "Premier Qualification Group", League, groups);
            ChampionshipGroup = CreateAndAddGroup(Playoffs, "ChampionshipGroup", Premier, groups);
        }

        public override void PrivateCreateGroupRules(Dictionary<string, Group> groups, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            CreateAndAddGroupRule(GroupRule.CreateFromDivisionBottom(PremierQualificationGroup, "Group Rule 1", Premier, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(PremierQualificationGroup, "Group Rule 2", Division1, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(ChampionshipGroup, "Group Rule 3", Premier, 1, 2), rules);

        }

        //how to add a new division
        public void RunUpdate1()
        {
            MyLeague = db.Leagues.Where(l => l.Name == LeagueName).First();
            RegularSeason = db.Seasons.Where(s => s.League.Id == MyLeague.Id && s.Year == 0 && s.Name == "Regular Season").First();

            Division2 = new Division(MyLeague, RegularSeason, "Division2", null, 1, 2, League);
            db.Divisions.Add(Division2);

            Colorado = teamService.CreateTeam("Colorado", 1, Division2);
            Pittsburgh = teamService.CreateTeam("Pittsburgh", 1, Division2);
            Philadelphia = teamService.CreateTeam("Philadelphia", 1, Division2);
            NewJersey = teamService.CreateTeam("New Jersey", 1, Division2);
            Hamilton = teamService.CreateTeam("Hamilton", 1, Division2);
            Nashville = teamService.CreateTeam("Nashville", 1, Division2);

            //may not be needed if no division specific schedule rules are needed
            ScheduleRule division2ScheduleRule = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 3", Division2, true, 5, 1, false);
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
            Division2.SortingRules.Add(division2SortingRule);
            db.SortingRules.Add(division2SortingRule);

            db.SaveChanges();
        }
        public override void UpdateData()
        {

            //DeleteAllData();
            //InsertData();
            RunUpdate1();
            //RunUpdate2();
            //RunUpdate3();
            //RunUpdate4();
            //RunUpdate5();
            //RunUpdate6();                  
            //RunUpdate7();
 


        }





    }
}

