using System.Collections.Generic;

namespace GitHubExplorer.API{
    public interface IRepository {
        string Name {get;}
        string full_Name{ get; }
        string Description {get;}
        public string Login{ get;}
        IIssue CreateIssue(string title, string description);
        List<IIssue> GetIssues();
        void ShowInfo();

    }
}