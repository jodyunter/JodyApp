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
        public JodyTestDataDriver(JodyAppContext db) : base() {  teamService = new TeamService(db); }

        Division League, Premier, Division1, Division2;
        Team Toronto, Montreal, Ottawa, NewYork, Boston, QuebecCity;
        Team Vancouver, Edmonton, Calgary;
        Team Winnipeg, Minnesota, Chicago;
        Team Colorado, Pittsburgh, Philadelphia, NewJersey, Hamilton, Nashville;
        Team Washington, Victoria, Columbus;
        Season RegularSeason;
        League MyLeague;
        Playoff Playoffs;
        
        SeriesRule QualificationRule, FinalRule;
        SeriesRule SemiFinal1, SemiFinal2;
        SeriesRule D1QualificationRule;
        string QualificationPool = "Qualification Pool";
        string FinalPool = "Final Pool";
        string SemiFinalPool = "Semi Final Pool";
        string D1QualPool = "Division 1 Qualification Pool";

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

        public override void PrivateCreateSeriesRules(Dictionary<string, Playoff> playoffs, Dictionary<string, SeriesRule> rules)
        {
            QualificationRule = CreateAndAddSeriesRule(Playoffs, "Qualification", 1, QualificationPool, 1, QualificationPool, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            FinalRule = CreateAndAddSeriesRule(Playoffs, "Final", 2, FinalPool, 1, FinalPool, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);

            
        }

        public override void PrivateCreateGroupRules(Dictionary<string, Playoff> playoffs, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            CreateAndAddGroupRule(GroupRule.CreateFromDivisionBottom(Playoffs, "Group Rule 1", QualificationPool, League, Premier, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, "Group Rule 2", QualificationPool, League, Division1, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, "Group Rule 3", FinalPool, Premier, Premier, 1, 2), rules);



        }
        
        public void RunUpdate1()
        {            


            Playoffs = db.Playoffs.Where(p => p.Year == 0).First();
            Premier = db.Divisions.Where(d => d.Name == "Premier" && d.Season.Year == 0).First();

            //create semi final pools            
            var SemiFinalGroup = GroupRule.CreateFromDivision(Playoffs, "Group Rule 4", SemiFinalPool, Premier, Premier, 1, 4);
            db.GroupRules.Add(SemiFinalGroup);

            //create semi final series rules
            SemiFinal1 = new SeriesRule(Playoffs, "Semi Final 1", 2, SemiFinalPool, 1, SemiFinalPool, 4, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1");
            SemiFinal2 = new SeriesRule(Playoffs, "Semi Final 2", 2, SemiFinalPool, 2, SemiFinalPool, 3, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1");

            db.SeriesRules.Add(SemiFinal1);
            db.SeriesRules.Add(SemiFinal2);

            //delete old final pool rule
            var FinalPoolRule = db.GroupRules.Where(gr => gr.Name == "Group Rule 3" && gr.Playoff.Year == 0).First();
            db.GroupRules.Remove(FinalPoolRule);

            //add new final pool rules
            db.GroupRules.Add(GroupRule.CreateFromSeriesWinner(Playoffs, "Group Rule 5", FinalPool, SemiFinal1.Name, Premier));
            db.GroupRules.Add(GroupRule.CreateFromSeriesWinner(Playoffs, "Group Rule 6", FinalPool, SemiFinal2.Name, Premier));

            FinalRule = db.SeriesRules.Where(s => s.Playoff.Year == 0 && s.Name == "Final").First();
            FinalRule.Round = 3;

            db.SaveChanges();
        }
        public void RunUpdate2()
        {
            Division1 = db.Divisions.Where(d => d.Name == "Division1" && d.Season.Year == 0).First();
            RegularSeason = Division1.Season;
            MyLeague = Division1.League;
            League = Division1.Parent;

            Division2 = new Division(MyLeague, RegularSeason, "Division2", null, 1, 3, League, null);
            db.Divisions.Add(Division2);

            var scheduleRule = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 3", Division2, true, 5, 1, false);
            db.ScheduleRules.Add(scheduleRule);

            Colorado = new Team("Colorado", 1, Division2);
            Pittsburgh = new Team("Pittsburgh", 1, Division2);
            Philadelphia = new Team("Philadelphia", 1, Division2);
            var teamList = new Team[] { Colorado, Pittsburgh, Philadelphia };
            Division2.Teams.AddRange(teamList);
            db.Teams.AddRange(teamList);
            
            SortingRule sortingRule = new SortingRule()
            {
                Division = League,
                Name = "Sorting Rule 3",
                GroupNumber = 2,
                DivisionToGetTeamsFrom = Division2,
                PositionsToUse = null,
                DivisionLevel = 0,
                Type = SortingRule.SINGLE_DIVISION
            };

            db.SortingRules.Add(sortingRule);

            db.SaveChanges();

        }

        public void RunUpdate3()
        {
            Division2 = db.Divisions.Where(d => d.Name == "Division2" && d.Season.Year == 0).First();            
            
            NewJersey = new Team("New Jersey", 1, Division2);            
            db.Teams.Add(NewJersey);

            db.SaveChanges();
            
        }

        public void RunUpdate4()
        {
            Division2 = db.Divisions.Where(d => d.Name == "Division2" && d.Season.Year == 0).First();

            Hamilton = new Team("Hamilton", 1, Division2);            
            db.Teams.Add(Hamilton);

            db.SaveChanges();

        }

        public void RunUpdate5()
        {
            Division2 = db.Divisions.Where(d => d.Name == "Division2" && d.Season.Year == 0).First();

            Nashville = new Team("Nashville", 1, Division2);
            Division2.Teams.Add(Nashville);
            db.Teams.Add(Nashville);

            db.SaveChanges();

        }

        public void RunUpdate6()
        {
            Division1 = db.Divisions.Where(d => d.Name == "Division1" && d.Season.Year == 0).First();
            Division2 = db.Divisions.Where(d => d.Name == "Division2" && d.Season.Year == 0).First();
            League = db.Divisions.Where(d => d.Name == "League" && d.Season.Year == 0).First();
            
            Playoffs = db.Playoffs.Where(p => p.Year == 0).First();

            var D1QualGroupRule1 = GroupRule.CreateFromDivisionBottom(Playoffs, "Group Rule 7", D1QualPool, League, Division1, 1, 1);
            var D1QualGroupRule2 = GroupRule.CreateFromDivision(Playoffs, "Group Rule 8", D1QualPool, League, Division2, 1, 1);
            db.GroupRules.Add(D1QualGroupRule1);
            db.GroupRules.Add(D1QualGroupRule2);

            //create semi final series rules
            D1QualificationRule = new SeriesRule(Playoffs, "Division 1 Qualification", 1, D1QualPool, 1, D1QualPool, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1");            
            db.SeriesRules.Add(D1QualificationRule);

            db.SaveChanges();

        }
        public void RunUpdate7()
        {

            Columbus = teamService.CreateTeam("Columbus", 1, "Division2");
            Victoria = teamService.CreateTeam("Victoria", 3, "Division2");

            var scheduleRule = db.ScheduleRules.Where(sc => sc.Name == "Rule 3" && sc.Season.Year == 0).First();
            scheduleRule.Rounds = 3;

            db.SaveChanges();
        }
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





    }
}

