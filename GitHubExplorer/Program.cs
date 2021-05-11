using System;
using System.Collections.Generic;
using GitHubExplorer.API;

namespace GitHubExplorer{
    internal class Program{
        //TODO: Get issues and create/publish issues!
        //TODO: Refactor!
        
        static IUser userProfile;
        static List<IRepository> repositories;
        static List<IIssue> issues;
        static IGitHubApi gitHubApi;
        
        static void Main(string[] args){

            Authenticate();
            if (!TryGetUser()) 
                return;
            repositories = userProfile.GetRepositories;
            
            Console.WriteLine(userProfile.Name);
            Console.WriteLine(userProfile.GetRepositories.Count);
            // Console.WriteLine(repositories[0].open_issues);
            // issues = repositories[0].GetIssues();
            
            Console.WriteLine("Shutting down");
            Console.ReadKey();
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