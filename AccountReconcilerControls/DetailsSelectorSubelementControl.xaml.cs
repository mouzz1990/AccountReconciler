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
    /// Логика взаимодействия для DetailsSelectorSubelementControl.xaml
    /// </summary>
    public partial class DetailsSelectorSubelementControl : UserControl
    {
        public DetailsSelectorSubelementControl()
        {
            ReturnResult = new DetailsControlReturned();
            ReturnResult.IsOrChecked = true;
            InitializeComponent();
        }

        //Result of action
        private DetailsControlReturned returnResult;
        public DetailsControlReturned ReturnResult
        {
            get { return returnResult; }
            set { returnResult = value; }
        }

        //checking radiobutton or
        private void rbOr_Checked(object sender, RoutedEventArgs e)
        {
            if (rbOr.IsChecked == true)
                ReturnResult.IsOrChecked = true;
        }

        //checking radiobutton and
        private void rbAnd_Checked(object sender, RoutedEventArgs e)
        {
            if (rbAnd.IsChecked == true) ReturnResult.IsOrChecked = false;
        }

        //return result description value
        private void txbSubValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReturnResult.ComparsionDetailsString = txbSubValue.Text;
        }

        //event, when item is removed
        public event EventHandler RemoveItem;
        public void OnRemoveItem()
        {
            if (RemoveItem != null) RemoveItem(this, EventArgs.Empty);
        }

        //clicking on "-" button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnRemoveItem();
        }
    }
}
