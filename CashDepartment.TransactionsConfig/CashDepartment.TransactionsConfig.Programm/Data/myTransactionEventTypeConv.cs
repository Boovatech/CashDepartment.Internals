using CashDepartment.Shared.Utils;
using CashDepartment.WellKnownBusinessObjects.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CashDepartment.TransactionsConfig.Programm.Data
{
    public class myTransactionEventTypeConv:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var enumValue = (TransactionEvent)value;
            var enumlocal = EnumHelper.GetLocalizedValuesList(enumValue.GetType());
            var friendlyName = enumlocal.First(x => x.Key.ToString() == enumValue.ToString());
            return friendlyName.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
