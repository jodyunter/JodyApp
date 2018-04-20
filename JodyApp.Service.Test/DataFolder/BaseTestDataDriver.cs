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

namespace JodyApp.Service.Test.DataFolder
{
    public class BaseTestDataDriver : AbstractTestDataDriver
    {
        public String LeagueName = "Base League Name";

        public BaseTestDataDriver(JodyAppContext db) : base(db) { }

        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {
            League l = new League() { Name = LeagueName };
            leagues.Add(l.Name, l);
            
        
        }

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, ConfigDivision> divs)
        {
            CreateAndAddDivision(leagues[LeagueName], "League",null, 0, 1, null, null, divs);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigScheduleRule> rules)
        {
            CreateAndAddRule("Rule 1", ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false, 10, 0, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigDivision> divs)
        {
            CreateAndAddTeam("Los Angelas", 5, divs["League"], teams);
            CreateAndAddTeam("Seattle", 5, divs["League"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["League"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["League"], teams);
        }

        public override void PrivateCreateSortingRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, SortingRule> rules)
        {
            //no special sorting rules by default
            return;
        }
    }
}

