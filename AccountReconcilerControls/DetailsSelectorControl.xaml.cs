using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Логика взаимодействия для DetailsSelectorControl.xaml
    /// </summary>
    public partial class DetailsSelectorControl : UserControl, INotifyPropertyChanged
    {
        public DetailsSelectorControl()
        {
            InitializeComponent();

            MainReturnedResult = new DetailsControlReturned() { IsOrChecked = true };
        }

        //result of main textbox
        private DetailsControlReturned mainReturnedResult;
        public DetailsControlReturned MainReturnedResult
        {
            get { return mainReturnedResult; }
            set { mainReturnedResult = value; OnPropertyChanged("MainReturnedResult"); }
        }

        //adding new UC element
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DetailsSelectorSubelementControl dsc = new DetailsSelectorSubelementControl();
            stcScnd.Children.Add(dsc);
            //ReturnedResults.Add(MainReturnedResult);
            ReturnedResults.Add(dsc.ReturnResult);

            dsc.RemoveItem += new EventHandler(dsc_RemoveItem);
        }

        //Remove UC element
        void dsc_RemoveItem(object sender, EventArgs e)
        {
            DetailsSelectorSubelementControl scnd = (DetailsSelectorSubelementControl)sender;

            ReturnedResults.Remove(scnd.ReturnResult);

            stcScnd.Children.Remove(scnd);
        }

        //Result collection, it has be binding to my source
        public ObservableCollection<DetailsControlReturned> ReturnedResults
        {
            get { return (ObservableCollection<DetailsControlReturned>)GetValue(ReturnedResultsProperty); }
            set { SetValue(ReturnedResultsProperty, value); }
        }

        public static readonly DependencyProperty ReturnedResultsProperty =
            DependencyProperty.Register("ReturnedResults", typeof(ObservableCollection<DetailsControlReturned>), typeof(DetailsSelectorControl), new FrameworkPropertyMetadata(new ObservableCollection<DetailsControlReturned>()));

        //temp method - have to be deleted
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    foreach (var d in ReturnedResults)
        //    {
        //        Debug.WriteLine(d.IsOrChecked + " " + d.ComparsionDetailsString);
        //    }

        //    DetMainCotnrol.ReturnedResults.Clear();

        //    DetMainCotnrol.ReturnedResults.Add(MainReturnedResult);

        //    foreach (var c in DetMainCotnrol.stcScnd.Children)
        //    {
        //        if ((c as DetailsSelectorSubelementControl) != null)
        //        {
        //            DetMainCotnrol.ReturnedResults.Add(((DetailsSelectorSubelementControl)c).ReturnResult);
        //        }
        //    }
        //}

        //updating main txb result

        private void tbTitleText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ReturnedResults.Contains(MainReturnedResult)) ReturnedResults.Add(MainReturnedResult);
            MainReturnedResult.ComparsionDetailsString = tbTitleText.Text;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DetailsSelectorControl), new FrameworkPropertyMetadata("Title 1", new PropertyChangedCallback(OnTitleChanged)));

        public static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DetailsSelectorControl uc = (DetailsSelectorControl)sender;
            if (e.Property == TitleProperty)
                uc.txbTitle.Text = (string)e.NewValue;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
