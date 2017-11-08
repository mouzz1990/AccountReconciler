using AccountReconcilerLibrary.Models;
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

namespace AccountReconcilerLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для Group2ChangeAddPage.xaml
    /// </summary>
    public partial class Group2ChangeAddPage : Page
    {
        public Group2ChangeAddPage(Group1 gr1)
        {
            InitializeComponent();
            DataContext = new Group2ChangeAddViewModel(gr1);
        }

        public Group2ChangeAddPage(Group1 gr1, Group2 gr2)
        {
            InitializeComponent();
            DataContext = new Group2ChangeAddViewModel(gr1, gr2);
        }
    }
}
