using System;
using System.Collections.Generic;

namespace GitHubExplorer.API{
    public class UserData : GitHubRequester, IUser{
        public string Login{ get; set; }
        public string url{ get; set; }
        public string html_url{ get; set; }
        public string organizations_url{ get; set; }
        public string repos_url{ get; set; }
        public string events_url{ get; set; }
        public string received_events_url{ get; set; }
        public string Name{ get; set; }
        public string Company{ get; set; }
        public string Location { get; set; }
        public string Bio{ get; set; }
        public int public_repos{ get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        
        public List<IRepository> GetRepositories{ get; set;}

        public void DisplayInfo(){
            var info = new[]{$"User: {Login}", $"Name: {Name}", $"Bio: {Bio}",$"Location: {Location}", $"Company:{Company}",organizations_url, $"Public repositories: {public_repos}", html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
        public UserData(string token) : base(token){ }
    }
}