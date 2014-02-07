using CashDepartment.TransactionsConfig.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace CashDepartment.TransactionsConfig.Shell.Data
{
    public class ListViewMetaDataTemplateSelector : DataTemplateSelector
    {

        public DataGrid DG { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is TransactionMetadataGroup)
            {
                TransactionMetadataGroup Item = item as TransactionMetadataGroup;
                Window window = Application.Current.MainWindow;
             
                var metaType = Item.Metadata.First().GetType();

                if (metaType == typeof(InterbankEncashTransactionMetadata))
                {
                    return element.FindResource("ListViewMainDataTemplateInterbank") as DataTemplate;
                }
                else if (metaType == typeof(AtmInCashTransactionMetadata))
                {
                    return element.FindResource("ListViewMainDataTemplateAtm") as DataTemplate;
                } 
            }
            return null;
        }
    }
}
