using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public interface BaseConfigItem
    {        
        //can be null so we can plan ahead
        int? FirstYear { get; set; }
        int? LastYear { get; set; }
    }
}
