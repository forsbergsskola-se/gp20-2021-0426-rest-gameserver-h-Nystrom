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
        public string issue_events_url{ get; set; }

        public IIssue CreateIssue(string title, string description){
            throw new NotImplementedException();
        }

        public List<IIssue> GetIssues(){
            try{
                var issues = Run<List<Issue>>($"repos/{full_Name}/issues");
                return issues.Cast<IIssue>().ToList();
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }
        // public void DisplayInfo(){
        //     var info = new []{$"Repository: {Name}",$"Owner: {Owner.Login}",$"Description: {Description}",$"Created at: {created_at}", 
        //         $"Last updated: {updated_at}",$"Watchers: {Watchers}", $"Forks: {forks_count}", $"Open issues: {open_issues}", issue_events_url,html_url};
        //     foreach (var element in info){
        //         Console.WriteLine(element);
        //     }
        // }
    }
}