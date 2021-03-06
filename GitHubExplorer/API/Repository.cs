using System;
using System.Collections.Generic;
using System.Linq;
using GitHubExplorer.API.Data;

namespace GitHubExplorer.API{
    [Serializable]
    public class Repository : GitHubRequester, IRepository{
        public Repository() : base(token){ }
        
        public string Name{ get; set; }
        public string full_Name{ get; set; }
        public string Description{ get; set; }
        public Owner Owner{ get; set; }
        public string Login => Owner.Login;
        public string html_url{ get; set; }
        public string created_at{ get; set;}
        public string updated_at{ get; set;}
        public int forks_count{ get; set;}
        public int Watchers{ get; set;}
        public int open_issues{ get; set;}

        public IIssue CreateIssue(string title, string description){
            try{
                return Request<Issue>($"repos/{full_Name}/issues",new PostMessage(title, description));
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }

        public List<IIssue> GetIssues(){
            try{
                var issues = Request<List<Issue>>($"repos/{full_Name}/issues");
                return issues.Cast<IIssue>().ToList();
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }
        public void ShowInfo(){
            var info = new []{$"Repository: {Name}",$"Owner: {Owner.Login}",$"Description: {Description}",$"Created at: {created_at}", 
                $"Last updated: {updated_at}",$"Watchers: {Watchers}", $"Forks: {forks_count}", $"Open issues: {open_issues}",html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
    }
}