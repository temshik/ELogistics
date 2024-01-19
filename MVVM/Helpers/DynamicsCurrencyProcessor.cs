using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVVM.Helpers
{
    public class DynamicsCurrencyProcessor
    {

        /// <summary>
        /// по мимо того, что надо проверять дату исключения валюты из перечня валют, к которым устанавливается официальный курс бел. рубля
        /// так ещё и надо проверять количество единиц иностранной валюты к которой привязан курс
        /// наименования, количества единиц к которому устанавливается курс белорусского рубля, буквенного, цифрового кодов
        /// </summary>
        /// <param name="curId">Currency Id</param>
        /// <param name="start">By default one year ago</param>
        /// <param name="end">By default today</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<IList<RateShort>> LoadDynamicsCurrency(int curId, DateOnly? start, DateOnly? end)
        {
            string url = $"/ExRates/Rates/Dynamics/{curId}?startDate={start.Value.ToString("yyyy-MM-dd")}&endDate={end.Value.ToString("yyyy-MM-dd")}";

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
