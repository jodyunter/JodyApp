using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public abstract class ConfigurableDomainObject:DomainObject
    {

        public abstract void CheckForErrors(List<string> errorMessages);

        public bool ValidateConfiguration(List<string> errorMessages)
        {
            CheckForErrors(errorMessages);
            return errorMessages == null || errorMessages.Count == 0;
        }

        public void AddMessage(string formatString, List<string> errorMessages, params string[] info)
        {
            errorMessages.Add(String.Format(formatString, info));
        }
    }
}
