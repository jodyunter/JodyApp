using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Exceptions
{
    public class ConfigurationException:ApplicationException
    {
        public ConfigurationException(string message) : base(message) { }
        
    }
}
