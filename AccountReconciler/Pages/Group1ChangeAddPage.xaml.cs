using AccountReconcilerLibrary.Models;
using AccountReconcilerLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AccountReconcilerLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для Group1ChangeAddPage.xaml
    /// </summary>
    public partial class Group1ChangeAddPage : Page
    {
        public Group1ChangeAddPage()
        {
            InitializeComponent();
            DataContext = new Group1ChangeAddViewModel();
        }

        public Group1ChangeAddPage(Group1 group)
        {
            InitializeComponent();
            DataContext = new Group1ChangeAddViewModel(group);
        }
    }
}
