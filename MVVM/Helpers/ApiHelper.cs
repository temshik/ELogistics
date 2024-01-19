using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVVM.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient(string baseUrl)
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(baseUrl);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatino/json"));
        }
    }
}