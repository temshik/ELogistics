using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVVM.Helpers
{
    public class DayCurrencyProcessor
    {
        public static async Task<List<Rate>> LoadDayCurrency(bool periodicity = true)
        {
            string url = "";
            if (periodicity == false)
            {
                url = $"https://api.nbrb.by/exrates/rates?periodicity=1";
            }
            else
            {
                url = $"https://api.nbrb.by/exrates/rates?periodicity=0";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Rate> rates = await response.Content.ReadFromJsonAsync<List<Rate>>();
                    return rates;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
