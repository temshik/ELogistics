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
            return await _fileReader.ReadDirectory();
        }

        /// <summary>
        /// Get all Currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        public async Task<IList<Currency>> GetCurrencyStatistic(string[] files)
        {
            return await _fileReader.ReadFiles(files);
        }
    }
}
