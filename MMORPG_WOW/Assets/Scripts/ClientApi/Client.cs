using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientApi.Utility;
using UnityEngine;

namespace ClientApi{
    public class Client : IClient{
        readonly string baseUrl;

        public Client(string baseUrl){
            this.baseUrl = baseUrl;
        }
        public async Task<string> GetTargetObjects(string uri){
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
        public async Task<string> GetTargetObject(string uri, Guid id){
            var httpClient = new HttpClient().HeaderSetup($"{baseUrl}");
            try{
                var response = await httpClient.PostAsync(uri, new StringContent(id.ToString()));
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

        public Task<string> CreateTargetObject<TObject>(string uri, TObject targetObject){
            throw new NotImplementedException("Implement!");
        }
        public Task<string> ModifyTargetObject<TObject>(string uri, TObject targetObject){
            throw new NotImplementedException();
        }
        public Task<string> DeleteTargetObject<TObject>(string uri, TObject targetObject){
            throw new NotImplementedException();
        }
    }
}