using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary
{
    public class AccountsEventArgs : EventArgs
    {
        public Account NewObject;
        public Account OldObject;
    }
}
