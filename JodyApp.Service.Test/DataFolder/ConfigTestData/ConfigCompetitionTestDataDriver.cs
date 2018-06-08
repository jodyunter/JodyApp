using JodyApp.Domain;
using JodyApp.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.Test
{
    public class ConfigCompetitionTestDataDriver:SimpleTestDataDriver
    {
        League league1, league2;
        ConfigCompetition ref1;

        public override void PrivateCreateLeagues()
        {            
            league1 = CreateAndAddLeague("Test 1 League");
            league2 = CreateAndAddLeague("Test 2 League");
        }

        public override void PrivateCreateConfigCompetitions()
        {
            ref1 = CreateAndAddConfigCompetition(league1, "Season 1", ConfigCompetition.SEASON, null, 1, 1, null);
            CreateAndAddConfigCompetition(league1, "Season 2", ConfigCompetition.SEASON, null, 1, 1, null);
            CreateAndAddConfigCompetition(league1, "Playoff 1", ConfigCompetition.PLAYOFF, ref1, 1, 1, null);
        }
    }
}
