﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExplorer.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitHubExplorer.API{
    public abstract class GitHubRequester{
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
        protected TObject Run<TObject>(string uri){
            try{
                var requestUserTask = SendWebRequest(uri);
                requestUserTask.Wait();
                return DeserializeJson<TObject>(requestUserTask.Result);
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