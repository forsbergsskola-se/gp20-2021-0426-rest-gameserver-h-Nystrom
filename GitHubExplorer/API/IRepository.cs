using System.Collections.Generic;
using GitHubExplorer.API.Data;

namespace GitHubExplorer.API{
    public interface IRepository {
        string name {get;}
        string description {get;}
        Owner owner{ get; }
        List<IIssue> GetIssues();
        
    }
}