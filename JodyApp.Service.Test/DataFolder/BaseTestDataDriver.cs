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

        public override void PrivateCreateSeasons(Dictionary<string, League> leagues, Dictionary<string, Season> seasons)
        {
            CreateAndAddSeason(leagues[LeagueName], "My Season", seasons, 1);
        }

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League",null, 0, 1, null, null, divs);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {
            CreateAndAddScheduleRule(leagues[LeagueName], seasons["My Season"], "Rule 1", ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false, 10, 0, 1, false, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {
            CreateAndAddTeam("Los Angelas", 5, divs["League"], teams);
            CreateAndAddTeam("Seattle", 5, divs["League"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["League"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["League"], teams);
        }

        public override void PrivateCreateSortingRules(Dictionary<string, Division> divs, Dictionary<string, SortingRule> rules)
        {
            //no special sorting rules by default
            return;
        }

        public override void PrivateCreateSeriesRules(Dictionary<string, Playoff> playoffs, Dictionary<string, SeriesRule> rules)
        {
            return;
        }

        public override void PrivateCreateGroupRules(Dictionary<string, Playoff> playoffs, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules)
        {
            return;
        }

        public override void PrivateCreatePlayoffs(Dictionary<string, League> leagues, Dictionary<string, Playoff> playoffs)
        {
            return;
        }
    }
}

