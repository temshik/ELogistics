using MVVM.Helpers;
using MVVM.Models;
using MVVM.Services;
using MVVM.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IFileService fileService, IDialogService dialogService)
        {
            InitializeComponent();
            ApiHelper.InitializeClient();

            DataContext = new MainViewModel(fileService, dialogService, CurrencyProcessor.LoadCurrencies());
        }
    }
}
