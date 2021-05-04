using System;
using System.Collections.Generic;

namespace GitHubExplorer.API.Data{
    [Serializable]
    public class Repository : IRepository{
        public string name{ get; set; }
        public string description{ get; set; }
        public Owner owner{ get; set; }
        public string html_url{ get; set; }
        public string created_at{ get; set;}
        public string updated_at{ get; set;}
        public int forks_count{ get; set;}
        public int watchers{ get; set;}
        public int open_issues{ get; set;}
        public string issue_events_url{ get; set; }

        public List<IIssue> GetIssues(){
            throw new NotImplementedException();
        }
        public void DisplayInfoSnippet(){
            var info = new []{$"Repository: {name}",$"Owner: {owner.login}",$"Created at: {created_at}",$"Open issues: {open_issues}",html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
        public void DisplayInfo(){
            var info = new []{$"Repository: {name}",$"Owner: {owner.login}",$"Description: {description}",$"Created at: {created_at}", $"Last updated: {updated_at}",$"Watchers: {watchers}", $"Forks: {forks_count}" ,$"Open issues: {open_issues}", issue_events_url,html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
    }
}