using CashDepartment.Shared.ComponentModel;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    class MetaDataContentViewModel
    {
         #region Data

        private BusinessProcessSourceType currentBusinessProcessSourceType;
        private ObservableCollection<TransactionMetadataGroup> dataCollection;
        private bool isFirstNavigate;        

        #endregion

        #region Properties

        public ObservableCollection<TransactionMetadataGroup> DataCollection
        {
            get { return this.dataCollection; }
            private set { this.dataCollection = value; }
        }

        public RelayCommand AddNewRowCommand { get; set; }

        #endregion

        #region Constructors

        public MetaDataContentViewModel()
        {
            this.DataCollection = new ObservableCollection<TransactionMetadataGroup>();
            this.isFirstNavigate = true;
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
        }

        #endregion

        #region Methods

        internal void NavigateTo(string currentBusinessProcessSourceType)
        {            
            //if (this.isFirstNavigate)
            //{
                this.currentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
                //this.isFirstNavigate = false;
                switch (this.currentBusinessProcessSourceType)
                {
                    case BusinessProcessSourceType.Atm:
                        break;
                    case BusinessProcessSourceType.CashCenter:
                        break;
                    case BusinessProcessSourceType.Client:
                        break;
                    case BusinessProcessSourceType.Interbank:
                        this.InterbankInit();
                        break;
                    case BusinessProcessSourceType.Terminal:
                        break;
                    case BusinessProcessSourceType.Unit:
                        break;
                    case BusinessProcessSourceType.None:
                        break;
                }
            //}
        }

        private async void InterbankInit()
        {            
            await Task.Factory.StartNew(() =>
            {
                var curItems = AllData.GetInstance().DataCollection
                .Where(
                x => x.ProcessSourceType == this.currentBusinessProcessSourceType);               
                App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.DataCollection.Clear();
                    foreach (var item in curItems)
                    {
                        this.DataCollection.Add(item);
                    }
                }), System.Windows.Threading.DispatcherPriority.DataBind);
            });     
        }

        public void AddNewRow(object arg)
        {
            switch (this.currentBusinessProcessSourceType)
            {
                case BusinessProcessSourceType.Atm:
                    break;
                case BusinessProcessSourceType.CashCenter:
                    break;
                case BusinessProcessSourceType.Client:
                    break;
                case BusinessProcessSourceType.Interbank:
                    var dataList = arg as BindingListEx<TransactionMetadata>;
                    var meta = new InterbankEncashTransactionMetadata();
                    meta.Params = new BindingListEx<TransactionMetadataParams>();
                    dataList.Add(meta);
                    break;
                case BusinessProcessSourceType.Terminal:
                    break;
                case BusinessProcessSourceType.Unit:
                    break;
                case BusinessProcessSourceType.None:
                    break;
            }
        }

        #endregion
    }
}
