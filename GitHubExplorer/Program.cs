using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubExplorer.Data;

namespace GitHubExplorer
{
    class Program{

        const string GitHubUrl = "https://api.github.com/";
        static HttpClient _httpClient;
        static string _uri;
        static string _token;
        static UserProfile _userProfile;
        
        static void Main(string[] args){

            if (!UserLogin()){
                Console.Clear();
                Console.WriteLine("Program is shutting down!");
                return;
            }
            Console.WriteLine($"Welcome {_userProfile.login}!");
            //TODO: Display user profile info:
            InputNavigation();
            
            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        // static void DisplayUserProfile(){
        //     Console.WriteLine(_userProfile.name);
        //     Console.WriteLine(_userProfile.bio);
        //     Console.WriteLine(_userProfile.location);
        //     Console.WriteLine(_userProfile.twitter_username);
        //     Console.WriteLine(_userProfile.email);
        //     Console.WriteLine(_userProfile.blog);
        //     Console.WriteLine(_userProfile.company);
        //     Console.WriteLine(_userProfile.followers);
        //     Console.WriteLine(_userProfile.following);
        //     Console.WriteLine(_userProfile.public_repos);
        // }

        static async Task<string> HttpRequestJson(string uri){
            try{
                _httpClient = new HttpClient().HeaderSetup(_token, GitHubUrl);
                var response = await _httpClient.GetAsync($"{uri}");
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
        static bool UserLogin(){
            while (true){
                    RequestUserInfo();
                    try{
                        var requestTask = HttpRequestJson($"users/{_uri}");
                        requestTask.Wait();
                        _userProfile = _userProfile = JsonSerializer.Deserialize<UserProfile>(requestTask.Result);
                        Console.Clear();
                        return true;
                    }
                    catch (AggregateException e){
                        Console.WriteLine(e.GetBaseException().Message);
                        Console.WriteLine("Press enter to try again!");
                        if (GetConsoleKey() != ConsoleKey.Enter){
                            return false;
                        }
                    }
            }
        }
        static void RequestUserInfo(){
            Console.Write("Enter your username: ");
            _uri = Console.ReadLine();
            Console.Write("Enter gitHub api-key: ");
            _token = Console.ReadLine();
        }

        static void InputNavigation(){
            Console.WriteLine("Options:");
            switch (GetConsoleKey()){
                case ConsoleKey.D1:
                    GetRepositories();
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Option 2:");
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Option 3:");
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Option 4:");
                    break;
                default:
                    Console.WriteLine("Unknown input");
                    break;
            }
        }

        static void GetRepositories(){
            Console.WriteLine("Repositories:");
            var requestTask = HttpRequestJson(_userProfile.repos_url);
            requestTask.Wait();
            var repo = JsonSerializer.Deserialize<List<Repository>>(requestTask.Result);
            if(repo.Count == 0)
                return;
            Console.WriteLine(repo.Count);
            Console.WriteLine(repo[0].owner.login);
        }

        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }
    }
}
