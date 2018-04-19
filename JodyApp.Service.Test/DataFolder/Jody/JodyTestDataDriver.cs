using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder.Jody
{
    public class JodyTestDataDriver : AbstractTestDataDriver
    {
        public JodyTestDataDriver(JodyAppContext db) : base(db) { }

        Division League, WestConference, EastConference;
        Division WestDivision, EastDivision;
        Team Toronto, Montreal, Ottawa;
        Team Vancouver, Edmonton, Calgary;

        public override void PrivateCreateDivisions(Dictionary<string, BaseDivision> divs)
        {
            League = CreateAndAddDivision("League", null,0, 1, null, null, divs);
            WestConference = CreateAndAddDivision("Western Confterence", "Western", 1, 1, League, null, divs);
            EastConference = CreateAndAddDivision("Eastern Conference", "Eastern", 1, 2, League, null, divs);
            WestDivision = CreateAndAddDivision("West", "West", 2, 1, WestConference, null, divs);
            EastDivision = CreateAndAddDivision("East", "East", 2, 2, EastConference, null, divs);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, BaseScheduleRule> rules)
        {
            ScheduleRule rule1, rule2, rule3, rule4;

            rule1 = BaseScheduleRule.CreateByDivisionVsSelf("Rule 1", League, true, 2);
            rule2 = BaseScheduleRule.CreateByDivisionVsSelf("Rule 2", WestDivision, true, 5);
            rule3 = BaseScheduleRule.CreateByDivisionVsSelf("Rule 3", EastDivision, true, 5);
            rule4 = BaseScheduleRule.CreateByDivisionLevel("Rule 4", 1, true, 5);

            CreateAndAddRule(rule1, rules);
            CreateAndAddRule(rule2, rules);
            CreateAndAddRule(rule3, rules);
            CreateAndAddRule(rule4, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, BaseTeam> teams, Dictionary<string, BaseDivision> divs)
        {
            Toronto = CreateAndAddTeam("Toronto", 5, EastDivision, teams);
            Montreal = CreateAndAddTeam("Montreal", 5, EastDivision, teams);
            Ottawa = CreateAndAddTeam("Ottawa", 5, EastDivision, teams);
            Vancouver = CreateAndAddTeam("Vancouver", 5, WestDivision, teams);
            Edmonton = CreateAndAddTeam("Edmonton", 5, WestDivision, teams);
            Calgary = CreateAndAddTeam("Calgary", 5, WestDivision, teams);            
        }


    }
}

