using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubExplorer.Data;

namespace GitHubExplorer
{
    class Program{

        const string GitHubUrl = "https://api.github.com/users/";
        static HttpClient httpClient;
        static string myUserName = "h-nystrom";
        static string token;
        // static User userProfile;

        static void Main(string[] args){
            
            // Console.Write("Enter your username: ");
            // myUserName = Console.ReadLine();
            
            Console.Write("Enter gitHub api-key: ");
            token = Console.ReadLine();
            
            // Console.Write("Search user: ");
            // var userName = Console.ReadLine();
            
            var httpTask = GetHtmlFromWebsite("h-nystrom");
            httpTask.Wait();

            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        static async Task GetHtmlFromWebsite(string userName){
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            httpClient.BaseAddress = new Uri(GitHubUrl);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubExplorer1337", "1.0"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            
            try{
                var response = await httpClient.GetAsync($"{userName}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var userInfo = JsonSerializer.Deserialize<UserData>(responseBody);
                Console.WriteLine(userInfo?.bio);
            }
            catch (HttpRequestException e){
                Console.WriteLine(e);
            }
            httpClient.Dispose();
        }
    }
}
