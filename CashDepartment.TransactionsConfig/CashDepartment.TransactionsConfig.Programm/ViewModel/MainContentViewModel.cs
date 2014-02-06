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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Serialization;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    public class MainContentViewModel : NotifyPropertyChanged
    {

        #region Data

        private BusinessProcessSourceType currentBusinessProcessSourceType;
        private bool isFirstNavigate;
        private Uri frameSource;

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
        #endregion

        #region Constructors

        public MainContentViewModel()
        {
            this.TransactionEventsList = EnumHelper.GetLocalizedValuesList(typeof(TransactionEvent)).Select(x=>x.Value).ToList<string>();
            this.TransactionExportODBTypeList = EnumHelper.GetLocalizedValuesList(typeof(TransactionExportODBType)).Select(x => x.Value).ToList<string>();
            this.isFirstNavigate = true;
            this.GoToParamsOrMetaDataCommand = new RelayCommand(arg => this.GoToParamsOrMetaData(arg));
            this.MySaveCommand = new RelayCommand(arg => this.Save());
            this.AddNewTransactionEventCommand = new RelayCommand(arg => this.AddNewTransactionEven());
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
                    if (!mf.Source.OriginalString.Contains("Params"))
                    {
                        System.Windows.Input.NavigationCommands.GoToPage.Execute(string.Format("/Content/Params.xaml#{0}", this.currentBusinessProcessSourceType), mf);
                    }
                    else
                    {
                        System.Windows.Input.NavigationCommands.GoToPage.Execute(string.Format("/Content/MetaDataContent.xaml#{0}", this.currentBusinessProcessSourceType), mf);
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
                sfd.Filter = "Файл проводки (*.provodka)|*.provodka";

                if (sfd.ShowDialog().Value)
                {
                    if (!File.Exists(sfd.FileName))
                    {
                        File.Create(sfd.FileName).Close();
                    }
                    XmlSerializer xml = new XmlSerializer(CashDepartment.TransactionsConfig.Shell.Data.AllData.GetInstance().DataCollection.GetType());
                    using (StreamWriter wr = new StreamWriter(sfd.FileName))
                    {
                        xml.Serialize(wr, CashDepartment.TransactionsConfig.Shell.Data.AllData.GetInstance().DataCollection);
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

            switch (this.currentBusinessProcessSourceType)
            {
                case BusinessProcessSourceType.Atm:
                    break;
                case BusinessProcessSourceType.CashCenter:
                    break;
                case BusinessProcessSourceType.Client:
                    break;
                case BusinessProcessSourceType.Interbank:
                    var meta = new InterbankEncashTransactionMetadata();
                    meta.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
                    metaDataList.Add(meta);
                    break;
                case BusinessProcessSourceType.Terminal:
                    break;
                case BusinessProcessSourceType.Unit:
                    break;
                case BusinessProcessSourceType.None:
                    break;
            }

            tmg.Metadata = metaDataList;
            AllData.GetInstance().DataCollection.Add(tmg);

            ////временное решение
            //var frame = VisualHelper.FindChild<ModernFrame>(App.Current.Windows[0], "ContentFrame");            
            //System.Windows.Input.NavigationCommands.Refresh.Execute(null, frame);  
            var tmp = this.FrameSource;
            this.FrameSource = null;
            this.FrameSource = tmp;
        }


        internal void NavigateTo(string currentBusinessProcessSourceType)
        {
            //if (this.isFirstNavigate)
            //{
                this.currentBusinessProcessSourceType = (BusinessProcessSourceType)Enum.Parse(typeof(BusinessProcessSourceType), currentBusinessProcessSourceType);
                //this.isFirstNavigate = false;
                this.FrameSource = null;
                switch (this.currentBusinessProcessSourceType)
                {
                    case BusinessProcessSourceType.Atm:
                        break;
                    case BusinessProcessSourceType.CashCenter:
                        break;
                    case BusinessProcessSourceType.Client:
                        break;
                    case BusinessProcessSourceType.Interbank:
                        this.FrameSource = new Uri(string.Format("/Content/MetaDataContent.xaml#{0}", this.currentBusinessProcessSourceType), UriKind.Relative);
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
        #endregion
    }
}
