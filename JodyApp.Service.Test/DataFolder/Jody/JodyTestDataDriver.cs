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
        public JodyTestDataDriver(JodyAppContext db) : base(db) { }

        Division League, WestConference, EastConference, CentralConference;
        Division WestDivision, EastDivision, CentralDivision;
        Team Toronto, Montreal, Ottawa;
        Team Vancouver, Edmonton, Calgary;
        Team Winnipeg, Minnesota, Chicago;
        Season RegularSeason;
        League MyLeague;
        Playoff Playoffs;
        
        SeriesRule QF1Rule, QF2Rule, SF1Rule, SF2Rule, FinalRule;
        string Pool2Name = "The Rest";
        string Pool1Name = "Division Winers";
        string SFPoolName = "Quarter Final Winners";
        string FinalPoolName = "Semi Final Winners";

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {
            League = CreateAndAddDivision(MyLeague, RegularSeason, "League", null, 0, 1, null, null, divs);
            WestConference = CreateAndAddDivision(MyLeague, RegularSeason, "Western Confterence", "Western", 1, 1, League, null, divs);
            EastConference = CreateAndAddDivision(MyLeague, RegularSeason, "Eastern Conference", "Eastern", 1, 2, League, null, divs);
            CentralConference = CreateAndAddDivision(MyLeague, RegularSeason, "Central Conference", "Central", 1, 3, League, null, divs);
            WestDivision = CreateAndAddDivision(MyLeague, RegularSeason, "West", "West", 2, 1, WestConference, null, divs);
            EastDivision = CreateAndAddDivision(MyLeague, RegularSeason, "East", "East", 2, 2, EastConference, null, divs);
            CentralDivision = CreateAndAddDivision(MyLeague, RegularSeason, "Central", "Central", 2, 3, CentralConference, null, divs);
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {
            ScheduleRule rule1, rule2, rule3, rule4, rule5;

            rule1 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 1", League, true, 2,1);
            rule2 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 2", WestDivision, true, 5, 1);
            rule3 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 3", EastDivision, true, 5, 1);
            rule4 = ScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 4", CentralDivision, true, 5, 1);
            rule5 = ScheduleRule.CreateByDivisionLevel(MyLeague, RegularSeason, "Rule 5", 1, true, 5, 1);

            CreateAndAddScheduleRule(rule1, rules);
            CreateAndAddScheduleRule(rule2, rules);
            CreateAndAddScheduleRule(rule3, rules);
            CreateAndAddScheduleRule(rule4, rules);
            CreateAndAddScheduleRule(rule5, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {
            Toronto = CreateAndAddTeam("Toronto", 5, EastDivision, teams);
            Montreal = CreateAndAddTeam("Montreal", 5, EastDivision, teams);
            Ottawa = CreateAndAddTeam("Ottawa", 5, EastDivision, teams);
            Vancouver = CreateAndAddTeam("Vancouver", 5, WestDivision, teams);
            Edmonton = CreateAndAddTeam("Edmonton", 5, WestDivision, teams);
            Calgary = CreateAndAddTeam("Calgary", 5, WestDivision, teams);
            Winnipeg = CreateAndAddTeam("Winnipeg", 5, CentralDivision, teams);
            Minnesota = CreateAndAddTeam("Minnesota", 5, CentralDivision, teams);
            Chicago = CreateAndAddTeam("Chicago", 5, CentralDivision, teams);
        }

        public override void PrivateCreateSortingRules(Dictionary<string, Division> divs, Dictionary<string, SortingRule> rules)
        {
            CreateAndAddSortingRule(League, "Sorting Rule 1", 0, WestConference, "1", -1, -1, rules);
            CreateAndAddSortingRule(League, "Sorting Rule 2", 0, EastConference, "1", -1, -1, rules);
            CreateAndAddSortingRule(League, "Sorting Rule 3", 0, CentralConference, "1", -1, -1, rules);
        }
        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {
            MyLeague = CreateAndAddLeague(LeagueName, leagues);
        }

        public override void PrivateCreateSeasons(Dictionary<string, League> leagues, Dictionary<string, Season> seasons)
        {
            RegularSeason = CreateAndAddSeason(MyLeague, "Regular Season", seasons, 1);
        }

        public override void PrivateCreatePlayoffs(Dictionary<string, League> leagues, Dictionary<string, Playoff> playoffs)
        {
            Playoffs = CreateAndAddPlayoff(MyLeague, "Playoffs", playoffs, 2);
        }

        public override void PrivateCreateSeriesRules(Dictionary<string, Playoff> playoffs, Dictionary<string, SeriesRule> rules)
        {
            QF1Rule = CreateAndAddSeriesRule(Playoffs, "Quarter Final 1", 1, Pool1Name, 3, Pool2Name, 3, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            QF2Rule = CreateAndAddSeriesRule(Playoffs, "Quarter Final 2", 1, Pool2Name, 1, Pool2Name, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            SF1Rule = CreateAndAddSeriesRule(Playoffs, "Semi Final 1", 2, Pool1Name, 1, SFPoolName, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            SF2Rule = CreateAndAddSeriesRule(Playoffs, "Semi Final 2", 2, Pool1Name, 2, SFPoolName, 1, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            FinalRule = CreateAndAddSeriesRule(Playoffs, "Final", 3, FinalPoolName, 1, FinalPoolName, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);

            
        }

        public override void PrivateCreateGroupRules(Dictionary<string, Playoff> playoffs, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool1Name, League, EastDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool1Name, League, WestDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool1Name, League, CentralDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool2Name, League, EastDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool2Name, League, WestDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(Playoffs, Pool2Name, League, CentralDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(Playoffs, SFPoolName, QF1Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(Playoffs, SFPoolName, QF2Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(Playoffs, FinalPoolName, SF1Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(Playoffs, FinalPoolName, SF2Rule.Name, League), rules);


        }

    }
}

