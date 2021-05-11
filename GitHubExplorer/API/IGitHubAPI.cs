namespace GitHubExplorer.API{
    public interface IGitHubApi{
        IUser GetUser(string userName);
        IOrganization GetOrganization(string organisationName);
    }
}