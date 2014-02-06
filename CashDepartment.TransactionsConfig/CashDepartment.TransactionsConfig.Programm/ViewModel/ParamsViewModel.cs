using CashDepartment.Shared.ComponentModel;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Programm.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDepartment.TransactionsConfig.Programm.ViewModel
{
    public class ParamsViewModel
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

        public RelayCommand AddNewRowCommand;

        #endregion

        #region Constructors

        public ParamsViewModel()
        {
            this.DataCollection = new ObservableCollection<TransactionMetadataGroup>();
            this.isFirstNavigate = true;
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
        }       

        #endregion

        #region Methods

        internal void FrameNavigate(string currentBusinessProcessSourceType)
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

        private void AddNewRow(object arg)
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
                    dataList.Add(new InterbankEncashTransactionMetadata());
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
