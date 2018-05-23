using System.Collections.Generic;

namespace JodyApp.ViewModel
{
    public abstract class BaseViewModel
    {
        protected Database.JodyAppContext db = new Database.JodyAppContext(Database.JodyAppContext.CURRENT_DATABASE);
        public bool Error { get; set; }
        public List<string> ErrorMessages { get; set; }

        public void AddError(string message)
        {
            Error = true;
            ErrorMessages.Add(message);
        }
        
    }
}