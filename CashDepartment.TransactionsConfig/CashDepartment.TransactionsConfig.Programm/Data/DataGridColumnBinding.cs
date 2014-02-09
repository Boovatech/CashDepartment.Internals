using CashDepartment.TransactionsConfig.Data;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;

namespace CashDepartment.TransactionsConfig.Shell.Data
{
    public class DataGridColumnBinding
    {
        private static DataGridColumnBinding dataGridColumnBinding;
        private object dataLocker;

        private DataGridColumnBinding() 
        {
            this.dataLocker = new object();
        }

        static DataGridColumnBinding() { }

        public static DataGridColumnBinding GetInstance()
        {
            if (dataGridColumnBinding == null)
            {
                lock (typeof(DataGridColumnBinding))
                {
                    if (dataGridColumnBinding == null)
                    {
                        dataGridColumnBinding = new DataGridColumnBinding();
                    }
                }
            }
            return dataGridColumnBinding;
        }
       

        public void BindingRun()
        {
            lock (this.dataLocker)
            {
                var uc = VisualHelper.FindChild<UserControl>(App.Current.MainWindow, "MainContentControl");
                var mf = VisualHelper.FindChild<FirstFloor.ModernUI.Windows.Controls.ModernFrame>(uc, "myContentFrame");
                var tcc = VisualHelper.FindChild<FirstFloor.ModernUI.Windows.Controls.TransitioningContentControl>(mf, null);
                var ccps = VisualHelper.FindChild<ContentPresenter>(tcc, "CurrentContentPresentationSite") as ContentPresenter;
                var lb = VisualHelper.FindChild<ListBox>(ccps, "lbMain");
                var dg = VisualHelper.FindChild<DataGrid>(ccps, "dgMain");
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
                                Binding myBinding = new Binding("ActualWidth");
                                myBinding.Mode = BindingMode.OneWay;
                                myBinding.Source = col;
                                BindingOperations.SetBinding(colD, DataGridColumn.WidthProperty, myBinding);
                            }
                        }
                    }
                }
            }
        }
    }
}
