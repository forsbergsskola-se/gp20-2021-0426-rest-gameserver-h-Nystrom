using System;
using GitHubExplorer.API.Data;

namespace GitHubExplorer.API{
    public class Issue : IIssue{
        public string UserName => User.Login;
        public string repository_url{ get; set; }
        public string Title{ get; set; }
        public string Body{ get; set; }
        public Owner User{ get; set; }
        public string created_at{ get; set; }
        public void GetInfo(){
            var repository = repository_url.Split("/");
            var content = new []{$"Repository: {repository[^1]}",$"Title : {Title}",$"Description: {Body}",$"Created by: {User.Login} at {created_at}\n {repository_url}"};
            foreach (var info in content){
                Console.WriteLine(info);
            }
        }
    }
}