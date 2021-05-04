using System.Collections.Generic;

namespace GitHubExplorer.API{
    public interface IRepository {
        List<IIssue> GetIssues();
        string Name {get;}
        string Description {get;}
    }
}