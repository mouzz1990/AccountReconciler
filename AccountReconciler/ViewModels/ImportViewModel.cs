using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.ImportExportManager;
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
    public class ImportViewModel : INotifyPropertyChanged
    {
        DialogManager dialogManager;
        Messager messager;
        ImportExportCsvManager importExportManager;
        DatabaseContext context;

        public ImportViewModel()
        {
            context = DatabaseManager.DatabaseContext;
            dialogManager = new DialogManager();
            messager = new Messager();
            importExportManager = new ImportExportCsvManager();
            UnreconciledRecords = context.Records.Local;

            Accounts = context.Accounts.Local;
        }

        #region Properties

        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        private ObservableCollection<Record> unreconciledRecords;
        public ObservableCollection<Record> UnreconciledRecords
        {
            get { return unreconciledRecords; }
            set { unreconciledRecords = value; }
        }

        private Account selectedImportAccount;
        public Account SelectedImportAccount
        {
            get { return selectedImportAccount; }
            set { selectedImportAccount = value; OnPropertyChanged("SelectedImportAccount"); }
        }

        private string pathToImport;
        public string PathToImport
        {
            get { return pathToImport; }
            set { pathToImport = value; OnPropertyChanged("PathToImport"); }
        }
        #endregion

        #region Commands
        private RCommand openImportDialog;
        public RCommand OpenImportDialog
        {
            get
            {
                return openImportDialog ?? (openImportDialog = new RCommand(
                    (obj) =>
                    {
                        PathToImport = dialogManager.OpenFile();

                        if (!string.IsNullOrEmpty(PathToImport))
                        {
                            try
                            {
                                int count = 0;

                                importExportManager.ImportCsvFile(SelectedImportAccount, PathToImport, UnreconciledRecords, ref count);

                                messager.SuccessMessage(
                                string.Format("Import Successfull. Added {0} records.", count));
                                NavigationManager.GoToReconcile();
                            }
                            catch (Exception e)
                            {
                                messager.ErrorMessage(e.Message);
                            }
                        }
                    },
                    (obj) => { return SelectedImportAccount != null; }
                    ));
            }
        }

        private RCommand goBack;
        public RCommand GoBack
        {
            get
            {
                return goBack ?? (goBack = new RCommand(
                    (obj) =>
                    {
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
