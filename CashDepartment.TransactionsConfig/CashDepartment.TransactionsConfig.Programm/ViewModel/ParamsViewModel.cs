using CashDepartment.Shared.ComponentModel;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    public class ParamsViewModel
    {
         #region Data

        private BusinessProcessSourceType currentBusinessProcessSourceType;
        private CollectionViewSource collectionViewSource;      

        #endregion

        #region Properties

        public ICollectionView DataCollection
        {
            get
            {
                return this.collectionViewSource.View;
            }
        }

        public RelayCommand AddNewRowCommand { get; set; }

        #endregion

        #region Constructors

        public ParamsViewModel()
        {
            this.collectionViewSource = new CollectionViewSource();
            this.collectionViewSource.Source = AllData.GetInstance().DataCollection;
            this.collectionViewSource.Filter += collectionViewSource_Filter;
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
        }       

        #endregion

        #region Methods

        void collectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = (e.Item as TransactionMetadataGroup).ProcessSourceType == this.currentBusinessProcessSourceType ? true : false;
        }

        internal void FrameNavigate(string currentBusinessProcessSourceType)
        {
            this.currentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
            this.collectionViewSource.View.Refresh();
        }

        private void AddNewRow(object arg)
        {
            var dataList = arg as BindingListEx<TransactionMetadataParams>;
            dataList.AddNew();
        }

        #endregion
    }
}
