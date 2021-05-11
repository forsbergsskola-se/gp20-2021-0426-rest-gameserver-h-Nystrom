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
        static List<IUser> members;
        static IGitHubApi gitHubApi;
        static IOrganization organization;
        
        static void Main(string[] args){

            Authenticate();
            if (!TryGetUser()) 
                return;
            var isActive = true;
            do{
                try{
                    Console.WriteLine(userProfile.Name);
                    if (SimpleOption("Show user information?")){
                        userProfile.ShowInfo();
                    }
                    if (SimpleOption("Get repository?")){
                        Console.Write("\nEnter repository name: ");
                        var repositoryName = Console.ReadLine();
                        repository = userProfile.GetRepository(repositoryName);
                        if (SimpleOption("Show repository information?")){
                            repository.ShowInfo();
                        }
                        if(SimpleOption("Create issue?")){
                            Console.Write("Title: ");
                            var title = Console.ReadLine();
                            Console.Write("Description: ");
                            var description = Console.ReadLine();
                            var issue = repository.CreateIssue(title, description);
                            Console.WriteLine(issue.UserName);
                        }
                        if (SimpleOption("Show all issues?")){
                            issues = repository.GetIssues();
                            foreach (var issue in issues){
                                issue.GetInfo();
                            }
                        }
                    }
                    if (SimpleOption("Get all member names of Forsbergsskolan?")){
                        organization = gitHubApi.GetOrganization("forsbergsskola-se");
                        members = organization.GetMembers();
                            foreach (var member in members){
                                Console.WriteLine($"\nUser name: {member.Login}\n{member.html_url}");
                            }
                    }
                    if (SimpleOption("Search new user?")){
                        ResetUserValues();
                        TryGetUser();
                    }
                }
                catch (Exception e){
                    Console.WriteLine(e.GetBaseException().Message);
                }

                isActive = SimpleOption("Continue?");
                Console.Clear();
            } 
            while (isActive);
            
            Console.WriteLine("Shutting down");
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
                Console.Write("Enter user name: ");
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
        static void ResetUserValues(){
            userProfile = null;
            repository = null;
            issues = null;
        }
    }
}