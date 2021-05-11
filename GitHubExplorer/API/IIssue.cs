namespace GitHubExplorer.API{
    public interface IIssue{
        string UserName{ get; }
        string RepositoryName{ get; }
        string Title{ get; }
        string Body{ get; }
    }
}