using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.TransactionsConfig.Shell.ViewModel;
using FirstFloor.ModernUI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CashDepartment.TransactionsConfig.Shell.Content
{
    /// <summary>
    /// Interaction logic for MetaDataContent.xaml
    /// </summary>
    public partial class MetaDataContent : UserControl, IContent
    {
        public MetaDataContent()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var currentBusinessProcessSourceType = e.Fragment;
            (this.DataContext as MetaDataContentViewModel).NavigateTo(currentBusinessProcessSourceType);            
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }

        private void ColBinding()
        {
            var lb = VisualHelper.FindChild<ListBox>(this, "lbMain");
            var dg = VisualHelper.FindChild<DataGrid>(this, "dgMain");
            var items = lb.Items;

            foreach (var item in items)
            {
                DependencyObject obj = lb.ItemContainerGenerator.ContainerFromItem(item);
                var dataG = VisualHelper.FindChild<DataGrid>(obj, "dataInclude");
                if (dataG != null)
                {
                    foreach (var colD in dataG.Columns)
                    {
                        var col = dg.Columns.First(x => x.Header.ToString() == colD.Header.ToString());
                        if (col != null)
                        {
                            Binding myBinding = new Binding("Width");
                            myBinding.Mode = BindingMode.TwoWay;
                            myBinding.Source = col;
                            BindingOperations.SetBinding(colD, DataGridColumn.WidthProperty, myBinding);
                        }
                    }
                }
            }
        }

        private void dgMain_Loaded(object sender, RoutedEventArgs e)
        {
            this.ColBinding();
            //var dg = sender as DataGrid;


            //PropertyDescriptor pd = DependencyPropertyDescriptor.FromProperty(DataGridColumn.ActualWidthProperty, typeof(DataGridColumn));

            //foreach (DataGridColumn column in dg.Columns)
            //{
            //    pd.AddValueChanged(column, new EventHandler(ColumnWidthPropertyChanged));
            //}
        }

        //private void ColumnWidthPropertyChanged(object sender, EventArgs e)
        //{
        //    var col = sender as DataGridColumn;

        //    if (col != null)
        //    {
        //        var lb = CashDepartment.TransactionsConfig.Shell.Data.VisualHelper.FindChild<ListBox>(this, "lbMain");
        //        var items = lb.Items;
        //        foreach (var item in items)
        //        {
        //            var dataG = CashDepartment.TransactionsConfig.Shell.Data.VisualHelper.FindChild<DataGrid>(lb, "dataInclude");
        //            foreach (var colD in dataG.Columns)
        //            {
        //                if (col.Header.ToString() == colD.Header.ToString())
        //                {
        //                    colD.Width = col.Width;
        //                    return;
        //                }
        //            }
        //        }

        //    }
        //}

    }
}
