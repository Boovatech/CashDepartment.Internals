using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.TransactionsConfig.Shell.ViewModel;
using FirstFloor.ModernUI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace CashDepartment.TransactionsConfig.Shell.Content
{
    /// <summary>
    /// Interaction logic for MetaDataContent.xaml
    /// </summary>
    public partial class MetaDataContent : UserControl, IContent
    {
        public MetaDataContent()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var currentBusinessProcessSourceType = e.Fragment;
            (this.DataContext as MetaDataContentViewModel).NavigateTo(currentBusinessProcessSourceType);
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            
        }

        private void dgMain_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridColumnBinding.GetInstance().BindingRun();
        } 

    }
}
