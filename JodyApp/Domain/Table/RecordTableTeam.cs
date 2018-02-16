using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Table
{
    public class RecordTableTeam:Team
    {
        public TeamStatitistics Stats { get; set; }
    }
}
