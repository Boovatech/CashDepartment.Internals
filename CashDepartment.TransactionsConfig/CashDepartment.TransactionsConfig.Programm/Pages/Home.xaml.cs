﻿using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System.Linq;
using System.Windows.Controls;

namespace CashDepartment.TransactionsConfig.Shell.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IContent
    {
        public Home()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {            
            if(e.NavigationType == FirstFloor.ModernUI.Windows.Navigation.NavigationType.Back)
            {
                var menuCollection = (App.Current.MainWindow as ModernWindow).MenuLinkGroups.First((x) => { return x.GroupName == "MetaDataGroup"; });
                if (menuCollection != null && menuCollection.Links.Count > 0)
                {
                    while (menuCollection.Links.Count > 1)
                    {
                        menuCollection.Links.RemoveAt(1);
                    }                                      
                }
            }
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            
        }
    }
}
