﻿using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JodyApp.Service.CompetitionServices
{
    public class CompetitionService : BaseService<DomainObject>
    {


        SeasonService SeasonService { get; set; }
        PlayoffService PlayoffService { get; set; }
        LeagueService LeagueService { get; set; }
        ConfigDivisionService ConfigDivisionService { get; set; }        
        ScheduleService ScheduleService { get; set; }

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public CompetitionService(JodyAppContext db) : base(db)
        {
            LeagueService = new LeagueService(db);
            SeasonService = new SeasonService(db);
            PlayoffService = new PlayoffService(db);
            ConfigDivisionService = new ConfigDivisionService(db);
            ScheduleService = new ScheduleService(db);
        }

        public Competition StartNextYear(int leagueId)
        {
            var league = (League)LeagueService.GetById(leagueId);

            if (LeagueService.IsYearDone(league))
            {
                league.CurrentYear++;
                LeagueService.Save();
                return GetNextCompetition(league);
            }
            else
            {
                return null;
            }
        }
        public Competition GetNextCompetition(League league)
        {
            if (LeagueService.IsYearDone(league))
            {
                return null;
            }
            else
            {
                //get all the possible competitions for this league
                var referenceComps = league.GetActiveConfigCompetitions();

                Competition currentComp = null;

                for (int i = 0; i < referenceComps.Count; i++)
                {
                    //does the current year version exist?                
                    //if yes, is it complete?
                    //if yes, move on, we alreayd know at least one is not complete
                    //if no return it
                    //if no return it
                    ConfigCompetition rc = referenceComps[i];

                    switch (rc.Type)
                    {
                        case ConfigCompetition.SEASON:
                            currentComp = db.Seasons.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;
                        case ConfigCompetition.PLAYOFF:
                            currentComp = db.Playoffs.Where(s => s.Year == league.CurrentYear && s.Name == rc.Name).FirstOrDefault();
                            break;

                    }

                    //if there is no competition for this one, create one and return it
                    if (currentComp == null)
                        return CreateCompetition(rc, league.CurrentYear);
                    //if there is a current competition, and it is not complete, return it
                    else if (!currentComp.Complete)
                        return currentComp;

                }

                return currentComp;
            }
        }



        public Competition CreateCompetition(ConfigCompetition reference, int year)
        {
            var service = GetServiceByType(reference.Type);

            if (service == null) return null;
            else return service.CreateCompetition(reference, year);

        }

        public List<Game> GetNextGames(Competition competition)
        {
            //need to get next game number
            return competition.GetNextGames(0);
        }

        public List<Game> GetGames(Competition competition)
        {
            return competition.Games;
        }

        public List<Game> GetGames(Competition competition, string teamName)
        {
            return competition.Games.Where(g => g.HomeTeam.Name.Equals(teamName) || g.AwayTeam.Name.Equals(teamName)).ToList();
        }
        public ListViewModel GetModelForNextGames(int competitionId, string type)
        {
            var competition = (Competition)GetServiceByType(type).GetById(competitionId);

            return ScheduleService.GetModelForGames(GetNextGames(competition), competition);
        }

        public ListViewModel GetModelForGames(int competitionId, string type)
        {
            var competition = (Competition)GetServiceByType(type).GetById(competitionId);

            return ScheduleService.GetModelForGames(GetGames(competition), competition);
        }

        public ICompetitionService GetServiceByType(string type)
        {
            if (type == ConfigCompetitionViewModel.SEASON) return SeasonService;
            else if (type == ConfigCompetitionViewModel.PLAYOFF) return PlayoffService;

            return null;
        }

        public ICompetitionService GetServiceByType(int type)
        {
            if (type == ConfigCompetition.SEASON) return SeasonService;
            else if (type == ConfigCompetition.PLAYOFF) return PlayoffService;

            return null;
        }

        public ListViewModel GetModelForGames(int competitionId, string teamName, string type)
        {
            var competition = (Competition)GetServiceByType(type).GetById(competitionId);

            return ScheduleService.GetModelForGames(GetGames(competition, teamName), competition);
        }

        public void PlayGames(List<Game> games, Competition competition, Random random)
        {
            competition.PlayGames(games, random);
            if (competition is Season)
            {
                SeasonService.SortAllDivisions((Season)competition);
            }
        }

        public ListViewModel GetAllByLeagueId(int leagueId, string type)
        {
            var service = GetServiceByType(type);

            if (service == null) return null;
            else return CreateListViewModelFromList(service.GetByLeagueId(leagueId), DomainToDTO);

        }



        public static BaseViewModel StaticDomainToDTO(DomainObject obj)
        {
            var competition = (Competition)obj;
            string competitionType = (competition is Season) ? "Season" : "Playoff";

            return new CompetitionViewModel(obj.Id, GetReferenceObject(competition.League), competition.Name, competition.Year, competitionType, competition.Started, competition.Complete, competition.StartingDay);
        }
        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            return StaticDomainToDTO(obj);
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

        
    }
}
