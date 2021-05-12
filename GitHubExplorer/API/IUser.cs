using System;

namespace GitHubExplorer.API{
    public interface IUser {
        string Login{ get; }
        string Name {get;}
        string html_url{ get; }
        IRepository GetRepository(string repositoryName);
        void ShowInfo();
    }
}