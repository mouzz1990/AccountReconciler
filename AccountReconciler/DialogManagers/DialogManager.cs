using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountReconcilerLibrary.DialogManagers
{
    public class DialogManager
    {
        public string OpenFile()
        {
            string output = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV file|*.csv";

            if (ofd.ShowDialog() == true)
            {
                output = ofd.FileName;
                return output;
            }

            return string.Empty;
        }

        public string SaveFile()
        {
            string output = "";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file|*.csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == true)
            {
                output = sfd.FileName;
                return output;
            }

            return string.Empty;
        }

        public bool QuestionMessage(string message, string caption)
        {
            if (MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                return true;

            return false;
        }
    }
}
