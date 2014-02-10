using CashDepartment.TransactionsConfig.Data;
using FirstFloor.ModernUI.Presentation;
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
    public class TransactionDataContext
    {
        private static TransactionDataContext transactionDataContext;
        private object dataLocker;
        private ObservableCollection<TransactionMetadataGroup> dataCollection;

        private TransactionDataContext() 
        {
            this.dataLocker = new object();
            this.DataCollection = new ObservableCollection<TransactionMetadataGroup>();
        }

        public void LoadData(string path)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.dataCollection.Clear();
                }));
                if (File.Exists(path))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<TransactionMetadataGroup>));
                    using (StreamReader sr = new StreamReader(path))
                    {
                        var collection = xml.Deserialize(sr) as ObservableCollection<TransactionMetadataGroup>;
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.dataCollection = collection;
                        }));
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

        static TransactionDataContext() { }
 
        public static TransactionDataContext GetInstance()
        {
            if (transactionDataContext == null)
            {
                lock (typeof(TransactionDataContext))
                {
                    if (transactionDataContext == null)
                    {
                        transactionDataContext = new TransactionDataContext();
                    }
                }
            }
            return transactionDataContext;
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

        public void AddToDataCollection(TransactionMetadataGroup tmd)
        {
            lock (this.dataLocker)
            {
                this.dataCollection.Add(tmd);
            }
        }

        public void CreateNewData()
        {
            lock (this.dataLocker)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.dataCollection.Clear();
                }));
            }    
        }
    }
}
