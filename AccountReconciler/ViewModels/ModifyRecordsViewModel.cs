using AccountReconcilerLibrary;
using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconciler.ViewModels
{
    public class ModifyRecordsViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;
        Messager messager;
        DialogManager dialogManager;

        public ModifyRecordsViewModel(Record rec)
        {
            ModifiedRecord = rec;
            dialogManager = new DialogManager();

            messager = new Messager();
            context = DatabaseManager.DatabaseContext;
            Groups1 = context.Groups1.Local;
            Groups2 = ModifiedRecord.Group1.Groups2;

            ModifiedRecord.PropertyChanged += ModifiedRecord_PropertyChanged;
        }

        private void ModifiedRecord_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "Group1")
                {
                    Groups2 = ModifiedRecord.Group1.Groups2;
                }
            }
            catch (Exception)
            {

            }

        }

        #region Properties
        private Record modifiedRecord;
        public Record ModifiedRecord
        {
            get { return modifiedRecord; }
            set { modifiedRecord = value; }
        }

        private ObservableCollection<Group1> groups1;
        public ObservableCollection<Group1> Groups1
        {
            get { return groups1; }
            set { groups1 = value; }
        }

        private ObservableCollection<Group2> groups2;
        public ObservableCollection<Group2> Groups2
        {
            get { return groups2; }
            set { groups2 = value; }
        }
        #endregion

        #region Commands

        private RCommand saveCommand;
        public RCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RCommand(
                    (obj) => 
                    {
                        context.SaveChanges();
                        messager.SuccessMessage("Record changed successfully");
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
                        if (dialogManager.QuestionMessage("Remove selected record?", "Remove record"))
                        {
                            context.Records.Remove(ModifiedRecord);
                            context.SaveChanges();
                            
                            NavigationManager.GoToRecords();
                        }
                        

                    },
                    (obj) => { return true; }
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
                        DatabaseManager.DiscardChanges();
                        NavigationManager.GoBack();
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
