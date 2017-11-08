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
    /// Логика взаимодействия для AccountSelectorSubelementControl.xaml
    /// </summary>
    public partial class AccountSelectorSubelementControl : UserControl
    {
        public AccountSelectorSubelementControl()
        {
            InitializeComponent();
        }

        //Selected item in combobox
        public Account SelectedOtherItem
        {
            get { return (Account)GetValue(SelectedOtherItemProperty); }
            set { SetValue(SelectedOtherItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedOtherItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedOtherItemProperty =
            DependencyProperty.Register("SelectedOtherItem", typeof(Account), typeof(AccountSelectorSubelementControl), new FrameworkPropertyMetadata( new PropertyChangedCallback(OnItemSelected)));

        public static void OnItemSelected(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AccountSelectorSubelementControl ac = (AccountSelectorSubelementControl)sender;
            ac.SelectedOtherItem = (Account)e.NewValue;
        }

        //list of accounts items
        public ObservableCollection<Account> ComboBoxItems
        {
            get { return (ObservableCollection<Account>)GetValue(ComboBoxItemsProperty); }
            set { SetValue(ComboBoxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainComboBoxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxItemsProperty =
            DependencyProperty.Register("MainComboBoxItems", typeof(ObservableCollection<Account>), typeof(AccountSelectorSubelementControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnComboBoxItemsChanged)));

        public static void OnComboBoxItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AccountSelectorSubelementControl uc = (AccountSelectorSubelementControl)sender;
            if (e.Property == ComboBoxItemsProperty)
            {
                uc.cmbSvrAcc.Items.Clear();

                foreach (var o in (ObservableCollection<Account>)e.NewValue)
                {
                    uc.cmbSvrAcc.Items.Add(o);
                }
            }
        }

        //events, when UC is added
        public event EventHandler<AccountsEventArgs> AddItem;
        public void OnAddItem(Account rem)
        {
            AccountsEventArgs e = new AccountsEventArgs();
            e.OldObject = rem;
            e.NewObject = SelectedOtherItem;
            if (AddItem != null) AddItem(this, e);
        }

        //event, when UC is removed
        public event EventHandler RemoveItem;
        public void OnRemoveItem()
        {
            if (RemoveItem != null) RemoveItem(this, EventArgs.Empty);
        }

        //click on "-" button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnRemoveItem();
        }

        //updating selected other item when combobox selection is changed
        private void cmbSvrAcc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedOtherItem = (Account)cmbSvrAcc.SelectedItem;
            Account obj = null;

            if (e.RemovedItems.Count > 0)
                obj = (Account)e.RemovedItems[0];

            OnAddItem(obj);
        }
    }
}
