using LiveCharts;
using LiveCharts.Configurations;
using MVVM.AsyncCommand;
using MVVM.Helpers;
using MVVM.Models;
using MVVM.Services;
using MVVM.Writers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
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
        private ChartValues<DateOnly> _date;

        private AsyncCommand<NotifyTaskCompletion<IList<Currency>>> _openCommand;
        private AsyncCommand<Currency> _saveCommand;
        private AsyncCommand<Currency> _drowGridCommand;
        private AsyncCommand<Currency> _editCommand;

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

                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public ChartValues<decimal> SelectedCurrencyCurOfficialRate
        {
            get { return _selectedCurrencyCurOfficialRate; }
            set
            {
                _selectedCurrencyCurOfficialRate = value;                
                
                if (value != null)
                {
                    ColorMinAndMaxPoint();
                }

                OnPropertyChanged(nameof(SelectedCurrencyCurOfficialRate));
            }
        }

        public ChartValues<DateOnly> Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        #endregion

        public MainViewModel(IFileService fileService, IDialogService dialogService, Task<IList<Currency>> currencies)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Currencies = new NotifyTaskCompletion<IList<Currency>>(_fileService.GetCurrencyStatistic());
            //Currencies = new NotifyTaskCompletion <IList<Currency>>(currencies);
        }

        #region drow grid command
        public AsyncCommand<Currency> DrowGridCommand
        {
            get
            {
                return _drowGridCommand ??= new AsyncCommand<Currency>(async (currency) =>
                {
                    try
                    {
                        var model = await DynamicsCurrencyProcessor.LoadDynamicsCurrency(currency.Cur_ID, DateOnly.ParseExact("31/01/2023", "dd/MM/yyyy"), DateOnly.ParseExact("17/01/2024", "dd/MM/yyyy"));

                        SelectedCurrencyCurOfficialRate = new ChartValues<decimal>(model.Select(u => (u.Cur_OfficialRate)));
                        Date = new ChartValues<DateOnly>(model.Select(u => DateOnly.FromDateTime(u.Date)));                                                
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }
        #endregion

        #region edit currency command
        public AsyncCommand<Currency> EditCommand
        {
            get
            {
                return _editCommand ??= new AsyncCommand<Currency>(async (obj) =>
                {
                    try
                    {
                        Currency currency = obj as Currency;
                        if (currency != null) 
                        {
                            Currencies.Result.Remove(currency);
                            Currencies.Result.Add(currency);
                            SelectedCurrency = currency;
                        }
                        //Currencies

                        _dialogService.ShowMessage("Запись изменена");
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }
        #endregion

        #region open files command
        public AsyncCommand<NotifyTaskCompletion<IList<Currency>>> OpenCommand
        {
            get
            {
                return _openCommand ??= new AsyncCommand<NotifyTaskCompletion<IList<Currency>>>(async (obj) =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog() == true)
                        {
                            Currencies = new NotifyTaskCompletion<IList<Currency>>(_fileService.GetCurrencyStatistic(_dialogService.FilePaths));
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

        #region save Currency command
        public AsyncCommand<Currency> SaveCommand
        {
            get
            {
                return _saveCommand ??= new AsyncCommand<Currency>(async (currency) =>
                {
                    try
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            var fileExtension = _dialogService.FileExtension;
                            using StreamWriter writer = new StreamWriter(File.Create(_dialogService.FilePath));
                            switch (fileExtension)
                            {
                                /*case ".xml":
                                    await new UserXmlWriter(writer).Write(currency);
                                    break;*/
                                case ".json":
                                    await new CurrencyJsonWriter(writer).Write(Currencies.Result);
                                    break;
                                /*case ".csv":
                                    await new UserCsvWriter(writer).Write(currency);
                                    break;*/
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
                .Y(value => value)
                .Fill((value, index) =>
                value == SelectedCurrencyCurOfficialRate.Max() ?
                    Brushes.Red : value == SelectedCurrencyCurOfficialRate.Min() ?
                    Brushes.Green : null);
            LiveCharts.Charting.For<int>(mapper, SeriesOrientation.Horizontal);
        }
        #endregion
    }
}
