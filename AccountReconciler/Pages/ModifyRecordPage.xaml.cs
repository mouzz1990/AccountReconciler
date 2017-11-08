using AccountReconciler.ViewModels;
using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountReconciler.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModifyRecordPage.xaml
    /// </summary>
    public partial class ModifyRecordPage : Page
    {
        public ModifyRecordPage(Record rec)
        {
            InitializeComponent();
            DataContext = new ModifyRecordsViewModel(rec);
        }
    }
}
