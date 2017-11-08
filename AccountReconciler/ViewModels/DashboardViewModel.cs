using AccountReconcilerLibrary;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Data.Entity.Infrastructure;

namespace AccountReconciler.ViewModels
{
    public class DashboardViewModel
    {
        DatabaseContext context;
        Chart chart;

        public DashboardViewModel( Chart ch)
        {
            chart = ch;
            FillDictionary();
            context = DatabaseManager.DatabaseContext;
            
            ValidatedRecords = (from r in context.Records
                      where r.IsVaidated == true
                      select r).ToList();

            UnreconciledCount = (from r in context.Records
                                 where r.IsVaidated == false
                                 select r
                                 ).Count();
            
            //out
            string outQuery = @"SELECT RecordDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, RecordDate), 0), IsVaidated, outSum = SUM(RecordValue)
FROM Records
WHERE IsVaidated = 'True' AND RecordValue < 0
GROUP BY DATEADD(MONTH, DATEDIFF(MONTH, 0, RecordDate), 0), IsVaidated;";

            var outVal = context.Database.SqlQuery<ReturnedSqlResult>(outQuery);

            //in
            string inQuery = @"SELECT RecordDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, RecordDate), 0), IsVaidated, outSum = SUM(RecordValue)
FROM Records
WHERE IsVaidated = 'True' AND RecordValue > 0
GROUP BY DATEADD(MONTH, DATEDIFF(MONTH, 0, RecordDate), 0), IsVaidated;";

            var inVal = context.Database.SqlQuery<ReturnedSqlResult>(inQuery);


            LoadBarChartData(outVal, inVal);
        }

        private void LoadBarChartData(DbRawSqlQuery<ReturnedSqlResult> outVal, DbRawSqlQuery<ReturnedSqlResult> inVal)
        {
            var outRes = outVal.ToList();
            var inRes = inVal.ToList();

            //out serie
            var outSerie = (ColumnSeries)chart.Series[0];

            //count of months
            int outCount = outVal.Count();
            int inCount = inVal.Count();

            //out columns bar
            KeyValuePair<string, double>[] outPair = new KeyValuePair<string, double>[outCount];
            for (int i = 0; i < outCount; i++)
            {
                outPair[i] = new KeyValuePair<string, double>(outRes[i].RecordDate.ToString("MM-yyyy"), -outRes[i].OutSum);
            }

            outSerie.ItemsSource = outPair;


            //in columns bar
            KeyValuePair<string, double>[] inPair = new KeyValuePair<string, double>[inCount];
            for (int i = 0; i < inCount; i++)
            {
                inPair[i] = new KeyValuePair<string, double>(inRes[i].RecordDate.ToString("MM-yyyy"), inRes[i].OutSum);
            }

            var inSerie = (ColumnSeries)chart.Series[1];
            inSerie.ItemsSource = inPair;
        }

        private int unreconciledCount;
        public int UnreconciledCount
        {
            get { return unreconciledCount; }
            set { unreconciledCount = value; }
        }

        private List<Record> validatedRecords;
        public List<Record> ValidatedRecords
        {
            get { return validatedRecords; }
            set { validatedRecords = value; }
        }

        private Dictionary<string, double> chartData;
        public Dictionary<string, double> ChartData
        {
            get { return chartData; }
            set { chartData = value; }
        }


        Dictionary<int, string> Monthes = new Dictionary<int, string>();
        private void FillDictionary()
        {
            Monthes.Add(1, "January");
            Monthes.Add(2, "February");
            Monthes.Add(3, "March");
            Monthes.Add(4, "April");
            Monthes.Add(5, "May");
            Monthes.Add(6, "June");
            Monthes.Add(7, "July");
            Monthes.Add(8, "August");
            Monthes.Add(9, "September");
            Monthes.Add(10, "October");
            Monthes.Add(11, "November");
            Monthes.Add(12, "December");
        }
    }

    public class ReturnedSqlResult
    {
        public DateTime RecordDate { get; set; }
        public bool IsValidated{ get; set; }
        public double OutSum { get; set; }

    }
}
