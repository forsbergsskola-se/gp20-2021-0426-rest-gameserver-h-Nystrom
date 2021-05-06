using System;
using System.Collections.Generic;

namespace GitHubExplorer.API.Data{
    [Serializable]
    public class Repository : IRepository{
        public string Name{ get; set; }
        public string Description{ get; set; }
        public Owner Owner{ get; set; }
        public string html_url{ get; set; }
        public string created_at{ get; set;}
        public string updated_at{ get; set;}
        public int forks_count{ get; set;}
        public int Watchers{ get; set;}
        public int open_issues{ get; set;}
        public string issue_events_url{ get; set; }

        public List<IIssue> GetIssues(){
            throw new NotImplementedException();//TODO: Implement!
        }
        public void DisplayInfo(){
            var info = new []{$"Repository: {Name}",$"Owner: {Owner.Login}",$"Description: {Description}",$"Created at: {created_at}", 
                $"Last updated: {updated_at}",$"Watchers: {Watchers}", $"Forks: {forks_count}",html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
    }
}