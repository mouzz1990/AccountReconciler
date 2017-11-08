using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class Account : INotifyPropertyChanged
    {
        private int accountId;
        public int AccountID
        {
            get { return accountId; }
            set { accountId = value; }
        }

        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; OnPropertyChanged("AccountName"); }
        }

        public override string ToString()
        {
            return AccountName;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;

        //    Account acc = obj as Account;

        //    if (acc == null) return false;

        //    if (acc.AccountID == this.AccountID) return true;

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return AccountID;
        //}
        #region INotifyPropertyChanged implemented interface
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
