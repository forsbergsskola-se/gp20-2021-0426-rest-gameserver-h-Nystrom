using System;
using System.Globalization;
using GitHubExplorer.API;

namespace GitHubExplorer
{
    class Program{
        
        static IUser _userProfile;
        static IGitHubApi _gitHubApi;

        static void Main(string[] args){
            
            while (true){
                Console.WriteLine("Authorization:");
                InitializeApi();
                if (FindUser()) 
                    break;
                if (ExitApplication()) 
                    return;
            }
            while (true){//TODO: Refactor this!!!
                Console.Clear();
                DisplayUserProfile();
                Console.WriteLine($"\r\nOptions:\r\n1. Repositories({_userProfile.public_repos-1}), 2. Search new user, 3: Quit");
                var input = GetConsoleKey();
                if (input == ConsoleKey.D1){
                    var result = 0;
                    var repoInput = false;
                    while (!repoInput && result == Math.Clamp(result, 0, _userProfile.public_repos-1)){
                        Console.Clear();
                        DisplayRepositories();
                        Console.WriteLine($"\r\n Pick a repository between 0 and {_userProfile.Repositories.Count}:");
                        repoInput = int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out result);
                    }
                    Console.Clear();
                    _userProfile.Repositories[result].DisplayInfo();
                    Console.WriteLine("\r\nOptions:\r\nPress any key to go back! (WIP)");
                    Console.ReadLine();
                }
                else if (input == ConsoleKey.D2){
                    while (!FindUser()){
                        if (ExitApplication())
                            break;
                    }
                }else if (input == ConsoleKey.D3){
                    Console.Clear();
                    Console.WriteLine("Shutting down!");
                    break;
                }
            }
        }

        static void DisplayRepositories(){
            Console.Clear();
            Console.WriteLine("Options:");
            Console.WriteLine("Repositories:");
            for (var i = 0; i < _userProfile.Repositories.Count; i++){
                Console.WriteLine($"({i}) {_userProfile.Repositories[i].name}");
                _userProfile.Repositories[i].DisplayInfoSnippet();
            }
        }
        static void InitializeApi(){
            Console.Write("Enter gitHub api-key: ");
            var token = Console.ReadLine();
            _gitHubApi = new GitHubApi(token);
        }
        static bool FindUser(){
            Console.Write("Enter a username: ");
            var userName = Console.ReadLine();
            try{
                _userProfile = _gitHubApi.GetUser(userName);
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
            }
            return false;
        }
        static void DisplayUserProfile(){
            Console.Clear();
            _userProfile.DisplayInfo();
        }
        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }
        static bool ExitApplication(){
            Console.WriteLine("Press enter to try again!");
            if (GetConsoleKey() != ConsoleKey.Enter){
                Console.Clear();
                Console.WriteLine("Shutting down!");
                return true;
            }
            Console.Clear();
            return false;
        }
    }
}
