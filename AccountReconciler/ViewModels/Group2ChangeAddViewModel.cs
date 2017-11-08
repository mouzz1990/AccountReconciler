using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ViewModels
{
    public class Group2ChangeAddViewModel
    {
        DialogManager dialogManager;
        DatabaseContext context;

        public Group2ChangeAddViewModel(Group1 gr)
        {
            context = DatabaseManager.DatabaseContext;

            context.Groups1.ToList();
            context.Groups2.ToList();

            SelectedGroup1 = gr;
            
            Group = new Group2();
            context.SaveChanges();
            IsNewGroup = true;
        }

        public Group2ChangeAddViewModel(Group1 gr1, Group2 gr2)
        {
            dialogManager = new DialogManager();
            context = DatabaseManager.DatabaseContext;

            context.Groups1.ToList();
            context.Groups2.ToList();

            SelectedGroup1 = gr1;
           
            Group = gr2;
            IsNewGroup = false;
        }

        #region Properties
        private Group1 selectedGroup1;
        public Group1 SelectedGroup1
        {
            get { return selectedGroup1; }
            set { selectedGroup1 = value; }
        }

        private Group2 group;
        public Group2 Group
        {
            get { return group; }
            set { group = value; }
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
                            SelectedGroup1.Groups2.Add(Group);
                        }

                        //SaveChanges
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

                        SelectedGroup1.Groups2.Remove(Group);

                        //SaveChanges
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
