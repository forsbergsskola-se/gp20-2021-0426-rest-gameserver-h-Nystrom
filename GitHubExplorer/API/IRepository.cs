using System.Collections.Generic;

namespace GitHubExplorer.API{
    public interface IRepository {
        string Name {get;}
        string full_Name{ get; }
        string Description {get;}
        public string Login{ get;}
        string html_url{ get; }
        string created_at{ get; }
        string updated_at{ get; }
        int forks_count{ get; }
        int Watchers{ get; }
        int open_issues{ get; }
        string issue_events_url{ get;}
        IIssue CreateIssue(string title, string description);
        List<IIssue> GetIssues();
        void ShowInfo();

    }
}