﻿using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class TeamCommands:BaseViewCommands
    {
        public TeamCommands():base() { Service = new ConfigTeamService(); }

        public BaseView ListByDivision(ApplicationContext context, string divisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new TeamListView(service.GetTeamsByDivisionName(divisionName));

            view.Header = divisionName;

            return view;
        }
        
        public BaseView ChangeDivision(ApplicationContext context, int teamId, string newDivisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);

            service.ChangeDivision(service.GetTeamById(teamId), newDivisionName);

            service.Save();

            var model = service.GetTeamModelById(teamId);

            var view = new TeamView(model);

            return view;
        }

        public BaseView ListByLeague(ApplicationContext context, int leagueId = -55)
        {
            var searchId = -1;

            if (leagueId == -55)
            {
                searchId = (int)((LeagueViewModel)LeagueCommands.SelectLeague(context)).Id;
            }
            else
                searchId = (int)leagueId;

            return new TeamListView(((ConfigTeamService)Service).GetByLeague(searchId));
        
        }
        public override BaseView GetView(BaseViewModel model)
        {
            return new TeamView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new TeamListView(model);
        }

        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            var basicInput = IOMethods.GatherData(context, "New Team", new List<string> { "Name", "Skill", "First Year", "Last Year" });

            LeagueViewModel league = (LeagueViewModel)LeagueCommands.SelectLeague(context);
            ConfigDivisionViewModel division = (ConfigDivisionViewModel)DivisionCommands.SelectDivision(context, (int)league.Id);

            basicInput.Add("LeagueId", league.Id.ToString());
            basicInput.Add("LeagueName", league.Name);
            basicInput.Add("DivisionId", division.Id.ToString());
            basicInput.Add("DivisionName", division.Name);

            return basicInput;
        }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            int? id = null;
            string name = data["Name"];
            int skill = int.Parse(data["Skill"]);
            int leagueId = int.Parse(data["LeagueId"]);
            string leagueName = data["LeagueName"];
            int divisionId = int.Parse(data["DivisionId"]);
            string divisionName = data["DivisionName"];
            int? firstYear = GetNullableIntFromString(data["First Year"]);
            int? lastYear = GetNullableIntFromString(data["Last Year"]);

            var model = new ConfigTeamViewModel(id, name, skill, leagueId, leagueName, divisionId, divisionName, firstYear, lastYear);

            return model;
        }
    }
}
