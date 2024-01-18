using MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVVM.Writers
{
    /// <summary>
    /// User JSON writer.
    /// </summary>
    internal class CurrencyJsonWriter : IСurrencyWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyJsonWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public CurrencyJsonWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(IList<Currency> сurrency)
        {
            JsonSerializer serializer = new JsonSerializer { Formatting = Formatting.Indented };
            serializer.Serialize(_writer, сurrency);
        }
    }
}
