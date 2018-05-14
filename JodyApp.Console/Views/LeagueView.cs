﻿using JodyApp.Console.Views.Display;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class LeagueView : SingleEntityView
    {            
       
        public override string GetHeaderString()
        {
            return LeagueDisplay.GetHeaderStringForSingleEntity();
        }

        public override string GetDisplayStringNoHeader()
        {
            return LeagueDisplay.GetDisplayStringNoHeaderSingleEntity((LeagueViewModel)ViewModel);
        }

        public override void CreateViewModel()
        {
            ViewModel = new LeagueViewModel();
        }
    }
}