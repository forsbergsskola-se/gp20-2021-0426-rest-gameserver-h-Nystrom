namespace GitHubExplorer.API{
    public interface IUser {
        string Login{ get; }
        string Name {get;}
        string Bio{ get; }
        string Location {get;}
        string Company{ get; }
        string html_url{ get; }
        int public_repos{ get; }
        IRepository GetRepository(string repositoryName);
    }
}