using CashDepartment.WellKnownBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CashDepartment.TransactionsConfig.Shell.Data
{
    public class ContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is BusinessProcessSourceType)
            {
                var s = (BusinessProcessSourceType)item;
              
                switch (s)
                {
                    case BusinessProcessSourceType.Atm:
                        return element.FindResource("atmTemplate") as DataTemplate;                       
                    case BusinessProcessSourceType.Interbank:
                        return element.FindResource("interbankTemplate") as DataTemplate;                       
                }                
            }
            return null; 
        }
    }
}
