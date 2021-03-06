﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using JodyApp.ViewModel;
using System.Data.Entity;

namespace JodyApp.Service.CompetitionServices
{
    public class ScheduleService : BaseService<DomainObject>
    {
        DivisionService DivisionService { get; set; }

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public ScheduleService(JodyAppContext db) : base(db)
        {
            DivisionService = new DivisionService(db);
        }


        //update to get last game number in database for season
        public List<Game> CreateGamesFromRules(List<ConfigScheduleRule> rules,
                    Dictionary<string, Team> teams,
                    Dictionary<string, Division> divisions,
                    List<Game> games, int lastGameNumber)
        {
            rules.ForEach(rule =>
            {
                lastGameNumber = CreateGamesFromRule(rule, teams, divisions, games, lastGameNumber);
            });

            return games;
        }

        public int CreateGamesFromRule(ConfigScheduleRule rule,
            Dictionary<string, Team> seasonTeams,
            Dictionary<string, Division> seasonDivisions,
            List<Game> games, int lastGameNumber)
        {

            var homeTeams = new List<Team>();
            var awayTeams = new List<Team>();

            if (rule.HomeType == ConfigScheduleRule.BY_DIVISION_LEVEL)
            {
                List<Division> divisions;

                divisions = seasonDivisions.Values.ToList().Where(d => d.Level == rule.DivisionLevel).ToList();

                divisions.ForEach(d =>
                {
                    homeTeams = new List<Team>();

                    var list = DivisionService.GetAllTeamsInDivision(d);
                    if (rule.Reverse) list.Reverse();
                    homeTeams.AddRange(list);

                });
            }
            else
            {
                var homeTeam = rule.HomeTeam == null ? null : seasonTeams[rule.HomeTeam.Name];
                var homeDivision = rule.HomeDivision == null ? null : seasonDivisions[rule.HomeDivision.Name];
                var awayTeam = rule.AwayTeam == null ? null : seasonTeams[rule.AwayTeam.Name];
                var awayDivision = rule.AwayDivision == null ? null : seasonDivisions[rule.AwayDivision.Name];

                AddTeamsToListFromRule(homeTeams, rule.HomeType, homeTeam, homeDivision, rule.Reverse);
                AddTeamsToListFromRule(awayTeams, rule.AwayType, awayTeam, awayDivision, rule.Reverse);                

            }

            for (int i = 0; i < rule.Rounds; i++)
            {
                lastGameNumber = Scheduler.ScheduleGames(games, lastGameNumber, homeTeams.ToArray(), awayTeams.ToArray(), rule.PlayHomeAway, 1);
            }

            return lastGameNumber;
        }

        public void AddTeamsToListFromRule(List<Team> teamList, int ruleType, Team team, Division division, bool reverse)
        {
            switch (ruleType)
            {
                case ConfigScheduleRule.BY_TEAM:
                    teamList.Add(team);
                    break;
                case ConfigScheduleRule.BY_DIVISION:
                    var list = DivisionService.GetAllTeamsInDivision(division);
                    if (reverse) list.Reverse();
                    teamList.AddRange(list);
                    break;
                case ConfigScheduleRule.NONE:
                    break;
            }
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

        public ScheduleViewModel GetScheduleByDay(DomainObject competitionObject, int? firstDay, int? lastDay)
        {
            var competition = (Competition)competitionObject;

            var games = GetModelForGames(
                db.Games.Where(g =>

               (competition != null &&
               (g.Season != null && g.Season.Id == competition.Id || g.Series != null && g.Series.Playoff.Id == competition.Id))
               ||
               (competition == null &&
               firstDay == null ? true : g.Day >= firstDay &&
               lastDay == null ? true : g.Day <= lastDay
               )
            ).ToList(), competition);

            return new ScheduleViewModel(new CompetitionViewModel(competition.Id, GetReferenceObject(competition.League), competition.Name, competition.Year, competition.Type, competition.Started, competition.Complete, competition.StartingDay),                
               games);
        }

        public ListViewModel GetModelForGames(List<Game> games, Competition competition)
        {
            var items = new List<GameViewModel>();

            games.ForEach(g =>
            {

                items.Add(GameToDTO(competition, g));
            });


            return new ListViewModel(items.ToList<BaseViewModel>());
        }


        public static GameViewModel GameToDTO(Competition competition, Game game)
        {
            var model = new GameViewModel(
                game.Id,
                GetReferenceObject(game.HomeTeam),
                GetReferenceObject(game.AwayTeam),                
                game.HomeScore,
                game.AwayScore,
                competition.Name,
                competition.Year,
                game.Day,
                game.GameNumber,
                game.Complete);

            return model;
        }
    }

}
