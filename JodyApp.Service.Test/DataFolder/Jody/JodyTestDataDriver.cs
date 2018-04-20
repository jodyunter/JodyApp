using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Database;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;

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

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, ConfigDivision> divs)
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
        public override void PrivateCreateScheduleRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigScheduleRule> rules)
        {
            ScheduleRule rule1, rule2, rule3, rule4;

            rule1 = ConfigScheduleRule.CreateByDivisionVsSelf("Rule 1", League, true, 2);
            rule2 = ConfigScheduleRule.CreateByDivisionVsSelf("Rule 2", WestDivision, true, 5);
            rule3 = ConfigScheduleRule.CreateByDivisionVsSelf("Rule 3", EastDivision, true, 5);
            rule4 = ConfigScheduleRule.CreateByDivisionVsSelf("Rule 4", CentralDivision, true, 5);
            rule4 = ConfigScheduleRule.CreateByDivisionLevel("Rule 4", 1, true, 5);

            CreateAndAddRule(rule1, rules);
            CreateAndAddRule(rule2, rules);
            CreateAndAddRule(rule3, rules);
            CreateAndAddRule(rule4, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigDivision> divs)
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

        public override void PrivateCreateSortingRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, SortingRule> rules)
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
    }
}

