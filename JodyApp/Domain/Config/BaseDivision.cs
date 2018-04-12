using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class BaseDivision:Division
    {
        public BaseDivision() { }
        public BaseDivision(string name, int level, int order, Division parent) : base(name, level, order, parent) { }

        
    }
}
