using MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Writers
{
    /// <summary>
    /// User CSV writer.
    /// </summary>
    public class СurrencyCsvWriter : IСurrencyWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="СurrencyCsvWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public СurrencyCsvWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(IList<Currency> currency)
        {
            var builder = new StringBuilder();

            foreach (var cur in currency)
            {
                builder.AppendLine($"{cur.Cur_ID},{cur.Cur_ParentID},{cur.Cur_Code}," +
                    $"{cur.Cur_Abbreviation},{cur.Cur_Name},{cur.Cur_Name_Bel}," +
                    $"{cur.Cur_Name_Eng},{cur.Cur_QuotName},{cur.Cur_QuotName_Bel}," +
                    $"{cur.Cur_QuotName_Eng},{cur.Cur_NameMulti},{cur.Cur_Name_BelMulti}," +
                    $"{cur.Cur_Name_EngMulti},{cur.Cur_Scale},{cur.Cur_Periodicity}," +
                    $"{cur.Cur_DateStart}, {cur.Cur_DateEnd}");
            }

            await _writer.WriteLineAsync(builder.ToString());
        }
    }
}
