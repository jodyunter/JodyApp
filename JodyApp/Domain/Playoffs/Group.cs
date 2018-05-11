using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Group:ConfigurableDomainObject
    {
        public const string GROUP_RULE_ERRORS = "Errors in Group Rules";
        public const string NOSORTBYDIVISION_ONE_RULE_ONLY = "Only one rule can be used when no Sort By Division is set";
        public const string FROM_SERIES_DOES_NOT_EXIST = "Series with Name: {0} does not exist in Playoff: {1}";
        public const string FROM_DIVISION_NOSORTBYDIVISION_TOO_MANY_POSITIONS = "When using a From Division Rule without a Sort By Division, you can only specify one position in the division";
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
            string formatter = "{0}. GroupRule: {1}.";
            bool noRuleErrors = true;
    
            if (SortByDivision == null)            
                if (GroupRules.Count > 1) AddMessage(formatter, errorMessages, NOSORTBYDIVISION_ONE_RULE_ONLY, Name);                
            
            GroupRules.ForEach(gr =>
            {
                var validRule = gr.ValidateConfiguration(errorMessages);

                if (validRule)
                {
                    //if from series does series exist?
                    switch(gr.RuleType) 
                    {
                        case GroupRule.FROM_SERIES:
                            var seriesName = gr.FromSeries;
                            validRule = Playoff.SeriesRules.Where(sr => sr.Name == seriesName).FirstOrDefault() != null;
                            if (!validRule) AddMessage(formatter, errorMessages, String.Format(FROM_SERIES_DOES_NOT_EXIST, seriesName, Playoff.Name), Name);
                            break;
                        case GroupRule.FROM_DIVISION:
                        case GroupRule.FROM_DIVISION_BOTTOM:
                            if (SortByDivision == null)
                                if (gr.FromStartValue != gr.FromEndValue) AddMessage(formatter, errorMessages, FROM_DIVISION_NOSORTBYDIVISION_TOO_MANY_POSITIONS, Name);
                            break;
                    }                    
                }

                noRuleErrors = noRuleErrors && validRule;

            });

            if (!noRuleErrors) AddMessage(formatter, errorMessages, GROUP_RULE_ERRORS, Name);
            
        }
        #endregion

    }
}
