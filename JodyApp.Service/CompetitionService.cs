﻿using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class CompetitionService : BaseService
    {
        SeasonService seasonService = new SeasonService();
        PlayoffService playoffService = new PlayoffService();

        public CompetitionService():base(){}
        public CompetitionService(JodyAppContext db) : base(db) { }

        public override void Initialize()
        {
            seasonService.db = db;
            playoffService.db = db;
        }
        public Competition GetReferenceCompetitionByName(League league, string name)
        {
            var list = new List<Competition>();

            list.AddRange(league.ReferenceCompetitions.Where(rc => rc.Playoff != null).Select(rc => rc.Playoff));
            list.AddRange(league.ReferenceCompetitions.Where(rc => rc.Season != null).Select(rc => rc.Season));

            return list.Where(comp => comp.Name == name).FirstOrDefault();
        }

        public Competition CreateCompetition(Competition reference, int year)
        {
            if (reference is Season) return seasonService.CreateNewSeason((Season)reference, year);
            else if (reference is Playoff) return playoffService.CreateNewPlayoff((Playoff)reference, year);

            return null;
        }
    }
}
