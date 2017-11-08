using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.EqualityComparers
{
    class Group1EqualityComparer : IEqualityComparer<Group1>
    {
        public bool Equals(Group1 x, Group1 y)
        {
            if (x == null) return false;
            if (y == null) return false;

            Group1 ac1 = x as Group1;
            Group1 ac2 = y as Group1;

            if (ac1 == null || ac2 == null) return false;

            if (ac1.Group1ID == ac2.Group1ID) return true;

            return false;
        }

        public int GetHashCode(Group1 obj)
        {
            return obj.Group1ID;
        }
    }
}
