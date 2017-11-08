using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.EqualityComparers
{
    class Group2EqualityComparer : IEqualityComparer<Group2>
    {
        public bool Equals(Group2 x, Group2 y)
        {
            if (x == null) return false;
            if (y == null) return false;

            Group2 ac1 = x as Group2;
            Group2 ac2 = y as Group2;

            if (ac1 == null || ac2 == null) return false;

            if (ac1.Group2ID == ac2.Group2ID) return true;

            return false;
        }

        public int GetHashCode(Group2 obj)
        {
            return obj.Group2ID;
        }
    }
}
