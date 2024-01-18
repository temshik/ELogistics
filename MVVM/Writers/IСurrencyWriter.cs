using MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Writers
{
    /// <summary>
    /// Сurrency writer.
    /// </summary>
    public interface IСurrencyWriter
    {
        /// <summary>
        /// Writes the specified сurrency data.
        /// </summary>
        /// <param name="сurrency">The сurrency informtion.</param>
        /// <returns>A <see cref="Task">.</returns>
        Task Write(IList<Currency> сurrency);
    }
}