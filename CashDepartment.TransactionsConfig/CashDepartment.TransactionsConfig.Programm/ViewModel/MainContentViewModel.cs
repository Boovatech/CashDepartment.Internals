using CashDepartment.Shared.Utils;
using CashDepartment.TransactionsConfig.Data;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using CashDepartment.WellKnownBusinessObjects.Transactions;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    public class MainContentViewModel : NotifyPropertyChanged
    {

        #region Data

        private BusinessProcessSourceType currentBusinessProcessSourceType;
        private Uri frameSource;
        private System.Windows.Visibility leftButtonVisibility;
        private System.Windows.Visibility rightButtonVisibility;

        public Uri FrameSource
        {
            get { return this.frameSource; }

            private set
            {
                if(this.frameSource != value)
                {
                    this.frameSource = value;

                    OnPropertyChanged("FrameSource");
                }
            }
        }

        public RelayCommand GoToParamsOrMetaDataCommand { get; set; }  
        public RelayCommand MySaveCommand { get; set; }
        public RelayCommand AddNewTransactionEventCommand { get; set; }


        public List<string> TransactionEventsList { get; set; }
        public List<string> TransactionExportODBTypeList { get; set; }
        public string TransactionEventsSelectedItem { get; set; }
        public string TransactionExportODBTypeSelectedItem { get; set; }

        public System.Windows.Visibility LeftButtonVisibility
        {
            get { return this.leftButtonVisibility; }

            private set
            {
                if (this.leftButtonVisibility != value)
                {
                    this.leftButtonVisibility = value;

                    OnPropertyChanged("LeftButtonVisibility");
                }
            }
        }
        public System.Windows.Visibility RightButtonVisibility
        {
            get { return this.rightButtonVisibility; }

            private set
            {
                if (this.rightButtonVisibility != value)
                {
                    this.rightButtonVisibility = value;

                    OnPropertyChanged("RightButtonVisibility");
                }
            }
        }

        #endregion

        #region Constructors

        public MainContentViewModel()
        {
            this.TransactionEventsList = EnumHelper.GetLocalizedValuesList(typeof(TransactionEvent)).Select(x=>x.Value).ToList<string>();
            this.TransactionExportODBTypeList = EnumHelper.GetLocalizedValuesList(typeof(TransactionExportODBType)).Select(x => x.Value).ToList<string>();
            this.GoToParamsOrMetaDataCommand = new RelayCommand(arg => this.GoToParamsOrMetaData(arg));
            this.MySaveCommand = new RelayCommand(arg => this.Save());
            this.AddNewTransactionEventCommand = new RelayCommand(arg => this.AddNewTransactionEven());
            this.LeftButtonVisibility = System.Windows.Visibility.Hidden;
            this.RightButtonVisibility = System.Windows.Visibility.Hidden;
        }       
      
        #endregion

        #region Methods

        private void GoToParamsOrMetaData(object arg)
        {
            var mf = arg as ModernFrame;

            try
            {
                if (mf != null)
                {
                    var tcc = VisualHelper.FindChild<TransitioningContentControl>(mf, null);
                    if (!mf.Source.OriginalString.Contains("Params"))
                    {
                        tcc.Transition = "MyToLeftTransition";
                        System.Windows.Input.NavigationCommands.GoToPage.Execute(string.Format("/Content/Params.xaml#{0}", this.currentBusinessProcessSourceType), mf);                    
                        this.LeftButtonVisibility = System.Windows.Visibility.Visible;
                        this.RightButtonVisibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        tcc.Transition = "MyToRightTransition";
                        System.Windows.Input.NavigationCommands.GoToPage.Execute(string.Format("/Content/MetaDataContent.xaml#{0}", this.currentBusinessProcessSourceType), mf);
                        this.LeftButtonVisibility = System.Windows.Visibility.Hidden;
                        this.RightButtonVisibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch(Exception ex)
            {
#if DEBUG
                System.Windows.MessageBox.Show("Ex -> GoToParamsOrMetaData: " + ex.Message);
#endif
            }
        }


        private void Save()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Файл проводки (*.trs)|*.trs";

                if (sfd.ShowDialog().Value)
                {
                    if (!File.Exists(sfd.FileName))
                    {
                        File.Create(sfd.FileName).Close();
                    }
                    XmlSerializer xml = new XmlSerializer(CashDepartment.TransactionsConfig.Shell.Data.TransactionDataContext.GetInstance().DataCollection.GetType());
                    using (StreamWriter wr = new StreamWriter(sfd.FileName))
                    {
                        xml.Serialize(wr, CashDepartment.TransactionsConfig.Shell.Data.TransactionDataContext.GetInstance().DataCollection);
                    }
                }              
            }
            catch(Exception ex)
            {
#if DEBUG
                System.Windows.MessageBox.Show("Ex -> Save: " + ex.Message);
#endif
            }
        }

        private void AddNewTransactionEven()
        {
            var tmg = new TransactionMetadataGroup();
            tmg.ProcessSourceType = this.currentBusinessProcessSourceType;
            var strEnum = EnumHelper.GetLocalizedValuesList(typeof(TransactionEvent)).Where(x=>x.Value == this.TransactionEventsSelectedItem).Select(x=>x.Key.ToString());
            foreach(var item in strEnum)
            {
                tmg.TransactionEvent = (TransactionEvent)Enum.Parse(typeof(TransactionEvent), item);
                break;
            }

            strEnum = EnumHelper.GetLocalizedValuesList(typeof(TransactionExportODBType)).Where(x => x.Value == this.TransactionExportODBTypeSelectedItem).Select(x => x.Key.ToString());
            foreach (var item in strEnum)
            {
                tmg.ODBType = (TransactionExportODBType)Enum.Parse(typeof(TransactionExportODBType), item);
                break;
            }

            var metaDataList = new Shared.ComponentModel.BindingListEx<TransactionMetadata>();

            TransactionMetadata meta = new DefaultTransactionMetadata();

            switch (this.currentBusinessProcessSourceType)
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

            meta.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
            meta.Params.AddNew();
            metaDataList.Add(meta);         

            tmg.Metadata = metaDataList;

            TransactionDataContext.GetInstance().AddToDataCollection(tmg);           
        }


        internal void NavigateTo(string currentBusinessProcessSourceType)
        {
            this.currentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
            this.FrameSource = null;
            this.FrameSource = new Uri(string.Format("/Content/MetaDataContent.xaml#{0}", this.currentBusinessProcessSourceType), UriKind.Relative);
            //this.LeftButtonVisibility = System.Windows.Visibility.Hidden;
            //this.RightButtonVisibility = System.Windows.Visibility.Visible;
        }    
        #endregion
    }
}
