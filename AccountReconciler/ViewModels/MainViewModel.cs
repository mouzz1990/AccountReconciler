using AccountReconcilerLibrary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            NavigationManager.GoToDashboard();
        }
        
        private RCommand openDashboardCommand;
        public RCommand OpenDashboardCommand
        {
            get
            {
                return openDashboardCommand ?? (openDashboardCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToDashboard();
                    },
                    (obj) => { return true; }
                    ));
            }
        }


        private RCommand openAccountsCommand;
        public RCommand OpenAccountsCommand
        {
            get
            {
                return openAccountsCommand ?? (openAccountsCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToAccounts();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openGroupsCommand;
        public RCommand OpenGroupsCommand
        {
            get
            {
                return openGroupsCommand ?? (openGroupsCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToGroups();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openImportCommand;
        public RCommand OpenImportCommand
        {
            get
            {
                return openImportCommand ?? (openImportCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToImport();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openReconciledRecords;
        public RCommand OpenReconciledRecords
        {
            get
            {
                return openReconciledRecords ?? (openReconciledRecords = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToReconcile();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openRulesCommand;
        public RCommand OpenRulesCommand
        {
            get
            {
                return openRulesCommand ?? (openRulesCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToRules();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openRecordsCommand;
        public RCommand OpenRecordsCommand
        {
            get
            {
                return openRecordsCommand ?? (openRecordsCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToRecords();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openExportCommand;
        public RCommand OpenExportCommand
        {
            get
            {
                return openExportCommand ?? (openExportCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToExport();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand openModifyRulesCommand;
        public RCommand OpenModifyRulesCommand
        {
            get
            {
                return openModifyRulesCommand ?? (openModifyRulesCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToModifyRules();
                    },
                    (obj) => { return true; }
                    ));
            }
        }
        
    }
}
