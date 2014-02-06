using CashDepartment.TransactionsConfig.Programm.Data;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDepartment.TransactionsConfig.Programm.ViewModel
{
    public class HomeViewModel
    {
        public RelayCommand FileOpenCommand { get; set; }

        public HomeViewModel()
        {
            this.FileOpenCommand = new RelayCommand(arg => this.FileOpen());
        }

        private void FileOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файл проводки (*.provodka)|*.provodka";

            if (ofd.ShowDialog().Value)
            {
                AllData.GetInstance().LoadData(ofd.FileName);
            }
        }
    }
}
