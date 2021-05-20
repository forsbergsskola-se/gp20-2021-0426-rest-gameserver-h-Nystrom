using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientApi.Utility;
using Newtonsoft.Json;
using UnityEngine;

namespace ClientApi{
    public class Client : IClient{
        const string MediaTypeString = "application/json";
        readonly string baseUrl;
        Client(string baseUrl){
            this.baseUrl = baseUrl;
        }
        public static IClient NewClient(string baseUrl){
            return new Client(baseUrl);
        }
        public async Task<string> GetWebRequest(string uri){
            var httpClient = new HttpClient().HeaderSetup($"{baseUrl}");
            try{
                var response = await httpClient.GetAsync(uri);
                Debug.Log(response);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                return responseBody;
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                throw;
            }
        }
        public async Task<string> PostWebRequest<TObject>(string uri, TObject targetObject){
            var httpClient = new HttpClient().HeaderSetup($"{baseUrl}");
            try{
                var jsonRequest = JsonConvert.SerializeObject(targetObject);
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, requestContent);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                Debug.Log(response);
                return responseBody;
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                throw;
            }
        }
        public async Task<string> DeleteTargetObject(string uri){
            var httpClient = new HttpClient().HeaderSetup($"{baseUrl}");
            try{
                var response = await httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                Debug.Log(response);
                return responseBody;
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                throw;
            }
        }
    }
}