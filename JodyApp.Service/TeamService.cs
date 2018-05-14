﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class TeamService:BaseService
    {
        DivisionService divisionService = new DivisionService();

        public override void Initialize(JodyAppContext db) { divisionService.db = db; divisionService.Initialize(db); }

        public TeamService(JodyAppContext context) : base(context) { Initialize(context); }

        public List<Team> GetBaseTeams()
        {

            return Team.GetBaseTeams(db);
        }

        public Team GetTeamByName(String name)
        {
            return Team.GetByName(db, name, null);
                        

        }

        public List<Team> GetTeamsBySeason(Season season)
        {
            return Team.GetTeams(db, season);
        }

        public void SetNewSkills(Random random)
        {
            GetBaseTeams().ForEach(team =>
            {
                int num = random.Next(0, 9);
                if (num < 2) team.Skill -= 1;
                if (num > 7) team.Skill += 1;
                if (team.Skill > 10) team.Skill = 10;
                if (team.Skill < 1) team.Skill = 1;
            });
        }

        public void ChangeDivision(Team team, string newDivisionName)
        {

            while (team.Parent != null)
            {
                team = team.Parent;
            }

            //not good enough for division            
            team.Division = db.Divisions.Where(d => d.Name == newDivisionName && d.Season.Year == 0).FirstOrDefault();

            db.SaveChanges();
        }

        public Team GetByName(string name)
        {
            return db.Teams.Where(t => t.Parent == null && t.Name == name).FirstOrDefault();
        }

        public Team CreateTeam(string name, int skill, string divisionName)
        {
            var team = CreateTeam(name, skill, (Division)null);
            ChangeDivision(team, divisionName);

            return team;
        }

        public Team CreateTeam(string name, int skill, Division div)
        {
            var team = new Team(name, skill, div);
            db.Teams.Add(team);
            return team;
        }
    }
}
