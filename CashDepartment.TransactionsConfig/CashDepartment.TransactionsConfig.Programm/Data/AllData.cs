using CashDepartment.TransactionsConfig.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CashDepartment.TransactionsConfig.Shell.Data
{
    public class AllData
    {
        private static AllData allData;
        private object dataLocker;
        private ObservableCollection<TransactionMetadataGroup> dataCollection;

        private AllData() 
        {
            this.dataLocker = new object();
            this.DataCollection = new ObservableCollection<TransactionMetadataGroup>();
        }

        public void LoadData(string path)
        {
            try
            {
                //System.Threading.Thread.Sleep(5000);
                this.ClearData();
                if (File.Exists(path))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<TransactionMetadataGroup>));
                    using (StreamReader sr = new StreamReader(path))
                    {
                        this.dataCollection = xml.Deserialize(sr) as ObservableCollection<TransactionMetadataGroup>;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Windows.MessageBox.Show("Ex -> LoadData: " + ex.Message);
#endif
            }
        }

        //private void InitData()
        //{
        //    var temp1 = new TransactionMetadataGroup();
        //    temp1.TransactionEvent = WellKnownBusinessObjects.Transactions.TransactionEvent.BackOffice_PaymentForInCash;
        //    temp1.ODBType = WellKnownBusinessObjects.Transactions.TransactionExportODBType.Aval;
        //    temp1.ProcessSourceType = WellKnownBusinessObjects.BusinessProcessSourceType.Interbank;               

        //    temp1.Metadata = new Shared.ComponentModel.BindingListEx<TransactionMetadata>();

        //    var temp2 = new InterbankEncashTransactionMetadata();
        //    temp2.EventResult = CashDepartment.WellKnownBusinessObjects.Transactions.TransactionEventResult.NonPayment;
        //    temp2.ForDefaultCurrency = true;
        //    temp2.IsOurBank = true;
        //    temp2.IsResident = true;
        //    temp2.Solvency = WellKnownBusinessObjects.Enquiries.EnquirySolvency.Dilapidated;
        //    temp2.IsExchange = true;
        //    temp2.IsDefaultCurrency = true;
        //    temp2.IsPrepaid = true;
        //    temp2.PartyType = WellKnownBusinessObjects.Core.PartyType.Bank;
        //    temp2.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
        //    var temp3 = new TransactionMetadataParams();
        //    temp3.AdditionalParams ="testAddParams";
        //    temp3.Amount = Server.DomainModel.Enities.Transactions.TransactionAmount.Declared;
        //    temp3.CreditSource = WellKnownBusinessObjects.BusinessProcessSourceType.Atm;
        //    temp3.CreditType = WellKnownBusinessObjects.Core.AccountType.BankAccountsReceivable;
        //    temp3.DebitSource = WellKnownBusinessObjects.BusinessProcessSourceType.CashCenter;
        //    temp3.DebitType = WellKnownBusinessObjects.Core.AccountType.CashDepartment;
        //    temp3.DocType = WellKnownBusinessObjects.Transactions.TransactionDocType.Aval_DebitRequest;
        //    temp3.IgnoreDirty = true;
        //    temp3.IsUnion = true;
        //    temp3.NeedSymbol = true;
        //    temp3.PaymentPurpose = WellKnownBusinessObjects.Transactions.PaymentPurposeType.ATM_InCash_Unloading_EnrollmentFactAmount;
        //    temp3.Subtype = new WellKnownBusinessObjects.Transactions.TransactionSubtype();
        //    temp3.Symbol = "d";
        //    temp3.IsUnion = true;
        //    temp3.NeedSymbol = true;
        //    temp3.Type = WellKnownBusinessObjects.Transactions.TransactionType.ATMUnloading;
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp1.Metadata.Add(temp2);

        //    temp2 = new InterbankEncashTransactionMetadata();
        //    temp2.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
        //    temp3 = new TransactionMetadataParams();
        //    temp3.AdditionalParams = "testAddParams1";
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp2.Params.Add(temp3);
        //    temp1.Metadata.Add(temp2);

        //    this.DataCollection.Add(temp1);

        //    temp1 = new TransactionMetadataGroup();
        //    temp1.TransactionEvent = WellKnownBusinessObjects.Transactions.TransactionEvent.RecCenter_InputRecalculated;
        //    temp1.ODBType = WellKnownBusinessObjects.Transactions.TransactionExportODBType.Aval;
        //    temp1.ProcessSourceType = WellKnownBusinessObjects.BusinessProcessSourceType.Interbank;
        //    temp1.Metadata = new Shared.ComponentModel.BindingListEx<TransactionMetadata>();

        //    temp2 = new InterbankEncashTransactionMetadata();
        //    temp2.EventResult = CashDepartment.WellKnownBusinessObjects.Transactions.TransactionEventResult.NonPayment;
        //    temp2.ForDefaultCurrency = true;
        //    temp2.IsOurBank = true;
        //    temp2.IsResident = true;
        //    temp2.Solvency = WellKnownBusinessObjects.Enquiries.EnquirySolvency.Dilapidated;
        //    temp2.IsExchange = true;
        //    temp2.IsDefaultCurrency = true;
        //    temp2.IsPrepaid = true;
        //    temp2.PartyType = WellKnownBusinessObjects.Core.PartyType.Bank;
        //    temp2.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
        //    temp3 = new TransactionMetadataParams();
        //    temp3.AdditionalParams = "testAddParams2";
        //    temp2.Params.Add(temp3);
        //    temp1.Metadata.Add(temp2);

        //    temp2 = new InterbankEncashTransactionMetadata();
        //    temp2.Params = new Shared.ComponentModel.BindingListEx<TransactionMetadataParams>();
        //    temp3 = new TransactionMetadataParams();
        //    temp3.AdditionalParams = "testAddParams3";
        //    temp2.Params.Add(temp3);
        //    temp1.Metadata.Add(temp2);

        //    this.DataCollection.Add(temp1);
        //}

        static AllData() { }
 
        public static AllData GetInstance()
        {
            if (allData == null)
            {
                lock (typeof(AllData))
                {
                    if (allData == null)
                    {
                        allData = new AllData();
                    }
                }
            }
            return allData;
        }

        public ObservableCollection<TransactionMetadataGroup> DataCollection
        {
            get
            {
                lock (this.dataLocker)
                {
                    return this.dataCollection;
                }
            }
            set
            {
                lock (this.dataLocker)
                {
                    this.dataCollection = value;
                }
            }
        }

        private void ClearData()
        {
            this.dataCollection.Clear();
        }
    }
}
