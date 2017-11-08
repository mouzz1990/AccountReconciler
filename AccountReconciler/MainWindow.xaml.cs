using AccountReconcilerLibrary.ViewModels;
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

namespace AccountReconcilerLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DatabaseManager.DatabaseContext = new DatabaseContext();

            NavigationManager.Frame = frm;

            frm.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainViewModel mvm = new MainViewModel();

            DataContext = mvm;
        }
    }
}
