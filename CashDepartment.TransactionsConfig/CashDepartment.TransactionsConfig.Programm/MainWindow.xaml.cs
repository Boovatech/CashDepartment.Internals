﻿namespace CashDepartment.TransactionsConfig.Shell
{
    using CashDepartment.TransactionsConfig.Shell.ViewModel;
    using CashDepartment.WellKnownBusinessObjects;
    using FirstFloor.ModernUI.Windows.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            WellKnownBusinessObjects.PersonalizableEnumConverterBase.Init();
            this.DataContext = new MainWindowViewModel(this.MenuLinkGroups);
        }
    }
}
