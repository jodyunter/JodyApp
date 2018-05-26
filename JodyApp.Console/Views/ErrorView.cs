using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class ErrorView : BaseView
    {
        public string Message { get; set; }

        public ErrorView(string message):base(null) { Message = message; }
        public override string GetView()
        {
            return Message;
        }
    }
}
