using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClientApi.Utility{
    public static class HttpClientExtension{
        public static HttpClient HeaderSetup(this HttpClient httpClient, string uri){
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MMORPG", "v1"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
            return httpClient;
        }
    }
}