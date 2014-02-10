using CashDepartment.TransactionsConfig.Shell.Data;
using CashDepartment.TransactionsConfig.Shell.ViewModel;
using FirstFloor.ModernUI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace CashDepartment.TransactionsConfig.Shell.Content
{
    /// <summary>
    /// Interaction logic for Params.xaml
    /// </summary>
    public partial class Params : UserControl, IContent
    {
        public Params()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var currentBusinessProcessSourceType = e.Fragment;
            (this.DataContext as ParamsViewModel).FrameNavigate(currentBusinessProcessSourceType);
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

        private void dgMain_Loaded1(object sender, RoutedEventArgs e)
        {
            DataGridColumnBinding.GetInstance().BindingRun();
        }
    }
}
