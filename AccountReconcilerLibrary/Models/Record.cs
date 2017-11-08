using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.Models
{
    public class Record : INotifyPropertyChanged
    {
        #region Properties
        private int recordId;
        public int RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        private DateTime recordDate;
        public DateTime RecordDate
        {
            get { return recordDate; }
            set { recordDate = value; }
        }

        private Account bankAccount;
        public virtual Account BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        private string recordDetails;
        public string RecordDetails
        {
            get { return recordDetails; }
            set { recordDetails = value; }
        }

        private double recordValue;
        public double RecordValue
        {
            get { return recordValue; }
            set { recordValue = value; }
        }

        private string recordComment;
        public string RecordComment
        {
            get { return recordComment; }
            set { recordComment = value; OnPropertyChanged("RecordComment"); }
        }

        private Group1 group1;
        public virtual Group1 Group1
        {
            get { return group1; }
            set { group1 = value; OnPropertyChanged("Group1"); }
        }

        private Group2 group2;
        public virtual Group2 Group2
        {
            get { return group2; }
            set { group2 = value; OnPropertyChanged("Group2"); }
        }

        private bool isVaidated;
        public bool IsVaidated
        {
            get { return isVaidated; }
            set { isVaidated = value; OnPropertyChanged("IsVaidated"); }
        }

        private bool isDublicate;
        [NotMapped]
        public bool IsDublicate
        {
            get { return isDublicate; }
            set { isDublicate = value; OnPropertyChanged("IsDublicate"); }
        }

        #endregion

        #region Public Methods
        public StringBuilder GetRecordExportStringBuilder()
        {
            CultureInfo culture = new CultureInfo("en-US");
            string group1Name = "";

            if (Group1 != null)
                group1Name = Group1.GroupName;

            string group2Name = "";

            if (Group2 != null)
                group2Name = Group2.GroupName;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(culture, "{0},{1},{2},{3},{4},{5},{6},{7}",
                RecordId,
                RecordDate.ToString("dd/MM/yyyy"),
                BankAccount.AccountName,
                group1Name,
                group2Name,
                RecordDetails,
                RecordValue,
                RecordComment
                );

            return sb;
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
