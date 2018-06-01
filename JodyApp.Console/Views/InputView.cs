using JodyApp.ConsoleApp.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class InputView:BaseView
    {
        public string[] Prompts { get; set; }
        public InputView(string dataContext, string[] prompts, BaseView editingView) : base(null) { Prompts = prompts; EditingView = editingView; }

        public string DataContext { get; set; }
        public string CurrentPrompt { get; set; }
        public BaseView EditingView { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public override string GetView()
        {
            Data = new Dictionary<string, string>();

            for (int i = 0; i < Prompts.Count(); i++)
            {
                Data[Prompts[i]] = Application.ReadFromConsole(CurrentPrompt, DataContext + ">" + Prompts[i] + ">");
            }
            
            return "OK";
            
        }
    }
}
