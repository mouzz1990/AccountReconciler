using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class RuleAccount
    {
        private int ruleAccountId;
        [Key]
        public int RuleAccountId
        {
            get { return ruleAccountId; }
            set { ruleAccountId = value; }
        }

        private Account ruleAccountAttached;
        public Account RuleAccountAttached
        {
            get { return ruleAccountAttached; }
            set { ruleAccountAttached = value; }
        }

    }
}
