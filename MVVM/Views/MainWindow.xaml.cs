using MVVM.Services;
using MVVM.ViewModels;
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

            DataContext = new MainViewModel(fileService, dialogService);
        }
    }
}
