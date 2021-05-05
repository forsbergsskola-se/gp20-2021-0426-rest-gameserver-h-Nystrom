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
            //TODO: Refactor this!
            while (true){
                switch (SelectUserOption()){
                 case 1:
                     while (true){
                         var repositoryIndex = SelectRepository();
                         _userProfile.Repositories[repositoryIndex].DisplayInfo();
                         Console.WriteLine($"\r\nOptions:\r\n1. Repositories, 2. User profile, 3: Quit");
                         var repositoryOption = GetInput(1, 3);
                         if (repositoryOption == 2){
                             break;
                         }
                         if (repositoryOption == 3){
                             Console.Clear();
                             Console.WriteLine("Quitting!");
                             return;
                         }
                     }
                     break;
                 case 2:
                     while (!FindUser()){ 
                         if (ExitApplication())
                             return;
                     }
                     Console.WriteLine("Not implemented");
                     break;
                 case 3:
                     Console.Clear();
                     Console.WriteLine("Quitting!");
                     return;
                }
            }
        }
        //TODO: Refactor this to a command class!

        #region Put in a new command class
        static int SelectUserOption(){
            Console.Clear();
            _userProfile.DisplayInfo();
            Console.WriteLine($"\r\nOptions:\r\n1. Repositories({_userProfile.public_repos}), 2. Search new user, 3: Quit");
            var userInput = GetInput(1, 3);
            Console.Clear();
            return userInput;
        }
        static int SelectRepository(){
            Console.Clear();
            Console.WriteLine($"{_userProfile.login}'s public repositories:");
            for (var i = 0; i < _userProfile.Repositories.Count; i++){
                Console.WriteLine($"({i}) {_userProfile.Repositories[i].name}");
            }
            Console.WriteLine("Options:");
            var userInput = GetInput(0, _userProfile.Repositories.Count-1);
            Console.Clear();
            return userInput;
        }
        static int GetInput(int startIndex, int maxLenght){
            var result = -1;
            while (true){
                
                if(int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out result) && result == Math.Clamp(result, startIndex, maxLenght)){
                    Console.Clear();
                    return result;
                }
                Console.WriteLine($"Error: Needs to be a number between {startIndex} and {maxLenght}");
            }
            
        }
        #endregion
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
