namespace CashDepartment.TransactionsConfig.Programm.ViewModel
{
    using CashDepartment.TransactionsConfig.Data;
    using CashDepartment.WellKnownBusinessObjects;
    using FirstFloor.ModernUI.Presentation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class MainWindowViewModel
    {
        private FirstFloor.ModernUI.Presentation.LinkGroupCollection linkGroupCollection;

        #region Constructors

        public MainWindowViewModel(FirstFloor.ModernUI.Presentation.LinkGroupCollection linkGroupCollection)
        {
            // TODO: Complete member initialization
            this.linkGroupCollection = linkGroupCollection;
            this.MenuInit();
        }

        #endregion

        #region Methods

        private void MenuInit()
        {
            var menuCollection = this.linkGroupCollection.First((x) => { return x.GroupName == "MetaDataGroup"; });

            if(menuCollection != null)
            {
                foreach (var s in Enum.GetNames(typeof(BusinessProcessSourceType)))
                {
                    if (s == "None") continue;
                    var link = new Link();
                    link.DisplayName = s;
                    link.Source = new Uri(string.Format("/Content/MainContent.xaml#{0}", s), UriKind.Relative);
                    menuCollection.Links.Add(link);                    
                }
            }           
        }

        #endregion

        #region Data

        //private TransactionMetadataGroup menuItem;

        #endregion


    }
}
