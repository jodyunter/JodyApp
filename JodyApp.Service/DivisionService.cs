using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Service.DTO;
using JodyApp.Database;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {            
        }
        public Division GetByName(String Name)
        {
            var query = from d in db.Divisions where d.Name.Equals(Name) select d;
            
            Division division = null;
            foreach (var d in query)
            {
                division = d;
            }

            return division;
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
