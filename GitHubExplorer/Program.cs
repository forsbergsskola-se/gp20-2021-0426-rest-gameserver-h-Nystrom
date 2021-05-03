using System;
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
            Console.WriteLine("Options:");
            DisplayUserProfile();
            UserProfileOptions();
            
            var requestTask = RequestUserProfile(_userProfile.repos_url);
            requestTask.Wait();
            Console.ReadKey();

            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        static void DisplayUserProfile(){
            Console.WriteLine(_userProfile.name);
            Console.WriteLine(_userProfile.bio);
            Console.WriteLine(_userProfile.location);
            Console.WriteLine(_userProfile.twitter_username);
            Console.WriteLine(_userProfile.email);
            Console.WriteLine(_userProfile.blog);
            Console.WriteLine(_userProfile.company);
            Console.WriteLine(_userProfile.followers);
            Console.WriteLine(_userProfile.following);
            Console.WriteLine(_userProfile.public_repos);
        }

        static async Task RequestUserProfile(string uri){
            try{
                _httpClient = new HttpClient().HeaderSetup(_token, GitHubUrl);
                var response = await _httpClient.GetAsync($"{uri}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                _userProfile = JsonSerializer.Deserialize<UserProfile>(responseBody);
            }
            catch (HttpRequestException e){
                Console.WriteLine($"Request failed: {e.StatusCode}");
                throw;
            }
            _httpClient.Dispose();
        }
        static bool UserLogin(){
            while (true){
                    RequestUserInfo();
                    try{
                        var requestTask = RequestUserProfile($"users/{_uri}");
                        requestTask.Wait();
                        Console.Clear();
                        return true;
                    }
                    catch (AggregateException e){
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

        static void UserProfileOptions(){
            switch (GetConsoleKey()){
                case ConsoleKey.D0:
                    Console.WriteLine("Option 0:");
                    break;
                case ConsoleKey.D1:
                    Console.WriteLine("Option 1:");
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Option 2:");
                    break;
                default:
                    Console.WriteLine("Unknown input");
                    break;
            }
        }
        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }
    }
}
