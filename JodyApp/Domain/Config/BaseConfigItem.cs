using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public abstract class BaseConfigItem:DomainObject
    {
        public BaseConfigItem() { }
        public BaseConfigItem(int? firstYear, int? lastYear)
        {
            this.FirstYear = firstYear;
            this.LastYear = lastYear;
        }
        //can be null so we can plan ahead
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public bool IsActive(int currentYear)
        {
            if (FirstYear == null) return false;
            else
            {
                if (FirstYear > currentYear) return false;
                if (LastYear == null) return true;
                if (LastYear < currentYear) return false;
                else return true;
            }
        }
    }
}
