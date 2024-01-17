using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVVM.Helpers
{
    public class CurrencyProcessor
    {
        public static async Task<IList<Currency>> LoadCurrencies()
        {
            string url = "https://api.nbrb.by/exrates/currencies";          
            
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var currencies = await response.Content.ReadFromJsonAsync<IList<Currency>>();
                    return currencies.Where(i => i.Cur_DateStart <= DateTime.Today &&
                                                 i.Cur_DateEnd > DateTime.Today).ToList();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
