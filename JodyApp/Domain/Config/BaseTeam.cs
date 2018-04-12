using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class BaseTeam: Team
    {
        public BaseTeam() { }
        public BaseTeam(string name, int skill, Division div) : base(name, skill, div) { }
    }
}
