using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Group:ConfigurableDomainObject
    {
        public string Name { get; set; }
        virtual public Playoff Playoff { get; set; }
        virtual public List<GroupRule> GroupRules { get; set; }
        virtual public Division SortByDivision { get; set; }

        //when validating make sure that all series group rules have a series that exists in the playoffs
        public Group() { }

        public Group(string name, Playoff playoff, Division sortByDivision, List<GroupRule> groupRules)
        {
            Name = name;
            Playoff = playoff;
            GroupRules = groupRules;
            SortByDivision = sortByDivision;
        }

        #region Validation

        public override void CheckForErrors(List<string> errorMessages)
        {
            string formatter = "{0}";
            bool noRuleErrors = true;
            GroupRules.ForEach(gr =>
            {
                var validRule = gr.ValidateConfiguration(errorMessages);

                if (validRule)
                {
                    //if from series does series exist?
                    if (gr.RuleType == GroupRule.FROM_SERIES)
                    {
                        var seriesName = gr.FromSeries;
                        validRule = Playoff.SeriesRules.Where(sr => sr.Name == seriesName).FirstOrDefault() != null;
                        if (!validRule) AddMessage(formatter, errorMessages, "Series with Name: " + seriesName + " does not exist in Playoff: " + Playoff.Name);
                    }
                }

                noRuleErrors = noRuleErrors && validRule;

            });

            if (!noRuleErrors) AddMessage("{0}", errorMessages, "Errors in Group Rules");
            
        }
        #endregion

    }
}
