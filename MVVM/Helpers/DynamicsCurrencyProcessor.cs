using System.Collections.ObjectModel;
using bntu.vsrpp.AHotko.Core.Model;

namespace bntu.vsrpp.AHotko.Core
{
    public class DynamicsCurrencyProcessor
    {
        //по мимо того, что надо проверять дату исключения валюты из перечня валют, к которым устанавливается официальный курс бел. рубля
        //так ещё и надо проверять количество единиц иностранной валюты к которой привязан курс 
        //наименования, количества единиц к которому устанавливается курс белорусского рубля, буквенного, цифрового кодов 
        public static async Task<ObservableCollection<RateShort>> LoadDynamicsCurrency(int curId, DateTime? start, DateTime? end)
        {
            string url = $"https://api.nbrb.by/ExRates/Rates/Dynamics/{curId}?startDate={start.Value.ToString("yyyy-MM-dd")}&endDate={end.Value.ToString("yyyy-MM-dd")}";
            //string url = "https://api.nbrb.by/exrates/rates/dynamics/431?startDate=2021-07-09&endDate=2022-09-12";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<RateShort> rateShorts = await response.Content.ReadAsAsync<ObservableCollection<RateShort>>();
                    start = start.Value.AddDays(-1);
                    foreach (var rateShort in rateShorts)
                    {
                        start = start.Value.AddDays(1);
                        rateShort.Date = start.Value;
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
