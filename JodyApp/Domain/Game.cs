using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    [Table("Games")]
    public class Game:DomainObject
    {
        static int BASE_GAME_SCORE = 6;
        public int Day { get; set; }
        public Season Season { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }        
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool CanTie { get; set; }
        public bool Complete { get; set; }
        public int MaxOverTimePeriods { get; set; }
        //todo: implement Golden Goal or full OT Periods

        public void Play(Random random)
        {
            HomeScore = GetScore(HomeTeam, AwayTeam, random);
            AwayScore = GetScore(AwayTeam, HomeTeam, random);

            int currentOtPeriod = 1;

            while (IsOTPeriodRequired(currentOtPeriod))
            {
                PlayOTPeriod(random);

                currentOtPeriod++;
            }

            Complete = true;
        }

        public void PlayOTPeriod(Random random)
        {
            int hScore = GetScore(HomeTeam, AwayTeam, random);
            int aScore = GetScore(AwayTeam, HomeTeam, random);

            int score = hScore - aScore;

            if (score > 0) HomeScore++;
            else if (score < 0) AwayScore++;
            
        }

        public int GetScore(Team team, Team opponent, Random random)
        {
            return random.Next(BASE_GAME_SCORE + (team.Skill - opponent.Skill) / 2);
        }

        public bool IsOTPeriodRequired(int currentOtPeriod)
        {
            if (HomeScore == AwayScore)
            {
                if (!CanTie)
                {
                    //continue until someone wins
                    return true;
                }
                else if (currentOtPeriod > MaxOverTimePeriods)
                {
                    //stop if we've played the max number of OT periods
                    return false;
                }
                else
                {
                    //continue if we can tie but haven't played the max number of OT Periods
                    return true;
                }
            }
            else
            {
                return false;
            }
            
        }

        public Team GetWinner()
        {
            if (HomeScore > AwayScore) return HomeTeam;
            if (AwayScore > HomeScore) return AwayTeam;

            return null;
        }

        public Team GetLoser()
        {
            if (HomeScore > AwayScore) return AwayTeam;
            if (AwayScore > HomeScore) return HomeTeam;

            return null;
        }
    }
}
