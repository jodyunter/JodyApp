using System.Collections.Generic;

namespace JodyApp.ViewModel
{
    public class BaseViewModel
    {
        public bool Error { get; set; }
        public List<string> ErrorMessages { get; set; }

        public void AddError(string message)
        {
            Error = true;
            ErrorMessages.Add(message);
        }
    }
}