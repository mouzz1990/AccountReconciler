using AccountReconcilerLibrary.Models;
using AccountReconcilerLibrary.RulesHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary.ImportExportManager
{
    public class ImportExportCsvManager
    {
        DatabaseContext context;
        RulesValidator validator;

        public ImportExportCsvManager()
        {
            context = DatabaseManager.DatabaseContext;
            validator = new RulesValidator();
        }

        public bool ImportCsvFile(Account account, string path, ObservableCollection<Record> records, ref int count)
        {
            //this list is for remove records if something go wrong
            List<Record> importedRecords = new List<Record>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    try
                    {
                        //getting record from csv line
                        Record recToAdd = CsvLineToRecord(account, line);

                        records.Add(recToAdd);

                        importedRecords.Add(recToAdd);

                        count++;
                    }
                    catch (FileFormatException ffe)
                    {
                        throw ffe;
                    }
                    catch (FormatException fe)
                    {
                        ClearLists(records, importedRecords);
                        throw fe;
                    }
                    catch (ArgumentNullException ne)
                    {
                        ClearLists(records, importedRecords);
                        throw ne;
                    }
                }
            }

            return true;
        }

        //converting each line to Record object
        private Record CsvLineToRecord(Account acc, string line)
        {
            CultureInfo culture = new CultureInfo("en-US");
            
            //Separate csv line with ','
            var arr = line.Split(',');

            //if csv have incorrect length, throw FileFormatException
            if (arr.Length != 3) throw new FileFormatException("Opened file have an incorrect values. Please check that it has format \"Date,Value;Details\".");

            Record rec = new Record();
            rec.BankAccount = acc;

            //if there are problems with data typs
            DateTime rDate = DateTime.Parse(arr[0]);
            double rValue = double.Parse(arr[1], culture);
            string rDetails = arr[2];

            rec.RecordDate = rDate;
            rec.RecordValue = rValue;
            rec.RecordDetails = rDetails;

            //Validation
            //validator.ValidateRecord(rec);

            context.Records.Add(rec);
            context.SaveChanges();
            return rec;
        }

        //Clear records list if there was an error with import
        private void ClearLists(ICollection<Record> deleteFrom, ICollection<Record> deleteValues)
        {
            //Remove from observable list and data context
            foreach (var r in deleteValues)
            {
                context.Records.Remove(r);

                if (deleteFrom.Contains(r))
                    deleteFrom.Remove(r);

            }

            context.SaveChanges();
        }


        public void ExportCsvFile(string pathToSave)
        {
            var records = context.Records.Local.Where(x => x.IsVaidated == true);
            using (StreamWriter sw = new StreamWriter(pathToSave))
            {
                foreach (var r in records)
                {
                    StringBuilder sb = r.GetRecordExportStringBuilder();
                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}
