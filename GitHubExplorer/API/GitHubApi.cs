using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubExplorer.API.Data;
using GitHubExplorer.Utility;

namespace GitHubExplorer.API{
    public class GitHubApi : IGitHubApi{
        //TODO: Get issues and create/publish issues!

        const string GitHubUrl = "https://api.github.com/users/";
        HttpClient httpClient;
        readonly JsonSerializerOptions options;
        readonly string token;

        public GitHubApi(string token){
            this.token = token;
            options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public IUser GetUser(string userName){
            try{
                var requestUserTask = HttpRequestJson(userName);
                requestUserTask.Wait();
                var user = JsonSerializer.Deserialize<UserData>($"{requestUserTask.Result}", options);
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

        IEnumerable<Repository> GetRepositories(string repositoriesUrl){
            var requestTask = HttpRequestJson(repositoriesUrl);
            requestTask.Wait();
            var temp = JsonSerializer.Deserialize<List<Repository>>(requestTask.Result, options);
            return temp;
        }

        async Task<string> HttpRequestJson(string uri){
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
    }
}