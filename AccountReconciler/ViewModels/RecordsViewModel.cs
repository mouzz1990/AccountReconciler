using AccountReconcilerLibrary;
using AccountReconcilerLibrary.Commands;
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
    public class RecordsViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;

        public RecordsViewModel()
        {
            context = DatabaseManager.DatabaseContext;
            ReconciledRecords = new ObservableCollection<Record>(context.Records.Where(x=>x.IsVaidated == true));
        }

        #region Properties
        private ObservableCollection<Record> reconciledRecords;
        public ObservableCollection<Record> ReconciledRecords
        {
            get { return reconciledRecords; }
            set { reconciledRecords = value; }
        }

        private Record selectedRecord;
        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set { selectedRecord = value; OnPropertyChanged("SelectedRecord"); }
        }

        #endregion

        #region Commands
        private RCommand editRecordCommand;
        public RCommand EditRecordCommand
        {
            get
            {
                return editRecordCommand ?? (editRecordCommand =  new RCommand(
                    (obj) => { NavigationManager.GoToModifyRecord(SelectedRecord); },
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
