using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;
        DialogManager dialogManager;

        public AccountViewModel()
        {
            dialogManager = new DialogManager();
            context = DatabaseManager.DatabaseContext;

            context.Accounts.ToList();
            Accounts = context.Accounts.Local;
        }

        #region Properties
        private ObservableCollection<Account> accounts;
        public virtual ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        private Account selectedAccount;
        public Account SelectedAccount
        {
            get { return selectedAccount; }
            set { selectedAccount = value; OnPropertyChanged("SelectedAccount"); }
        }
        #endregion

        #region Commands
        private RCommand accountSaveCommand;
        public RCommand AccountSaveCommand
        {
            get
            {
                return accountSaveCommand ?? (accountSaveCommand = new RCommand(
                    (obj) =>
                    {
                        string accName = (string)obj;

                        SelectedAccount.AccountName = accName;

                        context.SaveChanges();
                        return;
                    },
                    (obj) => { return SelectedAccount != null; }
                    ));
            }
        }

        private RCommand accountAddCommand;
        public RCommand AccountAddCommand
        {
            get
            {
                return accountAddCommand ?? (accountAddCommand = new RCommand(
                    (obj) =>
                    {
                        string accName = (string)obj;

                        Account newAcc = new Account();
                        newAcc.AccountName = accName;

                        Accounts.Add(newAcc);
                        
                        context.SaveChanges();

                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand accountRemoveCommand;
        public RCommand AccountRemoveCommand
        {
            get
            {
                return accountRemoveCommand ?? (accountRemoveCommand = new RCommand(
                    (obj) =>
                    {
                        if (!dialogManager.QuestionMessage("Remove account from database?", "Remove Account"))
                            return;

                        Accounts.Remove(SelectedAccount);

                        context.SaveChanges();
                    },
                    (obj) => { return SelectedAccount != null; }
                    ));
            }
        }

        private RCommand backCommand;
        public RCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RCommand(
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
