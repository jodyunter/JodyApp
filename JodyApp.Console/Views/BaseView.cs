using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public abstract class BaseView
    {
        public BaseViewModel ViewModel { get; set; }
        public abstract string GetDisplayString();
        public abstract void CreateViewModel();

        public BaseView()
        {
            CreateViewModel();
        }
        
    }
}
