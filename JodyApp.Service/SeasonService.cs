﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service
{
    public class SeasonService:BaseService
    {
        TeamService teamService;
        DivisionService divisionService;        

        public SeasonService(JodyAppContext context):base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);            
        }

        public Season CreateNewSeason(string name, int year)
        {
            Season season = new Season();

            season.Name = name;
            season.Year = year;

            Dictionary<string, SeasonDivision> seasonDivisions = new Dictionary<string, SeasonDivision>();            
            
            foreach (Division d in db.Divisions)
            {
                //in the event the parent is added in the recursive steps, we don't want to do it agian
                if (!seasonDivisions.ContainsKey(d.Name))
                {
                    seasonDivisions.Add(d.Name, CreateSeasonDivision(d, seasonDivisions));
                }
            }

            foreach(KeyValuePair<string, SeasonDivision> entity in seasonDivisions)
            {                
                Console.WriteLine(entity.Key);
            }

            foreach(Team t in db.Teams)
            {
                SeasonTeam team = new SeasonTeam(t, seasonDivisions[t.Division.Name]);                
                db.SeasonTeams.Add(team);
            }

            foreach(ScheduleRule rule in db.ScheduleRules)
            {
                SeasonScheduleRule seasonRule = new SeasonScheduleRule(season,
                    rule.HomeType,
                    db.Teams.Find(rule.HomeTeam),
                    seasonDivisions[db.Divisions.Find(rule.HomeDivision).Name],
                    rule.AwayType,
                    db.Teams.Find(rule.AwayTeam),
                    seasonDivisions[db.Divisions.Find(rule.AwayDivision).Name],
                    rule.PlayHomeAway);
            }

            //need to change season rules too
            db.Seasons.Add(season);
            db.SeasonDivisions.AddRange(seasonDivisions.Values);            
            db.SaveChanges();
            return season;
            
        }

        private SeasonDivision CreateSeasonDivision(Division d, Dictionary<string, SeasonDivision> seasonDivisions)
        {
            SeasonDivision division = new SeasonDivision();
            division.Name = d.Name;
            division.Level = d.Level;
            division.Order = d.Order;
            if (d.Parent != null)
            {
                //if the parent isn't there add it
                if (!seasonDivisions.ContainsKey(d.Parent.Name))
                {
                    SeasonDivision parent = CreateSeasonDivision(d.Parent, seasonDivisions);
                    seasonDivisions.Add(parent.Name, parent);
                }

                division.Parent = seasonDivisions[d.Parent.Name];
            }

            return division;
        }
            
    }
}
