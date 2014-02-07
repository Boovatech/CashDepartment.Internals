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
using System.Windows;
using System.Windows.Controls;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    class MetaDataContentViewModel:NotifyPropertyChanged
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

        public DataGrid DataGridContent { get; set; }

        #endregion

        #region Constructors

        public MetaDataContentViewModel()
        {
            this.DataCollection = new ObservableCollection<TransactionMetadataGroup>();
            this.isFirstNavigate = true;
            this.AddNewRowCommand = new RelayCommand(arg => this.AddNewRow(arg));
            this.DataGridContent = new DataGrid();
        }

        #endregion

        #region Methods

        internal void NavigateTo(string currentBusinessProcessSourceType)
        {
            //if (this.isFirstNavigate)
            //{
            this.CurrentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
            this.BusinessProcessInit();           


            ////this.isFirstNavigate = false;
            //switch (this.CurrentBusinessProcessSourceType)
            //{
            //    case BusinessProcessSourceType.Atm:
            //        //this.AtmInit();
            //        break;
            //    case BusinessProcessSourceType.CashCenter:
            //        break;
            //    case BusinessProcessSourceType.Client:
            //        break;
            //    case BusinessProcessSourceType.Interbank:
            //        //this.InterbankInit();
            //        break;
            //    case BusinessProcessSourceType.Terminal:
            //        break;
            //    case BusinessProcessSourceType.Unit:
            //        break;
            //    case BusinessProcessSourceType.None:
            //        break;
            //}
            ////}
        }

        //private void AtmInit()
        //{
        //    this.DataGridContent.Columns.Clear();
        //    var col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colResult");
        //    col1.Header = "Result";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colDefaultCurrency");
        //    col1.Header = "DefaultCurrency";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colCreationTime");
        //    col1.Header = "CreationTime";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colisReserve");
        //    col1.Header = "isReserve";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colisOutCases");
        //    col1.Header = "isOutCases";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colwithoutDoc");
        //    col1.Header = "withoutDoc";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colisReturn");
        //    col1.Header = "isReturn";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colReturnType");
        //    col1.Header = "IsReturnType";
        //    this.DataGridContent.Columns.Add(col1);           
        //}

        //private void InterbankInit()
        //{
        //    this.DataGridContent.Columns.Clear();
        //    var col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colResult");
        //    col1.Header = "Result";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colDefaultCurrency");
        //    col1.Header = "DefaultCurrency";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colPartyType");
        //    col1.Header = "PartyType";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colIsOurBank");
        //    col1.Header = "IsOurBank";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colIsResident");
        //    col1.Header = "IsResident";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colSolvency");
        //    col1.Header = "Solvency";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colIsExchange");
        //    col1.Header = "IsExchange";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colIsDefaultCurrency");
        //    col1.Header = "IsDefaultCurrency";
        //    this.DataGridContent.Columns.Add(col1);
        //    col1 = new FirstFloor.ModernUI.Windows.Controls.DataGridComboBoxColumn();
        //    col1.SetValue(FrameworkElement.NameProperty, "colIsPrepaid");
        //    col1.Header = "IsPrepaid";
        //    this.DataGridContent.Columns.Add(col1);
        //}


        private async void BusinessProcessInit()
        {
            await Task.Factory.StartNew(() =>
            {
                var curItems = AllData.GetInstance().DataCollection
                .Where(
                x => x.ProcessSourceType == this.CurrentBusinessProcessSourceType);
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

        //private async void InterbankInit()
        //{            
        //    await Task.Factory.StartNew(() =>
        //    {
        //        var curItems = AllData.GetInstance().DataCollection
        //        .Where(
        //        x => x.ProcessSourceType == this.CurrentBusinessProcessSourceType);               
        //        App.Current.Dispatcher.BeginInvoke((Action)(() =>
        //        {
        //            this.DataCollection.Clear();
        //            foreach (var item in curItems)
        //            {
        //                this.DataCollection.Add(item);
        //            }
        //        }), System.Windows.Threading.DispatcherPriority.DataBind);
        //    });     
        //}

        public void AddNewRow(object arg)
        {
            var dataList = arg as BindingListEx<TransactionMetadata>;
            TransactionMetadata meta = new DefaultTransactionMetadata();

            switch (this.CurrentBusinessProcessSourceType)
            {
                case BusinessProcessSourceType.Atm:
                    meta = new AtmInCashTransactionMetadata();   
                    break;
                case BusinessProcessSourceType.CashCenter:                
                    break;
                case BusinessProcessSourceType.Client:                   
                    break;
                case BusinessProcessSourceType.Interbank:
                    meta = new InterbankEncashTransactionMetadata();                    
                    break;
                case BusinessProcessSourceType.Terminal:                    
                    break;
                case BusinessProcessSourceType.Unit:                    
                    break;
                case BusinessProcessSourceType.None:
                    meta = new DefaultTransactionMetadata();   
                    break;
            }

            meta.Params = new BindingListEx<TransactionMetadataParams>();
            dataList.Add(meta);
        }

        #endregion
    }
}
