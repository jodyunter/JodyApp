using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class GroupRule:ConfigurableDomainObject
    {
        public const string INVALID_GROUP_TYPE = "Invalid Group Type";
        public const string CANNOT_BE_NULL = "Group Cannot Be Null";
        public const string NAME_CANNOT_BE_NULL = "Name Cannot be Null";
        public const string FROM_DIVISION_CANNOT_BE_NULL = "From Division Cannot be null";
        public const string FROM_DIVISION_HAS_NO_TEAMS = "From Division has No Teams";
        public const string FROM_DIVISION_NOT_ENOUGH_TEAMS = "From Division does not have enough teams, expecting: {0}, found: {1}";
        public const string FROM_SERIES_CANNOT_BE_NULL = "From Series Cannot be null";
        public const string FROM_SERIES_BAD_TEAM_VALUE = "Value must be Series Winner or Series Loser";
        public const string FROM_TEAM_CANNOT_BE_NULL = "From Team Cannot be null";

        public const int SERIES_WINNER = 0;
        public const int SERIES_LOSER = 1;

        public const int FROM_DIVISION = 0;
        public const int FROM_TEAM = 1;
        public const int FROM_SERIES = 2;
        public const int FROM_DIVISION_BOTTOM = 3;

        

        virtual public Group Group { get; set; }        

        public int RuleType { get; set; }
        //example of different sorty and from
        //we sort by league
        //but we take DivA, 1,2,3 and DivB, 4,5 and ConferenceA, 1
        //no rounds because we might want DivA 1 to go directly to the final where as the winners of other places will be put in other groups        
        virtual public Division FromDivision { get; set; }        
        public String FromSeries { get; set; } //this means that all series need to be created at playoff creation time
        public int FromStartValue { get; set; } //ranking or WINNER/LOSER, or start = 1 or start = 2, or start at bottom and use end value to go backwards
        public int FromEndValue { get; set; } //division rankings 1, 10        
        virtual public Team FromTeam { get; set; }        
        public string Name { get; set; }
        public GroupRule() { }
        public GroupRule(GroupRule rule, Division fromDivision, Team team, Group g) : this(g, rule.FromSeries, rule.RuleType, fromDivision,
                                                                                                        rule.FromSeries, rule.FromStartValue, rule.FromEndValue, team)
        { }

        public GroupRule(Group group, string name, int ruleType, Division fromDivision, 
                            String seriesName, int fromStartValue, int fromEndValue, Team fromTeam)
        {
            Group = group;
            RuleType = ruleType;            
            FromDivision = fromDivision;
            FromSeries = seriesName;
            FromStartValue = fromStartValue;
            FromEndValue = fromEndValue;
            FromTeam = fromTeam;                     
            Name = name;
        }

        #region Validation

        public string GetTypeName(int GroupType)
        {
            switch (RuleType)
            {
                case FROM_DIVISION:
                    return "From Division Top";                    
                case FROM_DIVISION_BOTTOM:
                    return "From Division Bottom";                    
                case FROM_SERIES:
                    return "From Series";                    
                case FROM_TEAM:
                    return "From Team";                    
            }

            return INVALID_GROUP_TYPE;
        }

        public override void CheckForErrors(List<string> errorMessages)
        {
            if (errorMessages == null) errorMessages = new List<string>();

            string formatter = "{0}. Type: {1}. Name: {2}.";
            string TypeName = GetTypeName(RuleType);
            if (String.IsNullOrEmpty(Name)) AddMessage(formatter, errorMessages, NAME_CANNOT_BE_NULL, TypeName, Name);
            if (TypeName.Equals(INVALID_GROUP_TYPE)) AddMessage(formatter, errorMessages, "No message here", TypeName, Name);

            if (Group == null)
            {
                AddMessage(formatter, errorMessages, CANNOT_BE_NULL, TypeName, Name);
                return; //no group don't bother to continue
            }

            switch (RuleType)
            {
                case FROM_DIVISION:
                case FROM_DIVISION_BOTTOM:
                    ValidationFromDivision(formatter, errorMessages, TypeName);
                    break;
                case FROM_SERIES:
                    ValidationFromSeries(formatter, errorMessages, TypeName);
                    break;
                case FROM_TEAM:
                    ValidationFromTeam(formatter, errorMessages, TypeName);
                    break;
            }
        }

        private void ValidationFromDivision(String formatter, List<string> errorMessages, string TypeName)
        {
            if (FromDivision == null) AddMessage(formatter, errorMessages, FROM_DIVISION_CANNOT_BE_NULL, TypeName, Name);
            else
                if (FromDivision.Teams == null || FromDivision.Teams.Count == 0) AddMessage(formatter, errorMessages, FROM_DIVISION_HAS_NO_TEAMS, TypeName, Name);
                else
                    if (FromDivision.Teams.Count < FromEndValue)
                        AddMessage(formatter, errorMessages,
                        String.Format(FROM_DIVISION_NOT_ENOUGH_TEAMS, FromEndValue, FromDivision.Teams.Count),
                        TypeName, Name);
        }

        private void ValidationFromSeries(String formatter, List<string> errorMessages, string TypeName)
        {
            if (String.IsNullOrEmpty(FromSeries)) AddMessage(formatter, errorMessages, FROM_SERIES_CANNOT_BE_NULL, TypeName, Name);
            else
                if (FromStartValue != SERIES_WINNER && FromStartValue != SERIES_LOSER)
                    AddMessage(formatter, errorMessages, FROM_SERIES_BAD_TEAM_VALUE, TypeName, Name);
            
        }

        private void ValidationFromTeam(String formatter, List<string> errorMessages, string TypeName)
        {
            if (FromTeam == null) AddMessage(formatter, errorMessages, FROM_TEAM_CANNOT_BE_NULL, TypeName, Name);
        }
        #endregion

        #region Static Creates
        public static GroupRule CreateFromDivision(Group g, string name,Division fromDivision, int highestRank, int lowestRank)
        {            
            var rule =  new GroupRule(g, name, GroupRule.FROM_DIVISION, fromDivision, null, highestRank, lowestRank, null);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromDivisionBottom(Group g, string name, Division fromDivision, int highestRank, int lowestRank)
        {            
            var rule = new GroupRule(g, name, GroupRule.FROM_DIVISION_BOTTOM, fromDivision, null, highestRank, lowestRank, null);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromSeriesWinner(Group g, string name, string seriesName)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_WINNER, GroupRule.SERIES_WINNER, null);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromSeriesLoser(Group g, string name, string seriesName)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_SERIES, null, seriesName, GroupRule.SERIES_LOSER, GroupRule.SERIES_LOSER, null);
            g.GroupRules.Add(rule);
            return rule;
        }

        public static GroupRule CreateFromTeam(Group g, string name, Team team)
        {
            var rule = new GroupRule(g, name, GroupRule.FROM_TEAM, null, null, 1, 1, team);
            g.GroupRules.Add(rule);
            return rule;
        }
        #endregion

    }
}
