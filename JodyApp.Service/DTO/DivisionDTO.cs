using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.DTO
{
    public class DivisionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        List<TeamDTO> TeamList { get; set; }

        

    }
}
