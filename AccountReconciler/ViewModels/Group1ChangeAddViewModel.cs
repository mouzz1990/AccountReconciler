using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ViewModels
{
    public class Group1ChangeAddViewModel
    {
        DatabaseContext context;
        DialogManager dialogManager;

        public Group1ChangeAddViewModel()
        {
            context = DatabaseManager.DatabaseContext;
            context.Groups1.ToList();
            Groups1 = context.Groups1.Local;
            
            Group = new Group1();
            IsNewGroup = true;
        }

        public Group1ChangeAddViewModel(Group1 group)
        {
            dialogManager = new DialogManager();
            context = DatabaseManager.DatabaseContext;
            
            context.Groups1.ToList();
            Groups1 = context.Groups1.Local;
            Group = group;
            IsNewGroup = false;
        }

        #region Properties
        private Group1 group;
        public Group1 Group
        {
            get { return group; }
            set { group = value; }
        }

        private ObservableCollection<Group1> groups1;
        public ObservableCollection<Group1> Groups1
        {
            get { return groups1; }
            set { groups1 = value; }
        }
        #endregion

        bool IsNewGroup;

        #region Commands    
        private RCommand saveCommand;
        public RCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RCommand(
                    (obj) =>
                    {
                        if (IsNewGroup)
                        {
                            Groups1.Add(Group);
                            //context.Groups1.Add(Group);
                        }

                        context.SaveChanges();
                        
                        NavigationManager.GoBack();
                    },
                    (obj) =>
                    {
                        if (string.IsNullOrEmpty(Group.GroupName) || string.IsNullOrEmpty(Group.GroupDescription)) return false;

                        return true;
                    }
                    ));
            }
        }

        private RCommand cancelCommand;
        public RCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RCommand(
                    (obj) =>
                    {
                        //Cancel all changes
                        if (context.ChangeTracker.HasChanges())
                            DatabaseManager.DiscardChanges();

                        NavigationManager.GoBack();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand removeCommand;
        public RCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RCommand(
                    (obj) =>
                    {
                        if (!dialogManager.QuestionMessage("Remove group from database?", "Remove Group"))
                            return;

                        Groups1.Remove(Group);
                        context.SaveChanges();

                        NavigationManager.GoBack();
                    },
                    (obj) =>
                    {
                        return !IsNewGroup;
                    }
                    ));
            }
        }

        private RCommand goBackCommand;
        public RCommand GoBackCommand
        {
            get
            {
                return goBackCommand ?? (goBackCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoBack();
                    },
                    (obj) => { return true; }
                    ));
            }
        }
        #endregion
    }
}
