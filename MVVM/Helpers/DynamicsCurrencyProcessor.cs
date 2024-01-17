using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVVM.Helpers
{
    public class DynamicsCurrencyProcessor
    {
        //по мимо того, что надо проверять дату исключения валюты из перечня валют, к которым устанавливается официальный курс бел. рубля
        //так ещё и надо проверять количество единиц иностранной валюты к которой привязан курс 
        //наименования, количества единиц к которому устанавливается курс белорусского рубля, буквенного, цифрового кодов 
        public static async Task<IList<RateShort>> LoadDynamicsCurrency(int curId, DateOnly? start, DateOnly? end)
        {
            string url = $"https://api.nbrb.by/ExRates/Rates/Dynamics/{curId}?startDate={start.Value.ToString("yyyy-MM-dd")}&endDate={end.Value.ToString("yyyy-MM-dd")}";
            //string url = "https://api.nbrb.by/exrates/rates/dynamics/431?startDate=2021-07-09&endDate=2022-09-12";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    IList<RateShort> rateShorts = await response.Content.ReadFromJsonAsync<IList<RateShort>>();
                    start = start.Value.AddDays(-1);
                    foreach (var rateShort in rateShorts)
                    {
                        start = start.Value.AddDays(1);
                        rateShort.Date = start.Value.ToDateTime(TimeOnly.MinValue);
                    }
                    return rateShorts;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
