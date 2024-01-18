using MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Services
{
    /// <summary>
    /// File service.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get all currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        public Task<IList<Currency>> GetCurrencyStatistic();

        /// <summary>
        /// Get all currency statictic.
        /// </summary>
        /// <param name="files">Files.</param>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        public Task<IList<Currency>> GetCurrencyStatistic(string[] files);
    }
}
