using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Database;

namespace JodyApp.Service.Test.DataFolder.DivisionTestData
{
    public class DivisionTestDataDriver:BaseTestDataDriver
    {

        public DivisionTestDataDriver(JodyAppContext db) : base(db) { }                    


        public override void PrivateCreateDivisions(Dictionary<string, BaseDivision> divs)
        {            
            CreateAndAddDivision("League", 0, 1, null, divs);
            CreateAndAddDivision("West", 1, 2, divs["League"], divs);
            CreateAndAddDivision("East", 1, 2, divs["League"], divs);
            CreateAndAddDivision("Pacific", 2, 1, divs["West"], divs);
            CreateAndAddDivision("Central", 2, 2, divs["West"], divs);
            CreateAndAddDivision("North West", 2, 3, divs["West"], divs);
            CreateAndAddDivision("North East", 2, 4, divs["East"], divs);
            CreateAndAddDivision("Atlantic", 2, 4, divs["East"], divs);

            
        }

        public override void PrivateCreateTeams(Dictionary<string, BaseTeam> teams, Dictionary<string, BaseDivision> divs)
        {            
            CreateAndAddTeam("Los Angelas", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Seattle", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["Central"], teams);
            CreateAndAddTeam("Colorado", 5, divs["Central"], teams);
            CreateAndAddTeam("Chicago", 5, divs["Central"], teams);
            CreateAndAddTeam("Edmonton", 5, divs["North West"], teams);
            CreateAndAddTeam("Calgary", 5, divs["North West"], teams);
            CreateAndAddTeam("Winnipeg", 5, divs["North West"], teams);
            CreateAndAddTeam("Toronto", 5, divs["North East"], teams);
            CreateAndAddTeam("Montreal", 5, divs["North East"], teams);
            CreateAndAddTeam("Ottawa", 5, divs["North East"], teams);
            CreateAndAddTeam("Quebec City", 5, divs["North East"], teams);
            CreateAndAddTeam("Boston", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("New York", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("Philadelphia", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("Detroit", 5, divs["Atlantic"], teams);

        }
    }
}

