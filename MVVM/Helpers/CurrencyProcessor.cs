using System.Collections.ObjectModel;
using bntu.vsrpp.AHotko.Core.Model;

namespace bntu.vsrpp.AHotko.Core
{
    public class CurrencyProcessor
    {
        public static async Task<ObservableCollection<Currency>> LoadCurrencies()
        {
            string url = "https://api.nbrb.by/exrates/currencies";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<Currency> currencies = await response.Content.ReadAsAsync<ObservableCollection<Currency>>();
                    return currencies;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
