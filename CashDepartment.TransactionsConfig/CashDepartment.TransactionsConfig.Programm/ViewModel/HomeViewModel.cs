using CashDepartment.Shared.Utils;
using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.WellKnownBusinessObjects;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    public class HomeViewModel:NotifyPropertyChanged
    {
        private bool progressRingIsActive;

        public RelayCommand FileOpenCommand { get; set; }
        public RelayCommand FileCreateCommand { get; set; }
        public LinkGroupCollection linkGroupCollection { get; set; }

        public bool ProgressRingIsActive {
            get
            {
                return this.progressRingIsActive;
            }
            set
            {
                if(this.progressRingIsActive != value)
                {
                    this.progressRingIsActive = value;

                    OnPropertyChanged("ProgressRingIsActive");
                }
            }
        }

        public HomeViewModel()
        {
            this.FileOpenCommand = new RelayCommand(arg => this.FileOpen());
            this.FileCreateCommand = new RelayCommand(arg => this.FileCreate());
            this.ProgressRingIsActive = false;
            this.linkGroupCollection = (App.Current.MainWindow as ModernWindow).MenuLinkGroups;
            var mf = VisualHelper.FindChild<ModernFrame>(App.Current.MainWindow, "ContentFrame");
            mf.KeepContentAlive = false;
        }

        private void FileCreate()
        {
            TransactionDataContext.GetInstance().CreateNewData();
            this.MenuDeInit();
            this.MenuInit();
            this.NavigateToMetaData();
        }

        private void FileOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файл проводки (*.trs)|*.trs";

            if (ofd.ShowDialog().Value)
            {
                this.MenuDeInit();
                this.ProgressRingIsActive = true;
                var ts = Task.Factory.StartNew(() =>
                    {
                        TransactionDataContext.GetInstance().LoadData(ofd.FileName);
                        this.ProgressRingIsActive = false;
                        this.MenuInit();
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.NavigateToMetaData();
                        }));                       
                    });
            }
        }

        private void MenuInit()
        {
            var menuCollection = this.linkGroupCollection.First((x) => { return x.GroupName == "MetaDataGroup"; });

            if (menuCollection != null)
            {
                var menuData = EnumHelper.GetLocalizedValuesList(typeof(BusinessProcessSourceType));

                for (int i = 0; i < menuData.Count; i++)
                {
                    if (menuData[i].Key.ToString() == "None")
                    {
                        continue;
                    }
                    var link = new Link();                      
                    link.DisplayName = menuData[i].Value;
                    link.Source = new Uri(string.Format("/Content/MainContent.xaml#{0}", menuData[i].Key), UriKind.Relative);
                    App.Current.Dispatcher.Invoke(new Action(()=>{
                        menuCollection.Links.Add(link);
                    }), System.Windows.Threading.DispatcherPriority.Normal);
                }
            }
        }

        private void MenuDeInit()
        {
            var menuCollection = this.linkGroupCollection.First((x) => { return x.GroupName == "MetaDataGroup"; });
            if (menuCollection != null && menuCollection.Links.Count > 0)
            {
                menuCollection.Links.Clear();                
            }
        }
        
        private void NavigateToMetaData()
        {
            var menuCollection = this.linkGroupCollection.First((x) => { return x.GroupName == "MetaDataGroup"; });
            if (menuCollection != null && menuCollection.Links.Count > 0)
            {
                var mf = VisualHelper.FindChild<ModernFrame>(App.Current.MainWindow, "ContentFrame");
                System.Windows.Input.NavigationCommands.GoToPage.Execute(menuCollection.Links[0].Source.OriginalString, mf);
            }             
        }
            //var mf = VisualHelper.FindChild<ModernFrame>(App.Current.MainWindow, "ContentFrame");            
            //System.Windows.Input.NavigationCommands.GoToPage.Execute("/Content/MainContent.xaml#Interbank", mf); 
    }
}
