using CashDepartment.Shared.ComponentModel;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    class MetaDataContentViewModel:NotifyPropertyChanged
    {
         #region Data

        private BusinessProcessSourceType currentBusinessProcessSourceType;   
        private CollectionViewSource  collectionViewSource;

        #endregion

        #region Properties

        public ICollectionView DataCollection
        {
            get
            {
                return this.collectionViewSource.View;
            }
        }

        public BusinessProcessSourceType CurrentBusinessProcessSourceType
        {
            get { return this.currentBusinessProcessSourceType; }
            private set
            {
                if (this.currentBusinessProcessSourceType != value)
                {
                    this.currentBusinessProcessSourceType = value;
                    this.OnPropertyChanged("CurrentBusinessProcessSourceType");
                }
            }
        }

        public RelayCommand AddNewRowCommand { get; set; }

        #endregion

        #region Constructors

        public MetaDataContentViewModel()
        {           
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
            this.collectionViewSource = new CollectionViewSource();
            this.collectionViewSource.Source = TransactionDataContext.GetInstance().DataCollection;
            this.collectionViewSource.Filter += collectionViewSource_Filter;
        }       

        #endregion

        #region Methods

        void collectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = (e.Item as TransactionMetadataGroup).ProcessSourceType == this.CurrentBusinessProcessSourceType ? true : false;
        }

        internal void NavigateTo(string currentBusinessProcessSourceType)
        {
            this.CurrentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
            this.collectionViewSource.View.Refresh();
        }             

        public void AddNewRow(object arg)
        {
            var dataList = arg as BindingListEx<TransactionMetadata>;
            Type type = dataList[0].GetType();
            var trans = Activator.CreateInstance(type) as TransactionMetadata;
            trans.Params = new BindingListEx<TransactionMetadataParams>();
            dataList.Add(trans);        
        }

        #endregion
    }
}
