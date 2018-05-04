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
        
        SeriesRule Playin1, Playin2, PlayinF, QF1Rule, QF2Rule, SF1Rule, SF2Rule, FinalRule;
        string Pool2Name = "The Rest";
        string Pool1Name = "Division Winers";
        string SFPoolName = "Quarter Final Winners";
        string FinalPoolName = "Semi Final Winners";
        string PlayinPool = "Play in Pool";
        string PNPWinners = "Play in Pool Winners";
        string PNPChampion = "Play in Pool Champ";

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Division> divs)
        {
            League = CreateAndAddDivision(leagues[LeagueName], "League", null, 0, 1, null, null, divs);
            WestConference = CreateAndAddDivision(leagues[LeagueName], "Western Confterence", "Western", 1, 1, League, null, divs);
            EastConference = CreateAndAddDivision(leagues[LeagueName], "Eastern Conference", "Eastern", 1, 2, League, null, divs);
            CentralConference = CreateAndAddDivision(leagues[LeagueName], "Central Conference", "Central", 1, 3, League, null, divs);
            WestDivision = CreateAndAddDivision(leagues[LeagueName], "West", "West", 2, 1, WestConference, null, divs);
            EastDivision = CreateAndAddDivision(leagues[LeagueName], "East", "East", 2, 2, EastConference, null, divs);
            CentralDivision = CreateAndAddDivision(leagues[LeagueName], "Central", "Central", 2, 3, CentralConference, null, divs);
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {
            ScheduleRule rule1, rule2, rule3, rule4;

            rule1 = ScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], "Rule 1", League, true, 2,1);
            rule2 = ScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], "Rule 2", WestDivision, true, 5, 1);
            rule3 = ScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], "Rule 3", EastDivision, true, 5, 1);
            rule4 = ScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], "Rule 4", CentralDivision, true, 5, 1);
            rule4 = ScheduleRule.CreateByDivisionLevel(leagues[LeagueName], "Rule 4", 1, true, 5, 1);

            CreateAndAddRule(rule1, rules);
            CreateAndAddRule(rule2, rules);
            CreateAndAddRule(rule3, rules);
            CreateAndAddRule(rule4, rules);
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
            League league;

            league = CreateAndAddLeague(LeagueName, leagues);
        }

        public override void PrivateCreateSeriesRules(Dictionary<string, League> leagues, Dictionary<string, SeriesRule> rules)
        {
            Playin1 = CreateAndAddSeriesRule(leagues[LeagueName], "Play in 1", 1, PlayinPool, 1, PlayinPool, 4, SeriesRule.TYPE_BEST_OF, 1, false, "1", rules);
            Playin2 = CreateAndAddSeriesRule(leagues[LeagueName], "Play in 2", 1, PlayinPool, 2, PlayinPool, 3, SeriesRule.TYPE_BEST_OF, 1, false, "1", rules);
            PlayinF = CreateAndAddSeriesRule(leagues[LeagueName], "Play in Final", 2, PNPWinners, 1, PNPWinners, 2, SeriesRule.TYPE_BEST_OF, 1, false, "1", rules);
            QF1Rule = CreateAndAddSeriesRule(leagues[LeagueName], "Quarter Final 1", 3, Pool1Name, 3, PNPChampion, 1, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            QF2Rule = CreateAndAddSeriesRule(leagues[LeagueName], "Quarter Final 2", 3, Pool2Name, 1, Pool2Name, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            SF1Rule = CreateAndAddSeriesRule(leagues[LeagueName], "Semi Final 1", 4, Pool1Name, 1, SFPoolName, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            SF2Rule = CreateAndAddSeriesRule(leagues[LeagueName], "Semi Final 2", 4, Pool1Name, 2, SFPoolName, 1, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);
            FinalRule = CreateAndAddSeriesRule(leagues[LeagueName], "Final", 5, FinalPoolName, 1, FinalPoolName, 2, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,1", rules);

            
        }

        public override void PrivateCreateGroupRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool1Name, League, EastDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool1Name, League, WestDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool1Name, League, CentralDivision, 1, 1), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool2Name, League, EastDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool2Name, League, WestDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], Pool2Name, League, CentralDivision, 2, 3), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], SFPoolName, QF1Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], SFPoolName, QF2Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], FinalPoolName, SF1Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], FinalPoolName, SF2Rule.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(leagues[LeagueName], PlayinPool, League, League, 6, 9), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], PNPWinners, Playin1.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], PNPWinners, Playin2.Name, League), rules);
            CreateAndAddGroupRule(GroupRule.CreateFromSeriesWinner(leagues[LeagueName], PNPChampion, PlayinF.Name, League), rules);


        }

    }
}

