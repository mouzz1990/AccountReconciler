using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class Group2 : INotifyPropertyChanged
    {
        #region Properties
        private int group2Id;

        public int Group2ID
        {
            get { return group2Id; }
            set { group2Id = value; }
        }

        private string groupName;
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; OnPropertyChanged("GroupName"); }
        }

        private string groupDescription;
        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; OnPropertyChanged("GroupDescription"); }
        }

        private Group1 group1;
        public virtual Group1 Group1
        {
            get { return group1; }
            set { group1 = value; }
        }
        #endregion


        #region INotifyPropertyChanged implemented interface
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
