using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class StandingsViewModel:BaseViewModel
    {
        public string SeasonName { get; set; }
        public int Year { get; set; }
        public string StandingsName { get; set; }
        public List<StandingsRecordViewModel> Records { get; set; }

        public SeasonService seasonService { get; set; }
        public LeagueService leagueService { get; set; }

        public StandingsViewModel(JodyAppContext db)
        {
            seasonService = new SeasonService(db);
            leagueService = new LeagueService(db);
        }

        public void SetStandingsCurrentYear(string leagueName, string seasonName, string divisionName)
        {
            var league = leagueService.GetByName(leagueName);

            if (league == null)
            {
                AddError("Cannot find league.");
                return;
            }

            SetStandings(league, seasonName, league.CurrentYear, divisionName);
        }
        
        public void SetStandings(string leagueName, string seasonName, int year, string divisionName)
        {
            var league = leagueService.GetByName(leagueName);

            if (league == null)
            {
                AddError("Cannot find league.");
                return;
            }

            SetStandings(league, seasonName, league.CurrentYear, divisionName);
        }

        public void SetStandings(League league, string seasonName, int year, string divisionName)
        {
            var season = seasonService.GetSeason(league, seasonName, year);

            if (season == null)
            {
                AddError("Season Not Found.");
                return;
            }

            SeasonName = seasonName;
            Year = year;

            season.SetupStandings();
            seasonService.SortAllDivisions(season);

            var divisionToSortBy = season.Divisions.Where(d => d.Name == divisionName).FirstOrDefault();
            if (divisionToSortBy == null)
            {
                AddError("Cannot sort by division.");
                return;
            }
            
            var teams = seasonService.GetTeamsInDivisionByRank(divisionToSortBy);

            if (teams == null || teams.Count == 0)
            {
                AddError("No Teams in division.");
                return;
            }

            Records = new List<StandingsRecordViewModel>();

            teams.ForEach(team =>
            {
                Records.Add(
                    new StandingsRecordViewModel(
                        team.Name,
                        divisionToSortBy.Rankings.Where(r => r.Team.Name == team.Name).First().Rank,
                        team.Stats.Wins,
                        team.Stats.Loses,
                        team.Stats.Ties,
                        team.Stats.GamesPlayed,
                        team.Stats.Points,
                        team.Stats.GoalsFor,
                        team.Stats.GoalsAgainst,
                        team.Stats.GoalDifference
                        ));
            });


            Records = Records.OrderBy(r => r.Rank).ToList();

            StandingsName = divisionName;

        }


    }
}
