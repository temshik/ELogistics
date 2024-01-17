using LiveCharts;
using LiveCharts.Configurations;
using MVVM.AsyncCommand;
using MVVM.Models;
using MVVM.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTracker.Helpers;

namespace MVVM.ViewModels
{
    public class MainViewModel : BaseVM
    {
        #region private fields
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private Currency _selectedCurrency;
        private NotifyTaskCompletion<IList<Currency>> _currencies;
        private ChartValues<decimal> _selectedCurrencyCurOfficialRate;
        private ChartValues<DateTime> _date;

        private AsyncCommand<NotifyTaskCompletion<IList<Currency>>> _openCommand;
        private AsyncCommand<Currency> _saveCommand;
        #endregion

        #region properties
        public NotifyTaskCompletion<IList<Currency>> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        public Currency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                if (_selectedCurrency != null)
                {
                    //отображение на графике текущего курса
                    /*var selectedUserSteps = _selectedUser.UserData.Select(u => (u.Value.Steps));
                    SelectedUserSteps = new ChartValues<int>(selectedUserSteps);
                    var days = _selectedUser.UserData.Select(u => (u.Key));
                    Days = new ChartValues<int>(days);*/
                }
                else
                {
                    SelectedCurrencyCurOfficialRate = null;
                }
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public ChartValues<decimal> SelectedCurrencyCurOfficialRate
        {
            get { return _selectedCurrencyCurOfficialRate; }
            set
            {
                _selectedCurrencyCurOfficialRate = value;

                // color the minimum and maximum points of the selected cur rate graph
                if (value != null)
                {
                    ColorMinAndMaxPoint();
                }

                OnPropertyChanged(nameof(SelectedCurrencyCurOfficialRate));
            }
        }

        public ChartValues<DateTime> Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        #endregion

        public MainViewModel(IFileService fileService, IDialogService dialogService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Currencies = new NotifyTaskCompletion<IList<Currency>>(_fileService.GetUsersStatistic());
        }

        #region open files command
        public AsyncCommand<NotifyTaskCompletion<IList<User>>> OpenCommand
        {
            get
            {
                return _openCommand ??= new AsyncCommand<NotifyTaskCompletion<IList<Currency>>>(async (obj) =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog() == true)
                        {
                            Users = new NotifyTaskCompletion<IList<User>>(_fileService.GetUsersStatistic(_dialogService.FilePaths));
                            _dialogService.ShowMessage("Файлы открыты");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }
        #endregion

        #region save User command
        public AsyncCommand<Currency> SaveCommand
        {
            get
            {
                return _saveCommand ??= new AsyncCommand<Currency>(async (user) =>
                {
                    try
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            var fileExtension = _dialogService.FileExtension;
                            using StreamWriter writer = new StreamWriter(File.Create(_dialogService.FilePath));
                            switch (fileExtension)
                            {
                                case ".xml":
                                    await new UserXmlWriter(writer).Write(user);
                                    break;
                                case ".json":
                                    await new UserJsonWriter(writer).Write(user);
                                    break;
                                case ".csv":
                                    await new UserCsvWriter(writer).Write(user);
                                    break;
                                default:
                                    throw new FileFormatException("Неподдерживаемый формат файла");
                            }

                            _dialogService.ShowMessage("Файл сохранен");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }

        #endregion

        #region private helpers
        private void ColorMinAndMaxPoint()
        {
            var mapper = new CartesianMapper<int>()
                .X((value, index) => index)
                .Y(value => value);
                /*.Fill((value, index) =>
                value == SelectedUser?.TheBestResult ?
                    Brushes.Green : value == SelectedUser?.TheWorstResult ?
                        Brushes.Red : null);*/
            LiveCharts.Charting.For<int>(mapper, SeriesOrientation.Horizontal);
        }
        #endregion
    }
}
