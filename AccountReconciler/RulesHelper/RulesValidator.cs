using AccountReconcilerLibrary.EqualityComparers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.RulesHelper
{
    public class RulesValidator
    {
        DatabaseContext context;
        AccountEqualityComparer accEqComparer;

        public RulesValidator()
        {
            accEqComparer = new AccountEqualityComparer();
            context = DatabaseManager.DatabaseContext;

            Rules = context.Rules.Local;
        }

        #region Properties
        private ObservableCollection<Rule> rules;
        public ObservableCollection<Rule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        #endregion

        #region Private Methods
        //ACCOUNT Validation
        private bool AccountValidate(Record record, Rule r)
        {
            bool result = false;

            foreach (var ra in r.RuleAccounts)
            {
                result = result | (ra.RuleAccountAttached.Equals(record.BankAccount));
            }
            
            return result;
        }

        //DETAILS validation
        private bool DetailValidate(Record record, Rule rule)
        {
            bool result = false;

            var ruleBehaivor = rule.RuleContainsStrings;

            //All selected OR-strings for rule
            var trueGroup = from rb in ruleBehaivor
                            where rb.IsOrChecked == true
                            select rb;

            foreach (var r in trueGroup)
            {
                result = result | record.RecordDetails.Contains(r.ComparsionDetailsString);
            }

            if (!result) return false;

            //All selected AND-strings for rule
            var falseGroup = from rb in ruleBehaivor
                             where rb.IsOrChecked == false
                             select rb;

            foreach (var r in falseGroup)
            {
                result = result & record.RecordDetails.Contains(r.ComparsionDetailsString);
            }

            return result;

        }

        //VALUE validation
        private bool ValueValidate(Record record, Rule r)
        {
            if (string.IsNullOrEmpty(r.RuleOperator)) return true;

            
            if (r.RuleOperator.ToUpper() == "LESS THAN")
            {
                if (record.RecordValue < r.RuleValue)
                {
                    return true;
                }
            }

            if (r.RuleOperator.ToUpper() == "EQUALS TO")
            {
                if (record.RecordValue == r.RuleValue)
                {
                    return true;
                }
            }

            if (r.RuleOperator.ToUpper() == "MORE THAN")
            {
                if (record.RecordValue > r.RuleValue)
                {
                    return true;
                }
            }
            

            return false;
        }
        #endregion

        #region Public Methods
        public void ValidateRecord(Record rec)
        {
            foreach (var rule in Rules)
            {
                if (AccountValidate(rec, rule) && DetailValidate(rec, rule) && ValueValidate(rec, rule))
                {
                    rec.Group1 = rule.Group1;
                    rec.Group2 = rule.Group2;
                    rec.RecordComment = rule.RuleComment;
                    return;
                }
            }
        }
        #endregion
    }
}
