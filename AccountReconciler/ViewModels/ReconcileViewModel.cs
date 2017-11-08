using AccountReconciler.EqualityComparers;
using AccountReconcilerLibrary.Commands;
using AccountReconcilerLibrary.DialogManagers;
using AccountReconcilerLibrary.ImportExportManager;
using AccountReconcilerLibrary.Models;
using AccountReconcilerLibrary.RulesHelper;
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
    public class ReconcileViewModel : INotifyPropertyChanged
    {
        DatabaseContext context;
        RulesValidator validator;
        DialogManager dialogManager;
        RecordDublicateEqualityComparer recEqComparer;

        public ReconcileViewModel()
        {
            validator = new RulesValidator();
            dialogManager = new DialogManager();
            recEqComparer = new RecordDublicateEqualityComparer();

            context = DatabaseManager.DatabaseContext;

            var unrec = from rec in context.Records
                        where rec.IsVaidated == false
                        select rec;

            UnreconciledRecords = new ObservableCollection<Record>(unrec);

            foreach (var r in UnreconciledRecords)
                validator.ValidateRecord(r);

            Groups1 = context.Groups1.Local;

            CheckDublicate();
        }

        #region Properties
        private ObservableCollection<Record> unreconcilledRecords;
        public ObservableCollection<Record> UnreconciledRecords
        {
            get { return unreconcilledRecords; }
            set { unreconcilledRecords = value; }
        }

        private Record selectedRecord;
        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                selectedRecord = value;
                if (SelectedRecord != null)
                {
                    SelectedRecord.PropertyChanged -= new PropertyChangedEventHandler(SelectedRecord_PropertyChanged);
                    SelectedRecord.PropertyChanged += new PropertyChangedEventHandler(SelectedRecord_PropertyChanged);
                }

                OnPropertyChanged("SelectedRecord");
            }
        }

        void SelectedRecord_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Group1")
            {
                try
                {
                    Groups2 = SelectedRecord.Group1.Groups2;
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
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
        private RCommand validateRuleCommand;
        public RCommand ValidateRuleCommand
        {
            get
            {
                return validateRuleCommand ?? (validateRuleCommand = new RCommand(
                    (obj) =>
                    {
                        SelectedRecord.IsVaidated = true;
                        SelectedRecord.PropertyChanged -= new PropertyChangedEventHandler(SelectedRecord_PropertyChanged);
                        UnreconciledRecords.Remove(SelectedRecord);

                        context.SaveChanges();
                    },
                    (obj) => {
                        Group1 gr = obj as Group1;
                        return gr != null;
                    }
                    ));
            }
        }

        private RCommand addNewRuleCommand;
        public RCommand AddNewRuleCommand
        {
            get
            {
                return addNewRuleCommand ?? (addNewRuleCommand = new RCommand(
                    (obj) =>
                    {
                        NavigationManager.GoToRules();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand removeRecordCommand;
        public RCommand RemoveRecordCommand
        {
            get
            {
                return removeRecordCommand ?? (removeRecordCommand = new RCommand(
                    (obj) =>
                    {
                        if (dialogManager.QuestionMessage("Remove selected record?", "Removing record"))
                        {
                            context.Records.Remove(SelectedRecord);
                            UnreconciledRecords.Remove(SelectedRecord);
                            context.SaveChanges();
                        }
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        private RCommand checkDublicatesCommand;
        public RCommand CheckDublicatesCommand
        {
            get
            {
                return checkDublicatesCommand ?? (checkDublicatesCommand = new RCommand(
                    (obj) =>
                    {
                        CheckDublicate();
                    },
                    (obj) => { return true; }
                    ));
            }
        }

        
        private RCommand removeDublicatesCommand;
        public RCommand RemoveDublicatesCommand
        {
            get
            {
                return removeDublicatesCommand ?? (removeDublicatesCommand = new RCommand(
                    (obj) =>
                    {
                        if (dialogManager.QuestionMessage("Remove all dublicates?", "Remove dublicates"))
                        {
                            var dubls = (from r in UnreconciledRecords
                                         where r.IsDublicate == true
                                         select r).Distinct(recEqComparer);

                            List<Record> ToRemove = new List<Record>();
                            foreach (var r in dubls)
                                ToRemove.Add(r);

                            foreach (var rr in ToRemove)
                            {
                                UnreconciledRecords.Remove(rr);
                                context.Records.Remove(rr);
                            }

                            context.SaveChanges();
                            CheckDublicate();
                        }
                    },
                    (obj) => { return true; }
                    ));
            }
        }
        #endregion

        #region Private methods
        private void CheckDublicate()
        {
            foreach (var rec in UnreconciledRecords)
            {
                var dubls = from r in UnreconciledRecords
                            where r.RecordId != rec.RecordId
                            && r.RecordDate == rec.RecordDate
                            && r.RecordDetails == rec.RecordDetails
                            && r.RecordValue == rec.RecordValue
                            select r;

                foreach (var d in dubls)
                    d.IsDublicate = true;

                if (dubls.Count() == 0)
                    rec.IsDublicate = false;
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
