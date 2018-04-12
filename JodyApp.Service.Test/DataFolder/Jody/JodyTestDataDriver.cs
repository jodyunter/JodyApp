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
            League = CreateAndAddDivision("League", null,0, 1, null, divs);
            WestConference = CreateAndAddDivision("Western Confterence", "Western", 1, 1, League, divs);
            EastConference = CreateAndAddDivision("Eastern Conference", "Eastern", 1, 2, League, divs);
            WestDivision = CreateAndAddDivision("West", "West", 2, 1, WestConference, divs);
            EastDivision = CreateAndAddDivision("East", "East", 2, 2, EastConference, divs);
        }

        public override void PrivateCreateRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, BaseScheduleRule> rules)
        {
            CreateAndAddRule("Rule 1", ScheduleRule.BY_DIVISION, null, League, ScheduleRule.NONE, null, null, true, 2, 0, rules);
            CreateAndAddRule("Rule 2", ScheduleRule.BY_DIVISION, null, WestDivision, ScheduleRule.NONE, null, null, true, 5, 0, rules);
            CreateAndAddRule("Rule 3", ScheduleRule.BY_DIVISION, null, EastDivision, ScheduleRule.NONE, null, null, true, 5, 0, rules);
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

