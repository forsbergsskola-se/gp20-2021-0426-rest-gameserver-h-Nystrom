using System.Collections.Generic;
using GitHubExplorer.API.Data;

namespace GitHubExplorer.API{
    public interface IRepository {
        string name {get;}
        string description {get;}
        Owner owner{ get; }
        string html_url{ get; }
        string created_at{ get; }
        string updated_at{ get; }
        int forks_count{ get; }
        int watchers{ get; }
        int open_issues{ get; }
        string issue_events_url{ get;}
        

        List<IIssue> GetIssues();
        void DisplayInfo();

    }
}