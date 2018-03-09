using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Service.DTO;

namespace JodyApp.Service
{
    public class DivisionService
    {
        public Division GetByName(String Name)
        {
            return null;
        }

        public Division GetById(int id)
        {
            return null;
        }

        public Division GetById(int? divisionId)
        {
            throw new NotImplementedException();
        }
        
        public void UpdateDivisionNameAndParent(DivisionDTO divisionDTO)
        {

        }

        public void UpdateDivisionTeamList(DivisionDTO divisionDTO)
        {

        }

        public List<DivisionDTO> GetDivisionList()
        {
            return null;
        }

        public Division GetDivisionWithTeamList()
        {
            return null;
        }

   
    }
}
