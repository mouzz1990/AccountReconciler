using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class Rule
    {
        public Rule()
        {
            RuleAccounts = new ObservableCollection<RuleAccount>();
            RuleContainsStrings = new ObservableCollection<DetailsControlReturned>();
        }

        private int ruleId;
        public int RuleId
        {
            get { return ruleId; }
            set { ruleId = value; }
        }

        private Group1 group1;
        public virtual Group1 Group1
        {
            get { return group1; }
            set { group1 = value; }
        }

        private Group2 group2;
        public virtual Group2 Group2
        {
            get { return group2; }
            set { group2 = value; }
        }

        private ObservableCollection<RuleAccount> ruleAccounts;
        public virtual ObservableCollection<RuleAccount> RuleAccounts
        {
            get { return ruleAccounts; }
            set { ruleAccounts = value; }
        }

        private ObservableCollection<DetailsControlReturned> ruleContainsStings;
        public virtual ObservableCollection<DetailsControlReturned> RuleContainsStrings
        {
            get { return ruleContainsStings; }
            set { ruleContainsStings = value; }
        }

        private string ruleOperator;
        public string RuleOperator
        {
            get { return ruleOperator; }
            set { ruleOperator = value; }
        }

        private double ruleValue;
        public double RuleValue
        {
            get { return ruleValue; }
            set { ruleValue = value; }
        }

        private string ruleComment;
        public string RuleComment
        {
            get { return ruleComment; }
            set { ruleComment = value; }
        }
    }
}
