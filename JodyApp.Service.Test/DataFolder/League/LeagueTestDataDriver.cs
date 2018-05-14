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

        Division Division1;       

        Team Team1, Team2, Team3;
        
        Group ChampionshipGroup;
        string ChampionshipGroupName = "ChampionshipGroup";        

        Season RegularSeason;
        League MyLeague;
        Playoff Playoffs;
                
        SeriesRule ChampionshipSeriesRule;
        string ChampionshipSeriesName = "Final";

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {
            Division1 = CreateAndAddDivision(MyLeague, RegularSeason, "Division1", null, 0, 1, null, null, divs);            
        }

        //todo add league everywhere
        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ConfigScheduleRule> rules)
        {
            ConfigScheduleRule rule1;

            rule1 = ConfigScheduleRule.CreateByDivisionVsSelf(MyLeague, RegularSeason, "Rule 1", Division1, true, 5,1, false);            

            CreateAndAddScheduleRule(rule1, rules);            

        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {                                                                                 
            Team1 = CreateAndAddTeam("Team 1", 5, Division1, teams);
            Team2 = CreateAndAddTeam("Team 2", 5, Division1, teams);
            Team3 = CreateAndAddTeam("Team 3", 5, Division1, teams);
        }

        public override void PrivateCreateSortingRules(Dictionary<string, Division> divs, Dictionary<string, SortingRule> rules)
        {
            //CreateAndAddSortingRule(League, "Sorting Rule 1", 0, Premier,  null, 0, -1, rules);            
        }
        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {
            MyLeague = CreateAndAddLeague(LeagueName, leagues);
        }

        public override void PrivateCreateSeasons(Dictionary<string, League> leagues, Dictionary<string, Season> seasons)
        {
            RegularSeason = CreateAndAddSeason(MyLeague, RegularSeasonName, seasons, 1);
        }

        public override void PrivateCreatePlayoffs(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Playoff> playoffs)
        {
            Playoffs = CreateAndAddPlayoff(MyLeague, PlayoffName, playoffs, 2, RegularSeason);
        }

        public override void PrivateCreateSeriesRules(Dictionary<string, Playoff> playoffs, Dictionary<string, Group> groups, Dictionary<string, SeriesRule> rules)
        {            
            ChampionshipSeriesRule = CreateAndAddSeriesRule(Playoffs, ChampionshipSeriesName, 2, ChampionshipGroup, 1, ChampionshipGroup, 2, SeriesRule.TYPE_BEST_OF, 5, false,null, rules);

            
        }

        public override void PrivateCreateGroups(Dictionary<string, Playoff> playoffs, Dictionary<string, Division> divs, Dictionary<string, Group> groups)
        {            
            ChampionshipGroup = CreateAndAddGroup(Playoffs, ChampionshipGroupName, Division1, groups);
        }

        public override void PrivateCreateGroupRules(Dictionary<string, Group> groups, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            CreateAndAddGroupRule(GroupRule.CreateFromDivision(ChampionshipGroup, "Group Rule 1", Division1, 1, 2), rules);

        }

        public override void UpdateData()
        {
 


        }





    }
}

