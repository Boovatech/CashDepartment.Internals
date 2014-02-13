using CashDepartment.Shared.ComponentModel;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using System;
using System.ComponentModel;
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

        public string d { get { return "dsa"; } }

        public RelayCommand AddNewRowCommand { get; set; }
        public RelayCommand AddNewRowParamCommand { get; set; }

        #endregion

        #region Constructors

        public MetaDataContentViewModel()
        {           
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
            this.AddNewRowParamCommand = new RelayCommand(arg => this.AddNewRowParam(arg));
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
            trans.Params.AddNew();
            dataList.Add(trans);        
        }

        private void AddNewRowParam(object arg)
        {
            var dataList = arg as BindingListEx<TransactionMetadataParams>;
            dataList.AddNew();
            if (dataList.Count == 1)
            {
                DataGridColumnBinding.GetInstance().BindingRun();
            }
        }  

        #endregion
    }
}
