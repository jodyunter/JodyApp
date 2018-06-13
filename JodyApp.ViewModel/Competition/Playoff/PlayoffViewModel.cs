using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class PlayoffViewModel : CompetitionViewModel
    {
        public ListViewModel SeriesViews { get; set; }
        public PlayoffViewModel(int? id, int? leagueId, string league, string name, int year, string competitionType, bool started, bool complete, int startingDay, ListViewModel seriesViews)
            : base(id, leagueId, league, name, year, competitionType, started, complete, startingDay)
        {
            SeriesViews = seriesViews;
        }
    }
}
