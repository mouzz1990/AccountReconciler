using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class Group1 : INotifyPropertyChanged
    {
        public Group1()
        {
            Groups2 = new ObservableCollection<Group2>();
        }

        #region Properties
        private int group1Id;

        public int Group1ID
        {
            get { return group1Id; }
            set { group1Id = value; }
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

        private string groupType;
        public string GroupType
        {
            get { return groupType; }
            set { groupType = value; OnPropertyChanged("GroupType"); }
        }

        private string groupCode;
        public string GroupCode
        {
            get { return groupCode; }
            set { groupCode = value; OnPropertyChanged("GroupCode"); }
        }

        private double groupTax;
        public double GroupTax
        {
            get { return groupTax; }
            set { groupTax = value; OnPropertyChanged("GroupTax"); }
        }

        private ObservableCollection<Group2> groups2;
        public virtual ObservableCollection<Group2> Groups2
        {
            get { return groups2; }
            set { groups2 = value; }
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
