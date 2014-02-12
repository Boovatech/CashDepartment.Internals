namespace CashDepartment.TransactionsConfig.Shell.Content
{
    using CashDepartment.TransactionsConfig.Shell.Data;
    using CashDepartment.TransactionsConfig.Shell.ViewModel;
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Controls;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl, IContent
    {

        public MainContent()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

            var tcc = VisualHelper.FindChild<TransitioningContentControl>(this, null);
            if (tcc != null)
            {
                tcc.Transition = "ModernUITransition";
            }

            var currentBusinessProcessSourceType = e.Fragment;
            (this.DataContext as MainContentViewModel).NavigateTo(currentBusinessProcessSourceType); 
        }


        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {            
            //string data = e.Source.OriginalString;
            //var currentBusinessProcessSourceType = data.Substring(data.IndexOf('#') + 1);
            //(this.DataContext as MainContentViewModel).NavigateTo(currentBusinessProcessSourceType);            
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (ModernDialog.ShowMessage("Покинуть режим редактирования данных?\nВсе несохраненные данные будут утеряны.", "навигация", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
