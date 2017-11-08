using AccountReconciler.Pages;
using AccountReconcilerLibrary.Models;
using AccountReconcilerLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AccountReconcilerLibrary
{
    public static class NavigationManager
    {
        static DashboardPage dashboard;
        static AccountPage accounts;
        static GroupsPage groups;
        static Group1ChangeAddPage group1AddChange;
        static Group2ChangeAddPage group2AddChange;
        static ImportPage import;
        static ReconcilePage reconcile;
        static RulesPage rules;
        static RecordsPage records;
        static ModifyRecordPage modifyRecords;
        static ExportCsvPage export;
        static ModifyRules modifyRules;

        public static Frame Frame { get; set; }

        static NavigationManager()
        {
            //SINGLE PAGES
            accounts = new AccountPage();
            //groups = new GroupsPage();
            import = new ImportPage();
            //reconcile = new ReconcilePage();
            rules = new RulesPage();

            modifyRules = new ModifyRules();
        }

        //GO TO DASHBOARD PAGE
        public static void GoToDashboard()
        {
            dashboard = new DashboardPage();
            Frame.Navigate(dashboard);
        }

        //GO TO ACCOUNTS PAGE
        public static void GoToAccounts()
        {
            Frame.Navigate(accounts);
        }

        //GO TO GROUPS PAGE
        public static void GoToGroups()
        {
            groups = new GroupsPage();
            Frame.Navigate(groups);
        }

        //EDIT OR ADD GROUP 1 PAGE
        //NEW GROUP1
        public static void GoToNewGroup1()
        {
            group1AddChange = new Group1ChangeAddPage();
            Frame.Navigate(group1AddChange);
        }

        //EDIT GROUP1
        public static void GoToNewGroup1(Group1 group)
        {
            group1AddChange = new Group1ChangeAddPage(group);
            Frame.Navigate(group1AddChange);
        }

        //EDIT OR ADD NEW GROUP 2 PAGE
        //NEW GROUP2
        public static void GoToNewGroup2(Group1 gr1)
        {
            group2AddChange = new Group2ChangeAddPage(gr1);
            Frame.Navigate(group2AddChange);
        }

        //EDIT GROUP2
        public static void GoToNewGroup2(Group1 gr1, Group2 gr2)
        {
            group2AddChange = new Group2ChangeAddPage(gr1, gr2);
            Frame.Navigate(group2AddChange);
        }

        //GO TO IMPORT PAGE
        public static void GoToImport()
        {
            import = new ImportPage();
            Frame.Navigate(import);
        }

        //GO TO RECONCILE PAGE
        public static void GoToReconcile()
        {
            reconcile = new ReconcilePage();
            Frame.Navigate(reconcile);
        }

        //GO TO RULES PAGE
        public static void GoToRules()
        {
            rules = new RulesPage();
            Frame.Navigate(rules);
        }

        //GO TO RECORDS PAGE
        public static void GoToRecords()
        {
            records = new RecordsPage();
            Frame.Navigate(records);
        }

        //GO TO MODIFY RECORDS PAGE
        public static void GoToModifyRecord(Record rec)
        {
            modifyRecords = new ModifyRecordPage(rec);
            Frame.Navigate(modifyRecords);
        }

        //GO TO MODIFY EXPORT PAGE
        public static void GoToExport()
        {
            export = new ExportCsvPage();
            Frame.Navigate(export);
        }

        //GO TO MODIFY RULES PAGE
        public static void GoToModifyRules()
        {
            //modifyRules = new ModifyRules();
            Frame.Navigate(modifyRules);
        }

        //GO BACK METHOD
        public static void GoBack()
        {
            DatabaseManager.DiscardChanges();

            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
