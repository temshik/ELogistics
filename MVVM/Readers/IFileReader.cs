using MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Readers
{
    /// <summary>
    /// File reader.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Read all files from given directory and get list of objects.
        /// </summary>
        /// <param name="targetDirectory">Target directory.</param>
        /// <returns>A <see cref="Task{IDictionary{int,IList{Currency}}}">.</returns>
        Task<IDictionary<int, IList<Currency>>> ReadDirectory(string targetDirectory = null);

        /// <summary>
        /// Read all files and get list of objects.
        /// </summary>
        /// <param name="files">Files.</param>
        /// <returns>A <see cref="Task{IDictionary{int,IList{Currency}}}">.</returns>
        Task<IDictionary<int, IList<Currency>>> ReadFiles(string[] files);

        /// <summary>
        /// Read file and get list of objects.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}">.</returns>
        Task<IList<Currency>> ReadFile(string path);
    }
}