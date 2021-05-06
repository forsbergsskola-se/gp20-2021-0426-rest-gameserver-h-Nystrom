using System;
using System.Collections.Generic;

namespace GitHubExplorer.API.Data{
    public class UserData : IUser{
        public string Login{ get; set; }
        public int id { get; set; }
        public string node_id{ get; set; }
        public string avatar_url{ get; set; }
        public string gravatar_id{ get; set; }
        public string url{ get; set; }
        public string html_url{ get; set; }
        public string followers_url{ get; set; }
        public string following_url{ get; set; }
        public string gists_url{ get; set; }
        public string starred_url{ get; set; }
        public string subscriptions_url{ get; set; }
        public string organizations_url{ get; set; }
        public string repos_url{ get; set; }
        public string events_url{ get; set; }
        public string received_events_url{ get; set; }
        public string type{ get; set; }
        public bool site_admin { get; set; }
        public string Name{ get; set; }
        public string Company{ get; set; }
        public string blog{ get; set; }
        public string Location { get; set; }
        public  string email{ get; set; }
        public  bool hireable{ get; set; }
        public string Bio{ get; set; }
        public string twitter_username{ get;}
        public int public_repos{ get; set; }
        public int public_gists{ get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        
        public List<IRepository> Repositories{ get; set;}

        public void DisplayInfo(){
            var info = new[]{$"User: {Login}", $"Name: {Name}", $"Bio: {Bio}",$"Location: {Location}", $"Company:{Company}",organizations_url, $"Public repositories: {public_repos}", html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
    }
}