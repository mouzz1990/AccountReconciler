using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary
{
    public class DetailsControlReturned : INotifyPropertyChanged
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private bool isOrChecked;
        public bool IsOrChecked
        {
            get { return isOrChecked; }
            set { isOrChecked = value; OnPropertyChanged("IsOrChecked"); }
        }

        private string comparsionDetailsString;
        public string ComparsionDetailsString
        {
            get { return comparsionDetailsString; }
            set { comparsionDetailsString = value; OnPropertyChanged("ComparsionDetailsString"); }
        }

        [NotMapped]
        public bool IsAndChecked { get { return !IsOrChecked; } }

        private Rule rule;
        public Rule Rule
        {
            get { return rule; }
            set { rule = value; }
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            DetailsControlReturned x = obj as DetailsControlReturned;

            if (x == null) return false;

            if (this.ComparsionDetailsString == x.ComparsionDetailsString && this.IsOrChecked == x.IsOrChecked) return true;

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override int GetHashCode()
        {
            return 1213502048 + ID.GetHashCode();
        }
    }
}
