using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.EqualityComparers
{
    public class AccountEqualityComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account x, Account y)
        {
            if (x == null) return false;
            if (y == null) return false;

            Account ac1 = x as Account;
            Account ac2 = y as Account;

            if (ac1 == null || ac2 == null) return false;

            if (ac1.AccountID == ac2.AccountID) return true;

            return false;
        }

        public int GetHashCode(Account obj)
        {
            return obj.AccountID;
        }
    }
}
