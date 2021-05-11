using System.Collections.Generic;

namespace GitHubExplorer.API{
    public interface IOrganization{
        string Login{ get; }
        List<IUser> GetMembers();
    }
}