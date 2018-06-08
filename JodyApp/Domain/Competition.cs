using JodyApp.Database;
using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    
    public interface Competition
    {        
        int Year { get; set; }
        int StartingDay { get; set; }
        bool Complete { get; set; }
        bool Started { get; set; }
        League League { get; set; }
        string Name { get; set; }
        List<Game> GetNextGames(int lastGameNumber);
        bool IsComplete();
        void PlayGame(Game g, Random random);
        void PlayGames(List<Game> games, Random random);
        void ProcessGame(Game g);
        void StartCompetition();        
        //void FinishCompetition();
        List<Game> Games { get; set; }
    
    }
}
