namespace GitHubExplorer.API{
    public interface IIssue{
        string UserName{ get; }
        string repository_url{ get; }
        string Title{ get; }
        string Body{ get; }
        void GetInfo();
    }
}