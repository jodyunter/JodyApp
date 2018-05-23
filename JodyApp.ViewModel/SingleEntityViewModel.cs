using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public abstract class SingleEntityViewModel:BaseViewModel
    {

        public abstract void SetById(int id);


        public abstract string[] GetInputFields();
        public abstract void CreateModelFromInput(Dictionary<string, string> inputs);

    }
}
