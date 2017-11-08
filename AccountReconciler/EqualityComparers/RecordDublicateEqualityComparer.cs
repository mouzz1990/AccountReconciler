using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconciler.EqualityComparers
{
    public class RecordDublicateEqualityComparer : IEqualityComparer<Record>
    {
        public bool Equals(Record x, Record y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return  x.RecordDate == y.RecordDate &&
                    x.RecordValue == y.RecordValue &&
                    x.RecordDetails == y.RecordDetails;
        }

        public int GetHashCode(Record obj)
        {
            int recDateHash = obj.RecordDate.GetHashCode();
            int recValHash = obj.RecordValue.GetHashCode();
            int recDetailsHash = obj.RecordValue.GetHashCode();

            return recDateHash ^ recValHash ^ recDetailsHash;
        }
    }
}
