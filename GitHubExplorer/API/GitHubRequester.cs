using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using GitHubExplorer.API.Data;
using GitHubExplorer.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitHubExplorer.API{
    public abstract class GitHubRequester{
        //TODO: Refactor if I have time!
        const string GitHubUrl = "https://api.github.com/";
        HttpClient httpClient;
        readonly JsonSerializerSettings settings;
        protected static string token;
        
        protected GitHubRequester(string token){
            GitHubRequester.token = token;
            settings = new JsonSerializerSettings{
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
        }
        protected TObject Request<TObject>(string uri){
            try{
                var requestTask = SendWebRequest(uri);
                requestTask.Wait();
                if(requestTask.Result == "")
                    throw new ArgumentException("Could not be found...");
                return DeserializeJson<TObject>(requestTask.Result);
            }
            catch (AggregateException e){
                throw new AggregateException(e.GetBaseException().Message);
            }
        }
        protected TObject Request<TObject>(string uri, PostMessage message){
            try{
                var requestTask = PostWebRequest(uri, message);
                requestTask.Wait();
                if(requestTask.Result == "")
                    throw new ArgumentException("Could not be found...");
                return DeserializeJson<TObject>(requestTask.Result);
            }
            catch (AggregateException e){
                throw new AggregateException(e.GetBaseException().Message);
            }
        }
        async Task<string> SendWebRequest(string uri){
            try{
                httpClient = new HttpClient().HeaderSetup(token, GitHubUrl);
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                return responseBody;
            }
            catch (HttpRequestException e){
                httpClient.Dispose();
                throw new AggregateException($"Request failed: {e.StatusCode}");
            }
        }
        async Task<string> PostWebRequest(string uri, PostMessage message){
            try{
                var json = JsonConvert.SerializeObject(message);
                Console.WriteLine(json);
                httpClient = new HttpClient().HeaderSetup(token, GitHubUrl);
                var response = await httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json));
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                return responseBody;
            }
            catch (HttpRequestException e){
                httpClient.Dispose();
                throw new AggregateException($"Request failed: {e.StatusCode}");
            }
        }
        TObject DeserializeJson<TObject>(string rawJson){
            try{
                var user = JsonConvert.DeserializeObject<TObject>($"{rawJson}", settings);
                return user;
            }
            catch (JsonException e){
                Console.WriteLine(e);
                throw;
            }
        }
    }
}