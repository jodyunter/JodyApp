using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public abstract class DomainObject
    {
        [Key]
        public int? Id { get; set; } //can be null if new object
    }
}
