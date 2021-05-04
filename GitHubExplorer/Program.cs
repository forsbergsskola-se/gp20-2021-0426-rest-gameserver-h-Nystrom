using System;
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
            while (true){
                DisplayUserProfile();
                Options();
                if (ExitApplication()) 
                    break;
            }
        }

        static void Options(){
            Console.WriteLine("\r\nOptions:");
            Console.WriteLine("1. Repositories, 2. Search user, 3. Quit");
            var temp = GetConsoleKey();
            switch (temp){
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("OpenRepo");
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Search other");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Quit");
                    break;
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
        // static void InputNavigation(){
        //     Console.WriteLine("Options:");
        //     switch (GetConsoleKey()){
        //         case ConsoleKey.D1:
        //             // GetRepositories();
        //             break;
        //         case ConsoleKey.D2:
        //             Console.WriteLine("Option 2:");
        //             break;
        //         case ConsoleKey.D3:
        //             Console.WriteLine("Option 3:");
        //             break;
        //         case ConsoleKey.D4:
        //             Console.WriteLine("Option 4:");
        //             break;
        //         default:
        //             Console.WriteLine("Unknown input");
        //             break;
        //     }
        // }
    }
}
