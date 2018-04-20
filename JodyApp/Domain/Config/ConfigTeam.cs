using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigTeam: Team
    {
        public ConfigTeam() { }
        public ConfigTeam(string name, int skill, Division div) : base(name, skill, div) { }
    }
}
