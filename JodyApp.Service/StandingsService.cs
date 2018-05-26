﻿using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class StandingsService:BaseService
    {
        public SeasonService SeasonService { get; set; }
        public StandingsService(JodyAppContext db) : base(db) { SeasonService = new SeasonService(db); }
        public StandingsService(JodyAppContext db, SeasonService seasonService) : base(db) { SeasonService = seasonService; }

        public ListViewModel GetBySeasonAndDivisionName(Season season, string divisionName)
        {
            SeasonService.SortAllDivisions(season);
            var division = season.Divisions.Where(d => d.Name == divisionName).FirstOrDefault();
            var teams = SeasonService.GetTeamsInDivisionByRank(division);

            var recordModels = new List<BaseViewModel>();

            int i = 1;
            teams.ForEach(team =>
            {
                var model = new StandingsRecordViewModel(i, season.League.Name, division.Name, team.Name, team.Stats.Wins, team.Stats.Loses, team.Stats.Ties, team.Stats.Points, team.Stats.GamesPlayed, team.Stats.GoalsFor, team.Stats.GoalsAgainst, team.Stats.GoalDifference);
                recordModels.Add(model);
                i++;                
            });

            return new ListViewModel(recordModels);
        }
    }
}