using MVVM.Models;
using MVVM.Readers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Services
{
    /// <summary>
    /// JSON file service.
    /// </summary>
    public class JsonFileService : IFileService
    {
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileService"/> class.
        /// </summary>
        /// <param name="fileReader">File reader.</param>
        /// <exception cref="ArgumentNullException">File reader is null.</exception>
        public JsonFileService(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        /// <summary>
        /// Get all Currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        public async Task<IList<Currency>> GetCurrencyStatistic()
        {
            var allStatistic = await _fileReader.ReadDirectory();
            return GetCurrency(allStatistic);
        }

        /// <summary>
        /// Get all Currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        public async Task<IList<Currency>> GetCurrencyStatistic(string[] files)
        {
            var allStatistic = await _fileReader.ReadFiles(files);
            return GetCurrency(allStatistic);
        }

        private static IList<Currency> GetCurrency(IList<Currency> allStatistic)
        {
            var currencies = new List<Currency>();

            foreach (var day in allStatistic)
            {

            }

            return allStatistic;
        }
    }
}
