using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubExplorer.Utility{
    public static class HttpClientExtension{
        public static HttpClient HeaderSetup(this HttpClient httpClient, string token, string uri){
            httpClient.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubExplorer1337", "1.0"));
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            return httpClient;
        }
    }
}