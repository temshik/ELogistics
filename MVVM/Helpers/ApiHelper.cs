using System.Net.Http;
using System.Net.Http.Headers;

namespace MVVM.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("https://api.nbrb.by/exrates/rates/dynamics/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatino/json"));
        }
    }
}