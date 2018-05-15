using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain.Table;
using JodyApp.Domain.Playoffs;
using System.Data.Entity;

namespace JodyApp.Service.Test.DataFolder
{
    public class LeagueTestDataDriver : AbstractTestDataDriver
    {
        public static String LeagueName = "Jody League";
        public static String PlayoffName = "Playoffs";
        public static String RegularSeasonName = "Regular Season";

        public LeagueTestDataDriver() : base() { }
        public LeagueTestDataDriver(JodyAppContext db) : base(db) { }

        ConfigDivision Division1;       

        ConfigTeam Team1, Team2, Team3;
        
        ConfigGroup ChampionshipGroup;
        string ChampionshipGroupName = "ChampionshipGroup";        

        ConfigCompetition RegularSeason;
        League MyLeague;
        ConfigCompetition Playoffs;
                
        ConfigSeriesRule ChampionshipSeriesRule;
        string ChampionshipSeriesName = "Final";

        public override void PrivateCreateConfigDivisions()
        {
            Division1 = CreateAndAddConfigDivision(MyLeague, RegularSeason, "Division1", null, 0, 1, null, null, 1, null);            
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules()
        {
            ConfigScheduleRule rule1;

            rule1 = ConfigScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 1", Division1, true, 5,1, false);            

            CreateAndAddScheduleRule(rule1);            

        }

        public override void PrivateCreateTeams()
        {                                                                                 
            Team1 = CreateAndAddConfigTeam("Team 1", 5, Division1, MyLeague, 1, null);
            Team2 = CreateAndAddConfigTeam("Team 2", 5, Division1, MyLeague, 1, null);
            Team3 = CreateAndAddConfigTeam("Team 3", 5, Division1, MyLeague, 1, null);
        }

        public override void PrivateCreateSortingRules()
        {
            //CreateAndAddSortingRule(League, "Sorting Rule 1", 0, Premier,  null, 0, -1, rules);            
        }
        public override void PrivateCreateLeagues()
        {
            MyLeague = CreateAndAddLeague(LeagueName);
        }

        public override void PrivateCreateConfigCompetitions()
        {
            RegularSeason = CreateAndAddConfigCompetition(MyLeague, RegularSeasonName, ConfigCompetition.SEASON, null, 1, 1, 0);
            Playoffs = CreateAndAddConfigCompetition(MyLeague, PlayoffName, ConfigCompetition.SEASON, RegularSeason, 1, 1, 0);
        }

        public override void PrivateCreatePlayoffs()
        {
            return;
        }

        public override void PrivateCreateConfigSeriesRules()
        {            
            ChampionshipSeriesRule = CreateAndAddConfigSeriesRule(Playoffs, ChampionshipSeriesName, 2, ChampionshipGroup, 1, ChampionshipGroup, 2, SeriesRule.TYPE_BEST_OF, 5, false,null, 1, null);

            
        }

        public override void PrivateCreateConfigGroups()
        {            
            ChampionshipGroup = CreateAndAddConfigGroup(Playoffs, ChampionshipGroupName, Division1, 1, null);
        }

        public override void PrivateCreateConfigGroupRules()
        {
            CreateAndAddConfigGroupRule(ConfigGroupRule.CreateFromDivision(ChampionshipGroup, "Group Rule 1", Division1, 1, 2, 1, null));

        }

        public override void UpdateData()
        {
 


        }

        public override void PrivateCreateSeries()
        {
            return;
        }

        public override void PrivateCreateConfigSortingRules()
        {
            return;
        }

        public override void PrivateCreateConfigTeams()
        {
            return;
        }

        public override void PrivateCreateConfigPlayoffs()
        {
            return;
        }

        public override void PrivateCreateConfigSeasons()
        {
            return;
        }

        public override void PrivateCreateDivisions()
        {
            return;
        }

        public override void PrivateCreateSeriesRules()
        {
            return;
        }

        public override void PrivateCreateGroups()
        {
            return;
        }

        public override void PrivateCreateGroupRules()
        {
            return;
        }
    }
}

