using JodyApp.Domain;
using JodyApp.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class CompetitionService
    {
        public Season CreateNewSeason(ConfigCompetition referenceSeason, int year)
        {

            Season season = new Season();

            season.League = referenceSeason.League;
            season.Name = referenceSeason.Name;
            season.Year = year;

            Dictionary<string, Division> seasonDivisions = new Dictionary<string, Division>();
            Dictionary<string, Team> seasonTeams = new Dictionary<string, Team>();
            Dictionary<string, ConfigScheduleRule> seasonScheduleRules = new Dictionary<string, ConfigScheduleRule>();

            var activeConfigDivisions = ConfigService.GetDivisions(referenceSeason).Where(cd => cd.IsActive(year)).ToList();
            //loop once to create teams and new season divisions, order means we will not add a parent we haven't created yet
            activeConfigDivisions.OrderBy(d => d.Level).ToList().ForEach(configDivision =>
            {
                Division seasonDiv = season.CreateDivisionForSeason(configDivision);
                if (configDivision.Parent != null) seasonDiv.Parent = seasonDivisions[configDivision.Parent.Name];
                seasonDivisions.Add(seasonDiv.Name, seasonDiv);

            });

            activeConfigDivisions.ForEach(configDivision =>
                configDivision.Teams.Where(t => t.IsActive(year)).ToList().ForEach(dt =>
                {
                    Team seasonTeam = new Team(dt, seasonDivisions[configDivision.Name]);
                    seasonDivisions[configDivision.Name].Teams.Add(seasonTeam);
                    db.Teams.Add(seasonTeam);
                    seasonTeams.Add(seasonTeam.Name, seasonTeam);
                })
            );



            //need to change season rules too
            season.TeamData = seasonTeams.Values.ToList();

            db.Seasons.Add(season);
            db.Divisions.AddRange(seasonDivisions.Values);
            db.SaveChanges();

            seasonDivisions.Values.ToList().ForEach(seasonDiv =>
            {
                DivisionService.GetAllTeamsInDivision(seasonDiv).ForEach(team => { seasonDiv.SetRank(0, team); });

                db.DivisionRanks.AddRange(seasonDiv.Rankings);
            });

            var configRules = ConfigService.GetScheduleRulesByCompetition(referenceSeason).Where(rule => rule.IsActive(year)).ToList();

            season.Games = new List<Game>();
            ScheduleService.CreateGamesFromRules(configRules, seasonTeams, seasonDivisions, season.Games, 0);
            db.Games.AddRange(season.Games);

            season.SetupStandings();
            db.SaveChanges();

            return season;

        }
    }
}
