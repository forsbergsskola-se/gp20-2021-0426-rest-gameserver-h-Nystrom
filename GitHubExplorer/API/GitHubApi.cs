using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubExplorer.API.Data;
using GitHubExplorer.Utility;

namespace GitHubExplorer.API{
    public class GitHubApi : IGitHubApi{
        const string GitHubUrl = "https://api.github.com/users/";
        HttpClient _httpClient;
        IUser _userProfile;
        string token;

        public GitHubApi(string token){
            this.token = token;
        }
        
        public IUser GetUser(string userName){
            try{
                var requestTask = HttpRequestJson(userName);
                requestTask.Wait();
                return JsonSerializer.Deserialize<UserProfile>(requestTask.Result);
            }
            catch (AggregateException e){
                throw new AggregateException(e.GetBaseException().Message);
            }
        }
        async Task<string> HttpRequestJson(string uri){
            try{
                _httpClient = new HttpClient().HeaderSetup(token, GitHubUrl);
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                _httpClient.Dispose();
                return responseBody;
            }
            catch (HttpRequestException e){
                _httpClient.Dispose();
                throw new AggregateException($"Request failed: {e.StatusCode}");
            }
        }
        // static void GetRepositories(){
        //     Console.WriteLine("Repositories:");
        //     var requestTask = HttpRequestJson();
        //     requestTask.Wait();
        //     var repo = JsonSerializer.Deserialize<List<Repository>>(requestTask.Result);
        //     if(repo.Count == 0)
        //         return;
        //     Console.WriteLine(repo.Count);
        //     Console.WriteLine(repo[0].owner.login);
        // }
    }
    
}