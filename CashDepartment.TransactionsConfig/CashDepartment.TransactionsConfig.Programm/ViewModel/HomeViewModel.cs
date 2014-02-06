using CashDepartment.TransactionsConfig.Shell.Data;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDepartment.TransactionsConfig.Shell.ViewModel
{
    public class HomeViewModel:NotifyPropertyChanged
    {
        private bool progressRingIsActive;

        public RelayCommand FileOpenCommand { get; set; }
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
            this.ProgressRingIsActive = false;
        }

        private async void FileOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файл проводки (*.provodka)|*.provodka";

            if (ofd.ShowDialog().Value)
            {
                this.ProgressRingIsActive = true;
                await Task.Factory.StartNew(() =>
                    {
                        AllData.GetInstance().LoadData(ofd.FileName);
                        this.ProgressRingIsActive = false;
                        //временное решение
                        App.Current.Dispatcher.BeginInvoke((Action)(() =>
                            {
                                var frame = VisualHelper.FindChild<FirstFloor.ModernUI.Windows.Controls.ModernFrame>(App.Current.Windows[0], "myContentFrame");
                                System.Windows.Input.NavigationCommands.Refresh.Execute(null, frame); 
                            }));
                        
                    });
            }
        }
    }
}
