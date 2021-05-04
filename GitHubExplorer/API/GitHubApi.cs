using System;
using System.Collections.Generic;
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
                var requestUserTask = HttpRequestJson(userName);
                requestUserTask.Wait();
                var user = JsonSerializer.Deserialize<UserData>($"{requestUserTask.Result}");
                user.Repositories = new List<IRepository>();
                if (user.public_repos == 0)
                    return user;
                var temp = GetRepositories(user.repos_url);
                user.Repositories.AddRange(temp);
                return user;
            }
            catch (AggregateException e){
                throw new AggregateException(e.GetBaseException().Message);
            }
        }
        List<Repository> GetRepositories(string repositoriesUrl){
            var requestTask = HttpRequestJson(repositoriesUrl);
            requestTask.Wait();
            var temp = JsonSerializer.Deserialize<List<Repository>>(requestTask.Result);
            return temp;
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
    }
    
}