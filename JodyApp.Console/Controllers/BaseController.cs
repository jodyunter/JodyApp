using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Controllers
{
    public abstract class BaseController
    {
        public abstract void ParseInput(List<string> input, int offset);
    }
}
