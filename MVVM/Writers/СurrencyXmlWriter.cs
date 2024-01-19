using MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVM.Writers
{
    /// <summary>
    /// User XML writer.
    /// </summary>
    internal class СurrencyXmlWriter : IСurrencyWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="СurrencyXmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public СurrencyXmlWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(IList<Currency> currency)
        {
            XElement currencyInfo = new XElement("Currencies");
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), currencyInfo);
            
            foreach (var item in currency)
            {
                var currencyData = new XElement("Currency");
                currencyData.Add(
                    new XElement("Cur_ID", item.Cur_ID),
                    new XElement("Cur_ParentID", item.Cur_ParentID),
                    new XElement("Cur_Code", item.Cur_Code),
                    new XElement("Cur_Name", item.Cur_Name),
                    new XElement("Cur_Name_Bel", item.Cur_Name_Bel),
                    new XElement("Cur_Name_Eng", item.Cur_Name_Eng),
                    new XElement("Cur_QuotName", item.Cur_QuotName),
                    new XElement("Cur_QuotName_Bel", item.Cur_QuotName_Bel),
                    new XElement("Cur_QuotName_Eng", item.Cur_QuotName_Eng),
                    new XElement("Cur_NameMulti", item.Cur_NameMulti),
                    new XElement("Cur_Name_BelMulti", item.Cur_Name_BelMulti),
                    new XElement("Cur_Name_EngMulti", item.Cur_Name_EngMulti),
                    new XElement("Cur_Scale", item.Cur_Scale),
                    new XElement("Cur_Periodicity", item.Cur_Periodicity),
                    new XElement("Cur_DateStart", item.Cur_DateStart),
                    new XElement("Cur_DateEnd", item.Cur_DateEnd)
                    );
                currencyInfo.Add(currencyData);
            }
            
            await doc.SaveAsync(_writer, SaveOptions.None, default(CancellationToken));
        }
    }
}
