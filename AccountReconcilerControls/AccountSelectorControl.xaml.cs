using AccountReconcilerLibrary.Models;
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

namespace AccountReconcilerLibrary
{
    /// <summary>
    /// Логика взаимодействия для AccountSelectorControl.xaml
    /// </summary>
    public partial class AccountSelectorControl : UserControl
    {
        public AccountSelectorControl()
        {
            InitializeComponent();
        }

        //Title of main checkbox - have to be Account
        public string MainComboBoxTitle
        {
            get { return (string)GetValue(MainComboBoxTitleProperty); }
            set { SetValue(MainComboBoxTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainComboBoxTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainComboBoxTitleProperty =
            DependencyProperty.Register("MainComboBoxTitle", typeof(string), typeof(AccountSelectorControl), new FrameworkPropertyMetadata("Title 1", new PropertyChangedCallback(OnTitleChanged)));

        public static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AccountSelectorControl uc = (AccountSelectorControl)sender;
            if (e.Property == MainComboBoxTitleProperty)
                uc.txbMainCmb.Text = (string)e.NewValue;
        }

        //Items of account, have to bind my accounts list to this dp property
        public ObservableCollection<Account> MainComboBoxItems
        {
            get { return (ObservableCollection<Account>)GetValue(MainComboBoxItemsProperty); }
            set { SetValue(MainComboBoxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainComboBoxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainComboBoxItemsProperty =
            DependencyProperty.Register("MainComboBoxItems", typeof(ObservableCollection<Account>), typeof(AccountSelectorControl), new FrameworkPropertyMetadata(new ObservableCollection<Account>() ,new PropertyChangedCallback(OnComboBoxItemsChanged)));

        public static void OnComboBoxItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AccountSelectorControl uc = (AccountSelectorControl)sender;
            if (e.Property == MainComboBoxItemsProperty)
            {
                uc.cmbMainAcc.Items.Clear();

                foreach (var i in uc.MainComboBoxItems)
                    uc.cmbMainAcc.Items.Add(i);
            }
        }

        //clicking on "+" button for adding additional combobox with accounts
        private void btnAddNewAcc_Click(object sender, RoutedEventArgs e)
        {
            AccountSelectorSubelementControl ac = new AccountSelectorSubelementControl();
            ac.RemoveItem += new EventHandler(ac_RemoveItem);
            ac.AddItem += new EventHandler<AccountsEventArgs>(ac_AddItem);
            scStackPanel.Children.Add(ac);
            ac.ComboBoxItems = userControl.MainComboBoxItems;
        }

        //handler, when sub UC added
        void ac_AddItem(object sender, AccountsEventArgs e)
        {
            AccountSelectorSubelementControl ac = (AccountSelectorSubelementControl)sender;
            if (e.OldObject != null)
                AllSelectedItems.Remove(e.OldObject);
            AllSelectedItems.Add(ac.SelectedOtherItem);
        }

        //handler, when sub UC removed
        void ac_RemoveItem(object sender, EventArgs e)
        {
            AccountSelectorSubelementControl ac = (AccountSelectorSubelementControl)sender;
            AllSelectedItems.Remove(ac.SelectedOtherItem);
            scStackPanel.Children.Remove(ac);
        }

        //Collection of all selected Items - have to be bound to my results
        public ObservableCollection<Account> AllSelectedItems
        {
            get { return (ObservableCollection<Account>)GetValue(AllSelectedItemsProperty); }
            set { SetValue(AllSelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllSelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllSelectedItemsProperty =
            DependencyProperty.Register("AllSelectedItems", typeof(ObservableCollection<Account>), typeof(AccountSelectorControl), new FrameworkPropertyMetadata(new ObservableCollection<Account>()));

        //updating account selected in main combobox
        private void cmbMainAcc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Account oldVal = null;

            if (e.RemovedItems.Count > 0)
                oldVal = (Account)e.RemovedItems[0];

            if (oldVal != null)
                AllSelectedItems.Remove(oldVal);

            AllSelectedItems.Add((Account)((ComboBox)sender).SelectedItem);
        }
    }
}
