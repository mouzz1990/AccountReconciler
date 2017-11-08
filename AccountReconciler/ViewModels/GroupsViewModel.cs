using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ViewModels
{
    public class GroupsViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;

        public GroupsViewModel()
        {
            context = DatabaseManager.DatabaseContext;

            ////temp
            //Group1 gr1 = new Group1();
            //gr1.Group1ID = 1;
            //gr1.GroupName = "Group 1";
            //gr1.GroupDescription = "Group 1 Description";
            //gr1.GroupType = "Type 1";
            //gr1.GroupTax = 2.31;
            //gr1.GroupCode = "2-12";

            //Group2 gr2 = new Group2();
            //gr2.GroupName = "Group 2";
            //gr2.Group2ID = 1;
            //gr2.GroupDescription = "Group 1 - Group 2-1 Description";

            //gr1.Groups2.Add(gr2);

            //context.Groups1.Add(gr1);
            //context.SaveChanges();
            ////

            context.Groups1.ToList();
            Groups1 = context.Groups1.Local;
            //Groups1 = new ObservableCollection<Group1>(context.Groups1);
        }

        #region Properties
        //Visibility of Popup menu
        private bool addEditModeGroup1Enabled;
        public bool AddEditModeGroup1Enabled
        {
            get { return addEditModeGroup1Enabled; }
            set { addEditModeGroup1Enabled = value; OnPropertyChanged("AddEditModeGroup1Enabled"); }
        }
        //Visibility of Popup menu
        private bool addEditModeGroup2Enabled;
        public bool AddEditModeGroup2Enabled
        {
            get { return addEditModeGroup2Enabled; }
            set { addEditModeGroup2Enabled = value; OnPropertyChanged("AddEditModeGroup2Enabled"); }
        }

        //Groups 1
        private ObservableCollection<Group1> groups1;
        public ObservableCollection<Group1> Groups1
        {
            get { return groups1; }
            set { groups1 = value; }
        }

        private Group1 selectedGroup1;
        public Group1 SelectedGroup1
        {
            get { return selectedGroup1; }
            set { selectedGroup1 = value; OnPropertyChanged("SelectedGroup1"); }
        }

        //Groups 2
        private Group2 selectedGroup2;
        public Group2 SelectedGroup2
        {
            get { return selectedGroup2; }
            set { selectedGroup2 = value; OnPropertyChanged("SelectedGroup2"); }
        }
        #endregion

        #region Commands
        //Pressing Save Button
        private RCommand addGroup1Command;
        public RCommand AddGroup1Command
        {
            get
            {
                return addGroup1Command ?? (addGroup1Command = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToNewGroup1();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        //Press on Edit or Add Group1 Button
        private RCommand editGroup1Command;
        public RCommand EditGroup1Command
        {
            get
            {
                return editGroup1Command ?? (editGroup1Command = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToNewGroup1(SelectedGroup1);
                    },
                    (obj) => { return true; }
                    ));
            }

        }

        //Group 2
        ////Pressing Save Button Group 2 add/edit menu
        private RCommand addGroup2Command;
        public RCommand AddGroup2Command
        {
            get
            {
                return addGroup2Command ?? (addGroup2Command = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToNewGroup2(SelectedGroup1);
                    },
                    (obj) =>
                    {
                        return !(SelectedGroup1 == null);
                    }
                    ));
            }

        }

        //Press on Edit or Add Group2 Button
        private RCommand editGroup2Command;
        public RCommand EditGroup2Command
        {
            get
            {
                return editGroup2Command ?? (editGroup2Command = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToNewGroup2(SelectedGroup1, SelectedGroup2);
                    },
                    (obj) => { return true; }
                    ));
            }
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
