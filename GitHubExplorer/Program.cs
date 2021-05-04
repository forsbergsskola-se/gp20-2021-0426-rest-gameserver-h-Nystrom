using System;
using System.Collections.Generic;
using GitHubExplorer.API;
using GitHubExplorer.API.Data;
using GitHubExplorer.Utility;

namespace GitHubExplorer
{
    class Program{
        
        static IUser _userProfile;
        static IGitHubApi gitHubApi;

        static void Main(string[] args){
            
            while (true){
                Console.WriteLine("Authorization:");
                InitializeApi();
                if (FindUser()) 
                    break;
                if (ExitApplication()) 
                    return;
            }
            while (true){
                //TODO: Display current user info
                //TODO: Navigate userInfo
                //TODO: Get repos + display them
                //TODO: Get issues
                InputNavigation();
            }
            Console.WriteLine("Welcome");
            // var user = gitHubApi.GetUser(authorization);
            // var repository = user.GetRepository("");
            // var issues = repository.GetIssues();
            // Console.WriteLine($"Welcome {_userProfile.login}!");
            //TODO: Display user profile info:
            // InputNavigation();

            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        static bool FindUser(){
            Console.Write("Enter a username: ");
            var userName = Console.ReadLine();
            try{
                _userProfile = gitHubApi.GetUser(userName);
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
            }

            return false;
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

        static void InitializeApi(){
            Console.Write("Enter gitHub api-key: ");
            var token = Console.ReadLine();
            gitHubApi = new GitHubApi(token);
        }
        static void InputNavigation(){
            Console.WriteLine("Options:");
            switch (GetConsoleKey()){
                case ConsoleKey.D1:
                    // GetRepositories();
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
        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }
    }
}
