using MVVM.ErrorHandler;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVVM.Readers
{
    /// <summary>
    /// JSON file reader.
    /// </summary>
    public class JsonFileReader : IFileReader
    {
        private readonly IErrorHandler _errorHandler;
        private readonly string _directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileReader"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">Configuration is null.</exception>
        public JsonFileReader(IErrorHandler errorHandler, string directory)
        {
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _directory = directory;// ?? throw new ArgumentNullException(nameof(directory));
        }

        /// <summary>
        /// Get data about users for the entire period.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        /// <returns>
        ///     A <see cref="Task{IList{Currency}}">.
        ///     Returns a dictionary where the key is the number of the day and the value is information about the users on that day.
        /// </returns>
        public async Task<IList<Currency>> ReadDirectory(string targetDirectory = null)
        {
            if (targetDirectory == null)
            {
                targetDirectory = _directory;
            }

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            IList<Currency> allStatistic = await ReadFiles(fileEntries);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                await ReadDirectory(subdirectory);

            return allStatistic;
        }

        public async Task<IList<Currency>> ReadFiles(string[] files)
        {
            var allStatistic = new List<Currency>();
            foreach (string fileName in files)
            {
                try
                {
                    var dayStatistic = await ReadFile(fileName);
                    allStatistic.AddRange(dayStatistic);
                }
                catch (JsonException jsonex)
                {
                    _errorHandler.HandleError(jsonex, $"Файл '{fileName}' не соответствует формату.");
                }
            }

            return allStatistic;
        }

        /// <summary>
        /// Get data about users for a specific day.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        /// <returns>
        ///     A <see cref="Task{IList{Currency}}">.
        ///     Returns a list with the information about the users on the day.
        /// </returns>
        public async Task<IList<Currency>> ReadFile(string path)
        {
            using FileStream fs = File.OpenRead(path);
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new JsonStringEnumConverter { });
            return await JsonSerializer.DeserializeAsync<IList<Currency>>(fs, serializerOptions);
        }
    }
}
