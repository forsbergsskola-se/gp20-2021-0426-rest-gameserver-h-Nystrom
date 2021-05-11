using System;
using System.Collections.Generic;
using GitHubExplorer.API;

namespace GitHubExplorer{
    internal class Program{
        //TODO: Get issues and create/publish issues!
        //TODO: Refactor!
        
        static IUser userProfile;
        static IRepository repository;
        static List<IIssue> issues;
        static IGitHubApi gitHubApi;
        
        static void Main(string[] args){

            Authenticate();
            if (!TryGetUser()) 
                return;
            var isActive = true;
            do{
                try{
                    Console.WriteLine(userProfile.Name);
                    Console.Write("Search repository name: ");
                    var repositoryName = Console.ReadLine();
                    repository = userProfile.GetRepository(repositoryName);
                    
                    
                    if(SimpleOption("Create issue?")){
                        Console.Write("Title: ");
                        var title = Console.ReadLine();
                        Console.Write("Description: ");
                        var description = Console.ReadLine();
                        var issue = repository.CreateIssue(title, description);
                        Console.WriteLine(issue.UserName);
                    }
                    if (SimpleOption("Get issues?")){
                        issues = repository.GetIssues();
                        Console.WriteLine(issues.Count);
                    }
                }
                catch (Exception e){
                    Console.WriteLine(e.GetBaseException().Message);
                }

                isActive = SimpleOption("Continue?");
                // Console.Clear();
            } 
            while (isActive);
            
            Console.WriteLine("Shutting down");
            Console.ReadKey();
        }

        static bool SimpleOption(string message){
            Console.WriteLine($"{message} Y/N");
            switch (Console.ReadKey().Key){
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                    return false;
            }
            return true;
        }

        static void Authenticate(){
            Console.Write("Enter GitHub-API token: ");
            var token = Console.ReadLine();
            gitHubApi = new GitHubApi(token);
        }

        static bool TryGetUser(){
            try{
                Console.Write("Search user: ");
                var userName = Console.ReadLine();
                userProfile = gitHubApi.GetUser(userName);
                Console.Clear();
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                return false;
            }
            
        }
    }
}