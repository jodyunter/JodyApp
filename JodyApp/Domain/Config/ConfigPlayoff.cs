using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigPlayoff:DomainObject, BaseConfigItem
    {
        virtual public League League { get; set; }        
        public string Name { get; set; }        
        virtual public ConfigSeason Season { get; set; }
        virtual public List<ConfigGroup> Groups { get; set; }        
        virtual public List<ConfigSeriesRule> SeriesRules { get; set; }    
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }    
    }
}
